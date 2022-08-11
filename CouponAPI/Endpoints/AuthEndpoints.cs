namespace CouponAPI.Endpoints;

public static class AuthEndpoints
{
    public static void ConfigureAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/login", Login).WithName("Login").Accepts<LoginUserDTO>("application/json").Produces<APIResponse>(200).Produces(400);

        app.MapPost("/api/register", Register).WithName("Register").Accepts<RegUserDTO>("application/json").Produces<APIResponse>(200).Produces(400);
    }

    private async static Task<IResult> Register(IAuthService _authService, [FromBody] RegUserDTO model)
    {
        APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

        bool ifUserNameUnique = _authService.IsUniqueUser(model.UserName);

        if (!ifUserNameUnique)
        {
            response.ErrorMessages.Add("User already exists!");
            return Results.BadRequest(response);
        }

        var regResponse = await _authService.Register(model);

        if (regResponse is null || string.IsNullOrEmpty(regResponse.UserName))
        {
            //response.ErrorMessages.Add("Something went wrong!");
            return Results.BadRequest(response);
        }

        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }

    private async static Task<IResult> Login(IAuthService _authService, [FromBody] LoginUserDTO model)
    {
        APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
        var loginResponse = await _authService.Login(model);

        if (loginResponse is null)
        {
            response.ErrorMessages.Add("Username or Password is incorrect!");
            return Results.BadRequest(response);
        }

        response.Result = loginResponse;
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
}
