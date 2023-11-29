using FluentValidation;

namespace BugTrackerAPI.Features.AddProject;
public class AddProjectValidator : AbstractValidator<AddProjectRequest>
{
    public AddProjectValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);
    }
}
