using MyAppApplication.Shared.Contracts;
using MyAppDomain.Constatns;
using MyAppDomain.Helper;
using ValidationException = MyAppApplication.Shared.Exceptions.ValidationException;

namespace MyAppPresentaionAPI.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILocalizationService localizer)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ServiceResponse()
            {
                Code = StatusCodes.Status400BadRequest,
                Error = null,
                Message = localizer.GetLocalizedString(LocalizeKeys.ValidationErrorOccurred),
                ValidationError = exception.Errors,
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (InvalidOperationException exception)
        {
            await context.Response.WriteAsync("Something went wrong, InvalidOperationException Exception");
        }
        catch (Exception exception)
        {
            await context.Response.WriteAsync("Something went wrong, Unknown Exception");
        }
    }
}