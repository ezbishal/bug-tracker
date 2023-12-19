using BugTracker.Shared.Models;
using FluentValidation;

namespace BugTracker.Server.Features.Projects.Validators;

public class CreateProjectValidator : AbstractValidator<ProjectModel>
{
    public CreateProjectValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);

    }
}
