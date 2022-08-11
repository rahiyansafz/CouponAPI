namespace CouponAPI.Models;

public class LoginResponse
{
    public UserDTO User { get; set; }
    public string Token { get; set; }
}
