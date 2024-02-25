using FluentValidation;

namespace Server.Authentication;

public class RegisterUserModel
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}

public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
{
	public RegisterUserValidator()
	{
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
	}
}
