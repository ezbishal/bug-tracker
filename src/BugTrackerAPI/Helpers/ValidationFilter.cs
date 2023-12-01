using BugTrackerApi.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace BugTrackerApi.ValidationFilter;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is not null)
        {
            var entity = context.Arguments
                .OfType<T>()
                .FirstOrDefault();
            if (entity is not null)
            {
                ValidationResult? validationResult = await validator.ValidateAsync(entity);

                if (validationResult.IsValid)
                {
                    return await next(context);
                }

                ErrorDetailsCollection errorDetailsCollection = new();
                foreach (var item in validationResult.Errors)
                {
                    errorDetailsCollection.AddErrorDetails(item.ErrorMessage, item.ErrorCode, item.PropertyName);
                }

                throw new InputValidationException(errorDetailsCollection);
            }
            else
            {
                return Results.Problem("Could not find the type to validate");
            }
        }
        return await next(context);
    }
}
