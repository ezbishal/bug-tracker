using FluentValidation;
using Server.Models;

namespace Server.Areas.Projects.Validators;

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
