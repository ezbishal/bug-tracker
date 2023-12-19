using BugTracker.Shared.UserModels;
using FluentValidation;

namespace BugTracker.Server.Authentication;

public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
{
    public RegisterUserValidator()
    {

    }
}
