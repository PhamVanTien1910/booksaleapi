using BookSale.Api.Params;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Services.Admin.Book;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.Admin.Books
{
    [Route("v1")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("admin/books")]
        public async Task<IActionResult> CreateBooksAsync([FromBody] BookRequestDto bookRequestDto)
        {
            var bookDto = await _bookService.CreateBookAsync(bookRequestDto);
            return StatusCode(201, bookDto);
        }

        [HttpGet("books")]
        public async Task<IActionResult> PageBooksAsync(PaginationParams param)
        {
            var bookDto = await _bookService.GetPaginatedBookAsync(int.Parse(param.Page), int.Parse(param.PageSize));
            return StatusCode(200, bookDto);
        }

        [HttpDelete("admin/books/{id}")]
        public async Task<IActionResult> DeleteBooksAsync(BookIdParam param)
        {
            await _bookService.DaleteBookAsync(int.Parse(param.Id));
            return NoContent();
        }

        [HttpPut("admin/books/{id}")]
        public async Task<IActionResult> DeleteBooksAsync(BookIdParam param, [FromBody] BookRequestDto bookRequestDto)
        {
             var bookDto = await _bookService.UpdateBookAsync(int.Parse(param.Id), bookRequestDto);
            return StatusCode(200, bookDto);
        }
    }
}
