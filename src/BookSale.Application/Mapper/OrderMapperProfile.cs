using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Mapper
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile() 
        {
            CreateMap<Order, PaymentRequest>().ReverseMap();
        }
    }
}
