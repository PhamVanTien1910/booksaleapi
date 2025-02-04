using BookSale.Application.Dtos;
using BookSale.Domain.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Users
{
    public interface IUserService
    {
        Task<UserDto?> GetMe();
        Task<PaginatedResult<UserDto>> getPaginationUserAsync(int page, int pageSize);
        Task<UserDto> UpdateUserByIdAsysc(UpdateUserDto updateUserDto);
    }
}
