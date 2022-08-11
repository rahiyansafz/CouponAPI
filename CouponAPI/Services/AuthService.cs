using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CouponAPI.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _ctx;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private string secretKey;

    public AuthService(DataContext ctx, IMapper mapper, IConfiguration config)
    {
        _ctx = ctx;
        _mapper = mapper;
        _config = config;
        secretKey = _config.GetValue<string>("ApiSettings:Secret");
    }

    public bool IsUniqueUser(string username)
    {
        var user = _ctx.Users.FirstOrDefault(x => x.UserName == username);

        if (user is null) return true;

        return false;
    }

    public async Task<LoginResponse> Login(LoginUserDTO user)
    {
        var userLog = _ctx.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

        if (userLog is null) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userLog.UserName),
                new Claim(ClaimTypes.Role, userLog.Role),
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        LoginResponse logResponse = new()
        {
            User = _mapper.Map<UserDTO>(userLog),
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
        return logResponse;
    }

    public async Task<UserDTO> Register(RegUserDTO user)
    {
        User userReg = new()
        {
            UserName = user.UserName,
            Password = user.Password,
            Name = user.Name,
            Role = "Admin"
        };

        _ctx.Users.Add(userReg);
        _ctx.SaveChanges();
        userReg.Password = "";
        return _mapper.Map<UserDTO>(userReg);
    }
}
