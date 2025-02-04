using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Mapper
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile() 
        {
            CreateMap<Role, RoleDto>().ReverseMap();
           
        }
    }
}
