using BugTrackerApi.Models.Projects;
using FluentValidation;

namespace BugTrackerApi.Features.AddProject;
public class AddProjectValidator : AbstractValidator<AddProjectDto>
{
    public AddProjectValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);
    }
}
