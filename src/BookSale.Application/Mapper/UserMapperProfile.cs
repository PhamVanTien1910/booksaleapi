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
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegistrationDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, AdminUpdateUserDto>().ReverseMap();
        }

    }
}
