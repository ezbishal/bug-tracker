using FluentValidation;

namespace BugTrackerApi.Authentication.RegisterUserEndpoint;

public class RegisterUserRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterRequestValidator : AbstractValidator<RegisterUserRequest>
{

}