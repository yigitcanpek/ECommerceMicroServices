using ECommerce.Clients.WEB.Models.OrderViewModels;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IOrderService
    {
        //synchron (Directly to order microservice)
        Task<OrderStatusViewModel> CreateOrder(CheckOutViewModel checkOutViewModel);

        //Asynchron (To the rabbitmq)
        Task SuspendOrderAsync(CheckOutViewModel checkOutViewModel);

        Task<OrderViewModel> GetOrder();
            
    }
}
