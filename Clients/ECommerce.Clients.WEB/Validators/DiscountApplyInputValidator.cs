using ECommerce.Clients.WEB.Models.DiscountViewModels;
using FluentValidation;

namespace ECommerce.Clients.WEB.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Bu alan boş olamaz");
        }
    }
}
