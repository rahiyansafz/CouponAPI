namespace CouponAPI.Validations;

public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
{
    public CouponCreateValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Percent).InclusiveBetween(1, 100);
    }
}
