using AutoMapper;
using ECommerce.Services.Order.Application.Dtos;
using ECommerce.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Order.Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItemDto,OrderItemDto>().ReverseMap();
            CreateMap<Address, OrderDto>().ReverseMap();
        }
    }
}
