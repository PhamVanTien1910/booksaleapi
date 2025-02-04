using AutoMapper;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Mapper
{
    public class AuthorMapperProfile : Profile
    {
        public AuthorMapperProfile() 
        {
            CreateMap<AuthorsRequestDto, Authors>();

            CreateMap<Authors, AuthorsResponseDto>();

            CreateMap<Authors, UpdateAuthorsDto>().ReverseMap();
        }
    }
}
