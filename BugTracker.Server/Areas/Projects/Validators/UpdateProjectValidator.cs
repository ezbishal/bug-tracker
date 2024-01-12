using BugTracker.Shared.Models;
using FluentValidation;

namespace BugTracker.Server.Areas.Projects.Validators;

public class UpdateProjectValidator : AbstractValidator<ProjectModel>
{
    public UpdateProjectValidator()
    {

    }
}
