namespace BugTrackerApi.Helpers;

public class ErrorDetails
{
    public string ErrorMessage { get; }

    public string ErrorCode { get; }

    public string PropertyName { get; }

    public ErrorDetails(string message, string? code = null, string? field = null)
    {
        ErrorMessage = message;
        ErrorCode = code;
        PropertyName = field;
    }
}
