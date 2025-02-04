using BookSale.Application.Dtos.Request;
using BookSale.Application.Dtos.Response;
using BookSale.Domain.Entities;
using BookSale.Domain.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin.Book
{
    public interface IBookService
    {
       Task<BookResponseDto> CreateBookAsync(BookRequestDto bookRequestDto);
       Task DaleteBookAsync(int id);
       Task<PaginatedResult<BookResponseDto>> GetPaginatedBookAsync(int page, int pagesize);
       Task<BookResponseDto> UpdateBookAsync(int id, BookRequestDto bookRequestDto);
    }
}
