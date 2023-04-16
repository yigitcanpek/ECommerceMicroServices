using ECommerce.Clients.WEB.Models.OrderViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class OrderService : IOrderService
    {
        public Task<OrderStatusViewModel> CreateOrder(CheckOutViewModel checkOutViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel> GetOrder()
        {
            throw new NotImplementedException();
        }

        public Task SuspendOrderAsync(CheckOutViewModel checkOutViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
