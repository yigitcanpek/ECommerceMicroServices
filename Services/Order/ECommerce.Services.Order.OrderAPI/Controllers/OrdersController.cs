using ECommerce.Services.Order.Application.Commands;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Application.Queries;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using ECommerce.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Order.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;

        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            Response<List<OrderDto>> response = await _mediator.Send(new GetOrderByUserIdQuery { UserId=_sharedIdentityService.GetUserId});
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            
            Response<CreatedOrderDto> response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }
    }
}
