using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin.Authors
{
    public interface IAuthorsService
    {
        Task<AuthorsResponseDto> CreateAuthorAsync(AuthorsRequestDto authors);
        Task<AuthorsResponseDto> UpdateAuthorAsync(int id, UpdateAuthorsDto authors);
        Task DeleteAuthorByIdAsync(int id);
        Task<List<AuthorsResponseDto>> GetAllAuthorAsync();
        
    }
}
