using BugTracker.Shared.Generator.Attributes;
using Microsoft.CodeAnalysis;
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

                // Filter out null values from the sequences
                var properties = getClassWithoutIgnoredProperties.ChildNodes()
                    .OfType<PropertyDeclarationSyntax>();
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
        var classBuilder = new StringBuilder();

        classBuilder.AppendLine("using System.Dynamic;");
        classBuilder.AppendLine("using System.Collections;");
        foreach (var namespaceDirective in namespaces)
        {
            classBuilder.AppendLine($"using {namespaceDirective.Name.ToString()};");
        }

        foreach (var usingDirective in usingDirectives)
        {
            classBuilder.AppendLine(usingDirective.ToString());
        }

        classBuilder.AppendLine($@"
                namespace BugTracker.Shared.Models
                {{
                    public class {className}
                    {{");

        foreach (var property in properties)
        {
            var useExisting = GetUsingExistingAttribute(property);
            if (useExisting == null)
            {
                classBuilder.AppendLine($"\t\t{property}");
            }
            else
            {
                var replace = GetUsingArgument(useExisting, className);
                var dto = property.ToString().GetLastPart("]")
                    .ReplaceFirst(property.Type.ToString(), replace == null ? $"{property.Type}DTO" : replace);

                classBuilder.AppendLine($"\t\t{dto}");
            }
        }

        var param = useDynamic ? "dynamic" : originalName;
        classBuilder.AppendLine($@"
        public {className} Map({param} instance)
        {{");
        foreach (var property in properties)
        {
            var useExisting = GetUsingExistingAttribute(property);
            if (useExisting == null)
            {
                classBuilder.AppendLine($"\t\t\t{property.Identifier} = instance.{property.Identifier};");
            }
            else
            {
                var replace = GetUsingArgument(useExisting, className);
                var name = replace == null ? $"{property.Type}DTO" : replace;
                classBuilder.AppendLine($"\t\t\t{property.Identifier} = new {name}().Map(instance.{property.Identifier});");
            }
        }

        classBuilder.AppendLine("\t\t\treturn this;");

        classBuilder.AppendLine("\t\t}");
        classBuilder.AppendLine("    }");
        return classBuilder.AppendLine("}").ToString();
    }
}