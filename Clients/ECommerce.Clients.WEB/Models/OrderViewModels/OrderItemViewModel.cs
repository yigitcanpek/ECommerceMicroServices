namespace ECommerce.Clients.WEB.Models.OrderViewModels
{
    public class OrderItemViewModel
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUrl { get; private set; }
        public Decimal Price { get; private set; }
    }
}
