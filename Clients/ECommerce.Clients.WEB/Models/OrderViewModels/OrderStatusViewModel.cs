namespace ECommerce.Clients.WEB.Models.OrderViewModels
{
    public class OrderStatusViewModel
    {
        public string OrderId { get; set; }

        public string Error { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
