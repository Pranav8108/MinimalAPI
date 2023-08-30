using FluentValidation;
using MagicVilla_CouponAPI.Models.dto;

namespace MagicVilla_CouponAPI.Validations
{
    public class CouponCreateValidation : AbstractValidator<CouposDto> //from fluentapi nuget
    {
        public CouponCreateValidation()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Pecentage).InclusiveBetween(1, 100);
        }
    }
}
