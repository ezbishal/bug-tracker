using FluentValidation;
using System.Text.Json.Serialization;

namespace BugTrackerApi.Authentication.GetAuthTokenEndpoint;

public class GetAuthTokenRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}

public class GetAuthTokenRequestValidator : AbstractValidator<GetAuthTokenRequest>
{

}
