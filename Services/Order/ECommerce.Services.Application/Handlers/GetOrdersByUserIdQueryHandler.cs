using ECommerce.Serivces.Order.Infrastructure;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Application.Mapping;
using ECommerce.Services.Order.Application.Queries;
using ECommerce.Shared.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbContext;

        public GetOrdersByUserIdQueryHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Order.Domain.OrderAggregate.Order> orders = await _orderDbContext.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(),200);
            }
            List<OrderDto> ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return Response<List<OrderDto>>.Success(ordersDto,200);
        }
    }
}
