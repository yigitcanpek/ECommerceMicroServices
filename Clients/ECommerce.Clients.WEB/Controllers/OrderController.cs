using ECommerce.Clients.WEB.Models.BaskesViewModels;
using ECommerce.Clients.WEB.Models.OrderViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Clients.WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            BasketViewModel basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckOutViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckOutViewModel checkOutViewModel)
        {
            OrderStatusViewModel ordertatus = await _orderService.CreateOrder(checkOutViewModel);
            if (!ordertatus.IsSuccessful)
            {
                BasketViewModel basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = ordertatus.Error;
                return RedirectToAction(nameof(Checkout));
            }
            return RedirectToAction(nameof(SuccessfulCheckout), new {orderId = ordertatus.OrderId});
        }

        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}
