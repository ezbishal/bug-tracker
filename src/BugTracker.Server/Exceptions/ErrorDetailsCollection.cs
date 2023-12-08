namespace BugTrackerApi.Exceptions;

public class ErrorDetailsCollection
{
    private readonly List<ErrorDetails> _errors;

    public bool HasErrors => _errors.Count > 0;

    public IEnumerable<ErrorDetails> Errors => _errors;

    public ErrorDetailsCollection()
    {
        _errors = new List<ErrorDetails>();
    }

    public void AddErrorDetails(string errorMessage, string? errorCode = null, string? propertyName = null)
    {
        _errors.Add(new ErrorDetails(errorMessage, errorCode, propertyName));
    }
}
