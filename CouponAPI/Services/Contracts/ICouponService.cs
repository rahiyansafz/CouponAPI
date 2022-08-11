namespace CouponAPI.Services.Contracts;

public interface ICouponService
{
    Task<ICollection<Coupon>> GetCoupons();
    Task<Coupon> GetCoupon(int id);
    Task<Coupon> GetCoupon(string couponName);
    Task CreateCoupon(Coupon coupon);
    Task UpdateCoupon(Coupon coupon);
    Task RemoveCoupon(Coupon coupon);
    Task Save();
}
