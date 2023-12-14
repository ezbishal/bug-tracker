using BugTracker.Shared.Models;
using FluentValidation;

namespace BugTracker.Server.Features.Projects.Validators;

public class AddProjectValidator : AbstractValidator<AddProjectModel>
{
    public AddProjectValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3);

    }
}
