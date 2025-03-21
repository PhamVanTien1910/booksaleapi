﻿using AutoMapper;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Dtos.Response;
using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Mapper
{
    public class CartMapperProfile : Profile
    {
        public CartMapperProfile() 
        {
            CreateMap<CartsRequest, Cart>();
            CreateMap<Cart, CartItemResponseDto>();
        }
    }
}
