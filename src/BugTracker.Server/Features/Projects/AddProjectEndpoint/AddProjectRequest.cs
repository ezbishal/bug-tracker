using BugTrackerApi.Models.Projects;
using FluentValidation;

namespace BugTrackerApi.Features.Projects.AddProjectEndpoint;

public class AddProjectRequest : ProjectDtoBase
{

}

public class AddProjectRequestValidator : AbstractValidator<AddProjectRequest>
{
	public AddProjectRequestValidator()
	{
		RuleFor(p => p.Name)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3);
			
	}
}
