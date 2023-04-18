namespace ECommerce.Clients.WEB.Models.OrderViewModels
{
    public class CreateOrderItemViewModel
    {
        public string ProductId { get;  set; }
        public string ProductName { get;  set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get;  set; }
    }
}
