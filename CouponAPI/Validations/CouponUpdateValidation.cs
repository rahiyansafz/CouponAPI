namespace CouponAPI.Validations;

public class CouponUpdateValidation : AbstractValidator<CouponUpdateDTO>
{
    public CouponUpdateValidation()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Percent).InclusiveBetween(1, 100);
    }
}
