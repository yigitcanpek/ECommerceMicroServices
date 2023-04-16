using ECommerce.Services.FakePayment.ViewModels;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.FakePayment.Controllers
{
    
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {

            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
