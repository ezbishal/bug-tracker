using BugTracker.Shared.Models;
using FluentValidation;

namespace BugTracker.Server.Features.Projects.Validators;

public class UpdateProjectValidator : AbstractValidator<UpdateProjectModel>
{
    public UpdateProjectValidator()
    {

    }
}
