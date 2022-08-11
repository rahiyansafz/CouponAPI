namespace CouponAPI.Services.Contracts;

public interface IAuthService
{
    bool IsUniqueUser(string username);
    Task<LoginResponse> Login(LoginUserDTO user);
    Task<UserDTO> Register(RegUserDTO user);
}
