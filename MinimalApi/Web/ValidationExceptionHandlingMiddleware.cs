using System.Text.Json;

namespace MinimalApi.Web;

public class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors
                .Select(err => new { field = err.PropertyName, error = err.ErrorMessage });

            var response = JsonSerializer.Serialize(new
            {
                message = "Ошибка валидации",
                errors
            });

            await context.Response.WriteAsync(response);
        }
    }
}