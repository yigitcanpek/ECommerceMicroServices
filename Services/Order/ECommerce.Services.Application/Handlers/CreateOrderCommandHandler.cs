
using ECommerce.Serivces.Order.Infrastructure;
using ECommerce.Services.Order.Application.Commands;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Domain.OrderAggregate;
using ECommerce.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Address newAdress = new Address(request.AddressDto.Province, request.AddressDto.Line, request.AddressDto.Street, request.AddressDto.District, request.AddressDto.ZipCode);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyyerId, newAdress);
            request.OrderItemsDto.ForEach(x => { newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl); });
            _context.Orders.AddAsync(newOrder);
             await _context.SaveChangesAsync();
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id },200);
        }
    }
}
