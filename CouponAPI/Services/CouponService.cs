using CouponAPI.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CouponAPI.Services;

public class CouponService : ICouponService
{
    private readonly DataContext _ctx;
    public CouponService(DataContext ctx) => _ctx = ctx;

    public async Task CreateCoupon(Coupon coupon) => await _ctx.AddAsync(coupon);

    public async Task<Coupon> GetCoupon(int id) => await _ctx.Coupons.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Coupon> GetCoupon(string couponName) => await _ctx.Coupons.FirstOrDefaultAsync(c => c.Name.ToLower() == couponName.ToLower());

    public async Task<ICollection<Coupon>> GetCoupons() => await _ctx.Coupons.ToListAsync();

    public async Task RemoveCoupon(Coupon coupon) => _ctx.Coupons.Remove(coupon);

    public async Task Save() => await _ctx.SaveChangesAsync();

    public async Task UpdateCoupon(Coupon coupon)
    {
        _ctx.Coupons.Update(coupon);
    }
}
