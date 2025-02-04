using BookSale.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin
{
    public interface IAdminService
    {
        Task DeleteUserByIdAsync(int id);
        Task<UserDto> UpdateUserByIdAsync(int id, AdminUpdateUserDto updateUserDto);
    }
}
