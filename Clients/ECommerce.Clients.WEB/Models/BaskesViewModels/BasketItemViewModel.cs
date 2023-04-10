using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommerce.Clients.WEB.Models.BaskesViewModels
{
    public class BasketItemViewModel
    {
        public int Quantity { get; } = 1;
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }

        private decimal? DiscountAppliedPrice { get; set; }

        public decimal GetCurrentPrice { get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price; }

        public void AppliedDiscount(decimal discountAppliedPrice)
        {
            DiscountAppliedPrice = discountAppliedPrice;
        }
    }
}
