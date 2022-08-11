using Microsoft.AspNetCore.Authorization;

namespace CouponAPI.Endpoints;

public static class CouponEndpoints
{
    public static void ConfigureCouponEndpoints(this WebApplication app)
    {
        app.MapGet("/api/coupon", GetCoupons).WithName("GetCoupons").Produces<APIResponse>(200);

        app.MapGet("/api/coupon/{id:int}", GetCoupon).WithName("GetCoupon").Produces<APIResponse>(200);

        app.MapPost("/api/coupon", CreateCoupon).WithName("CreateCoupon").Accepts<CouponCreateDTO>("application/json").Produces<APIResponse>(201).Produces(400).RequireAuthorization("AdminOnly");

        app.MapPut("/api/coupon", UpdateCoupon).WithName("UpdateCoupon").Accepts<CouponUpdateDTO>("application/json").Produces<APIResponse>(200).Produces(400).RequireAuthorization("AdminOnly");

        app.MapDelete("/api/coupon/{id:int}", DeleteCoupon).RequireAuthorization("AdminOnly");
    }

    private async static Task<IResult> GetCoupons(ICouponService _couponService, ILogger<Program> _logger)
    {
        APIResponse response = new();
        _logger.Log(LogLevel.Information, "Getting all Coupons!");
        response.Result = await _couponService.GetCoupons();
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
    private async static Task<IResult> GetCoupon(ICouponService _couponService, ILogger<Program> _logger, int id)
    {
        APIResponse response = new();
        _logger.Log(LogLevel.Information, "Getting Single Coupon!");
        response.Result = await _couponService.GetCoupon(id);
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
    [Authorize(Roles = "AdminOnly")]
    private async static Task<IResult> CreateCoupon(ICouponService _couponService, IMapper _mapper, IValidator<CouponCreateDTO> _validation, [FromBody] CouponCreateDTO coupon)
    {
        APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

        var validationResult = await _validation.ValidateAsync(coupon);

        if (!validationResult.IsValid)
        {
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
            return Results.BadRequest(response);
        }

        if (await _couponService.GetCoupon(coupon.Name) != null)
        {
            response.ErrorMessages.Add("Coupon Name already exists!");
            return Results.BadRequest(response);
        }

        Coupon mappedCoupon = _mapper.Map<Coupon>(coupon);

        await _couponService.CreateCoupon(mappedCoupon);
        await _couponService.Save();

        CouponDTO convertedCoupon = _mapper.Map<CouponDTO>(mappedCoupon);

        response.Result = convertedCoupon;
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.Created;
        return Results.Ok(response);

        //return Results.CreatedAtRoute("GetCoupon", new { id = mappedCoupon.Id }, convertedCoupon);
    }
    [Authorize(Roles = "AdminOnly")]
    private async static Task<IResult> UpdateCoupon(ICouponService _couponService, IMapper _mapper, IValidator<CouponUpdateDTO> _validation, [FromBody] CouponUpdateDTO coupon)
    {
        APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

        var validationResult = await _validation.ValidateAsync(coupon);

        if (!validationResult.IsValid)
        {
            response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
            return Results.BadRequest(response);
        }

        //Coupon retrieveCoupon = await _couponService.GetCoupon(coupon.Id);
        //retrieveCoupon.IsActive = coupon.IsActive;
        //retrieveCoupon.Name = coupon.Name;
        //retrieveCoupon.Percent = coupon.Percent;
        //retrieveCoupon.LastUpdated = DateTime.Now;

        await _couponService.UpdateCoupon(_mapper.Map<Coupon>(coupon));

        await _couponService.Save();

        response.Result = _mapper.Map<CouponDTO>(await _couponService.GetCoupon(coupon.Id));
        response.IsSuccess = true;
        response.StatusCode = HttpStatusCode.OK;
        return Results.Ok(response);
    }
    [Authorize(Roles = "AdminOnly")]
    private async static Task<IResult> DeleteCoupon(ICouponService _couponService, int id)
    {
        APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

        Coupon coupon = await _couponService.GetCoupon(id);
        if (coupon is not null)
        {
            await _couponService.RemoveCoupon(coupon);
            await _couponService.Save();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.NoContent;
            return Results.Ok(response);
        }
        else
        {
            response.ErrorMessages.Add("Invalid Id");
            return Results.BadRequest(response);
        }
    }
}
