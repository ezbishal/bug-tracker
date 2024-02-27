using FluentValidation;
using Server.Models;

namespace Server.Areas.Projects.Validators;

public class UpdateProjectValidator : AbstractValidator<BugModel>
{
    public UpdateProjectValidator()
    {
    }
}