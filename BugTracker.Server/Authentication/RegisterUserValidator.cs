using BugTracker.Shared.Models;
using FluentValidation;

namespace BugTracker.Server.Authentication;

public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
{
    public RegisterUserValidator()
    {

    }
}
