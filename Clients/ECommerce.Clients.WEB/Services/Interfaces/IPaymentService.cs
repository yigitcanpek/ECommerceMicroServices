using ECommerce.Clients.WEB.Models.FakePaymentViewModels;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoViewModel paymentInfoViewModel);
    }
}
