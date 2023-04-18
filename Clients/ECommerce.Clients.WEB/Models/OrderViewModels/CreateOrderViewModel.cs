namespace ECommerce.Clients.WEB.Models.OrderViewModels
{
    public class CreateOrderViewModel
    {
        public CreateOrderViewModel()
        {
            CreateOrderItemViewModels= new List<CreateOrderItemViewModel>();
        }
        public string BuyyerId { get; set; }

        public List<CreateOrderItemViewModel> CreateOrderItemViewModels { get; set; }

        public CreateOrderAdressViewModel AddressDto { get; set; }
    }
}
