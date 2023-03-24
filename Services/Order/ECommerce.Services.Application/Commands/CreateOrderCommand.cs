using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<Response<CreatedOrderDto>>
    {
        public string BuyyerId { get; set; }

        public List<OrderItemDto> OrderItemsDto { get; set; }

        public AddressDto AddressDto { get; set; }
    }
}
