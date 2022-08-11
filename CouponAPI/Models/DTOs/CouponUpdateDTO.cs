namespace CouponAPI.Models.DTOs;

public class CouponUpdateDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Percent { get; set; }
    public bool IsActive { get; set; }
}
