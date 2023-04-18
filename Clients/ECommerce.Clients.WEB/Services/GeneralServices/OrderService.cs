using ECommerce.Clients.WEB.Models.BaskesViewModels;
using ECommerce.Clients.WEB.Models.FakePaymentViewModels;
using ECommerce.Clients.WEB.Models.OrderViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;
using ECommerce.Shared.Services;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
        }

        public async Task<OrderStatusViewModel> CreateOrder(CheckOutViewModel checkOutViewModel)
        {
            BasketViewModel basket = await _basketService.Get();
            PaymentInfoViewModel payment = new PaymentInfoViewModel()
            {
                CardName= checkOutViewModel.CardName,
                CardNumber= checkOutViewModel.CardNumber,
                Expiration= checkOutViewModel.Expiration,
                CVV= checkOutViewModel.CVV,
                TotalPrice=basket.TotalPrice,
            };

            bool responsePayment = await _paymentService.ReceivePayment(payment);

            if (!responsePayment)
            {
                return new OrderStatusViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }


            CreateOrderViewModel createOrderViewModel = new CreateOrderViewModel()
            {
                BuyyerId = _sharedIdentityService.GetUserId,
                AddressDto = new CreateOrderAdressViewModel { Province = checkOutViewModel.Province, District = checkOutViewModel.District, Line = checkOutViewModel.Line, Street = checkOutViewModel.Street, ZipCode = checkOutViewModel.ZipCode },
            };
            basket.basketItems.ForEach(basketItem => 
            {
                CreateOrderItemViewModel orderItemViewModel = new CreateOrderItemViewModel { ProductId = basketItem.CourseId, Price = basketItem.GetCurrentPrice, ProductName = basketItem.CourseName };
                createOrderViewModel.CreateOrderItemViewModels.Add(orderItemViewModel);
            });


            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<CreateOrderViewModel>("orders",createOrderViewModel);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderStatusViewModel() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };
            }

            Response<OrderStatusViewModel> orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderStatusViewModel>>();

            orderCreatedViewModel.Data.IsSuccessful = true;
            _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

            
        

        public async Task<List<OrderViewModel>> GetOrder()
        {
            Response<List<OrderViewModel>> response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public Task SuspendOrderAsync(CheckOutViewModel checkOutViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
