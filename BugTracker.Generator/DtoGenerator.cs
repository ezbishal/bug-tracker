using BugTracker.Shared.Generator.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace BugTracker.Shared.Generator;

[Generator]
public class DtoGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var classDeclarationsWithAttribute = context.SyntaxProvider.CreateSyntaxProvider(
				predicate: static (node, _) => node is ClassDeclarationSyntax classNode &&
											   classNode.AttributeLists.Any(al => al.Attributes.Any(a => a.Name.ToString() == nameof(GenerateDto))),
				transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
			).Where(m => m is not null);

		var compilation = context.CompilationProvider.Combine(classDeclarationsWithAttribute.Collect());

		// Register a source generator that operates on the collected class declarations
		context.RegisterSourceOutput(compilation, Execute);
	}

	private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right) tuple)
	{
		var (compilations, classDeclarations) = tuple;

		foreach (var classDeclarationSyntax in classDeclarations)
		{
			var originalName = classDeclarationSyntax.Identifier.ToString();
			var attributes = classDeclarationSyntax.AttributeLists.SelectMany(a => a.Attributes);
			var hasGenerateAttribute = attributes.FirstOrDefault(a => a.Name.ToString() == nameof(GenerateDto));

			var arguments = GetAttributeArguments(hasGenerateAttribute);
			var useDynamic = ExtractBool(arguments);
			if (!arguments.Any())
			{
				arguments.Add($"{originalName}DTO");
			}

			foreach (var className in arguments)
			{
				var ignoredProperties = GetIgnoredProperties(classDeclarationSyntax, className);
				var getClassWithoutIgnoredProperties = classDeclarationSyntax.RemoveNodes(ignoredProperties, SyntaxRemoveOptions.KeepEndOfLine);
				if (getClassWithoutIgnoredProperties == null)
				{
					continue;
				}
				var properties = getClassWithoutIgnoredProperties.Members
							   .OfType<PropertyDeclarationSyntax>()
							   .Where(p => p is not null)
							   .Select(p => p.WithoutTrivia());

				var directives = classDeclarationSyntax.SyntaxTree.GetRoot().DescendantNodes()
					.OfType<UsingDirectiveSyntax>();
				var namespaces = classDeclarationSyntax.SyntaxTree.GetRoot().DescendantNodes()
					.OfType<NamespaceDeclarationSyntax>();

				var generatedClass = GenerateClass(originalName, className, namespaces, directives, properties, useDynamic);
				context.AddSource(className, SourceText.From(generatedClass, Encoding.UTF8));
			}
		}
	}
	private IEnumerable<SyntaxNode> GetIgnoredProperties(ClassDeclarationSyntax declaration, string className)
	{
		var nodes = declaration.ChildNodes()
			.OfType<PropertyDeclarationSyntax>()
			.Where(p => p.AttributeLists.SelectMany(a => a.Attributes)
				.Any(a => a.Name.ToString() == nameof(ExcludeProperty) &&
						  (!GetAttributeArguments(a).Any() || GetAttributeArguments(a).Contains(className))));

		return nodes;
	}

	private AttributeSyntax GetUsingExistingAttribute(PropertyDeclarationSyntax property)
	{
		return property.AttributeLists
			.SelectMany(a => a.Attributes)
			.FirstOrDefault(a => a.Name.ToString() == nameof(UseExistingDto));
	}

	private List<string> GetAttributeArguments(AttributeSyntax attribute)
	{
		if (attribute.ArgumentList == null)
		{
			return new List<string>();
		}

		var arguments = attribute.ArgumentList.Arguments
			.Select(s => s.NormalizeWhitespace().ToFullString().Replace("\"", "")).ToList();

		return arguments;
	}

	private bool ExtractBool(List<string> arguments)
	{
		if (arguments.Any() && bool.TryParse(arguments.First(), out var parsedValue))
		{
			arguments.RemoveAt(0);
			return parsedValue;
		}
		return false;
	}

	private string GetUsingArgument(AttributeSyntax usingSyntax, string className)
	{
		var argument = GetAttributeArguments(usingSyntax)
			.Where(u => u.StartsWith(className) && u.Contains(" > "));
		return argument.FirstOrDefault()?.Split('>')[1];
	}
	
	private string GenerateClass(string originalName, string className, IEnumerable<NamespaceDeclarationSyntax> namespaces, IEnumerable<UsingDirectiveSyntax> usingDirectives,
        IEnumerable<PropertyDeclarationSyntax> properties, bool useDynamic)

    {
        var usings = new List<UsingDirectiveSyntax>
        {
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Dynamic")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections")),
            SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("BugTracker.Shared.Models"))
        };

        usings.AddRange(namespaces.Select(nd => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(nd.Name.ToString()))));

        usings.AddRange(usingDirectives);

        var classDeclaration = SyntaxFactory.ClassDeclaration(className)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

        foreach (var property in properties)
        {
            var useExisting = GetUsingExistingAttribute(property);
            var propertyDeclaration = useExisting == null
                ? property
                : SyntaxFactory.PropertyDeclaration(
                    SyntaxFactory.ParseTypeName(GetUsingArgument(useExisting, className) ?? $"{property.Type}DTO"),
                    property.Identifier)
                .WithModifiers(property.Modifiers)
                .WithAccessorList(property.AccessorList);

            classDeclaration = classDeclaration.AddMembers(propertyDeclaration);
        }

        var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("BugTracker.Shared.Models"))
            .AddMembers(classDeclaration);

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .AddUsings([.. usings])
            .AddMembers(namespaceDeclaration)
            .NormalizeWhitespace();

        return compilationUnit.ToString();
    }
}