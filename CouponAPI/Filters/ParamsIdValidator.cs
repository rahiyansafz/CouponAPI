﻿//namespace CouponAPI.Filters;

//public class ParamsIdValidator : IRouteHandlerFilter
//{
//    private ILogger<ParameterIDValidator> _logger;
//    public ParameterIDValidator(ILogger<ParameterIDValidator> logger) => _logger = logger;

//    public async ValueTask<object> InvokeAsync(RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next)
//    {
//        var id = context.Arguments.SingleOrDefault(x => x?.GetType() == typeof(int)) as int?;
//        if (id is null || id == 0)
//        {
//            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
//            response.ErrorMessages.Add("Id cannot be zero.");
//            _logger.Log(LogLevel.Error, "ID cannot be 0");
//            return Results.BadRequest(response);
//        }
//        return await next(context);
//    }
//}
