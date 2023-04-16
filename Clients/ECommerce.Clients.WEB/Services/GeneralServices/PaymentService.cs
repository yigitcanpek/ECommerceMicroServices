using ECommerce.Clients.WEB.Models.FakePaymentViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpclient;

        public PaymentService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoViewModel paymentInfoViewModel)
        {
            HttpResponseMessage response = await _httpclient.PostAsJsonAsync<PaymentInfoViewModel>("fakepayments", paymentInfoViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}
