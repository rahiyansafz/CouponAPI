namespace CouponAPI.Filters;

public class ParamsIdValidator : IEndpointFilter
{
    private ILogger<ParamsIdValidator> _logger;
    public ParamsIdValidator(ILogger<ParamsIdValidator> logger) => _logger = logger;

    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.Arguments.SingleOrDefault(x => x?.GetType() == typeof(int)) as int?;
        if (id is null || id == 0)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            response.ErrorMessages.Add("Id cannot be zero.");
            _logger.Log(LogLevel.Error, "ID cannot be 0");
            return Results.BadRequest(response);
        }
        return await next(context);
    }
}
