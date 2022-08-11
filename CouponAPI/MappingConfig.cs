namespace CouponAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
        CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();
        CreateMap<Coupon, CouponDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
    }
}