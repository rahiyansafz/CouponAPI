namespace CouponAPI.Models.DTOs;

public class CouponCreateDTO
{
    public string Name { get; set; } = default!;
    public int Percent { get; set; }
    public bool IsActive { get; set; }
}
