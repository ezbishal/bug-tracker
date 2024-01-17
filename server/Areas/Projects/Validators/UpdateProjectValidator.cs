using FluentValidation;
using server.Models;

namespace server.Areas.Projects.Validators;

public class UpdateProjectValidator : AbstractValidator<ProjectModel>
{
    public UpdateProjectValidator()
    {

    }
}
