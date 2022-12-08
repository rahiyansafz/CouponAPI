namespace CouponAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                Name = "10OFF",
                Percent = 10,
                IsActive = true
            },
            new Coupon
            {
                Id = 2,
                Name = "20OFF",
                Percent = 20,
                IsActive = false
            },
            new Coupon
            {
                Id = 3,
                Name = "30OFF",
                Percent = 30,
                IsActive = false
            },
            new Coupon
            {
                Id = 4,
                Name = "50OFF",
                Percent = 50,
                IsActive = false
            }
        );
    }
}
