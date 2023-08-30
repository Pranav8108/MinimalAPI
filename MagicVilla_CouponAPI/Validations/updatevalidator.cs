using FluentValidation;
using MagicVilla_CouponAPI.Models.dto;

namespace MagicVilla_CouponAPI.Validations
{
    public class UpdateValidator : AbstractValidator<UpdateDto> //from fluentapi nuget
    {
        public UpdateValidator()
        {
            RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Pecentage).InclusiveBetween(1, 100);
        }
    }
}

