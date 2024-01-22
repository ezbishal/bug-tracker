using FluentValidation;
using server.Models;

namespace server.Areas.Projects.Validators;

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
