using BookSale.Api.Params;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Services.Admin.Authors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.Admin.Authors
{
    [Route("v1")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;
        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost("admin/authors")]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorsRequestDto authorsDto)
        {
            var authorDto =  await _authorsService.CreateAuthorAsync(authorsDto);
            return StatusCode(201, authorDto);
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthorAsync()
        {
           var author = await _authorsService.GetAllAuthorAsync();
            return StatusCode(200, author);
        }

        [HttpPut("admin/authors/{id}")]
        public async Task<IActionResult> UpdateAuthorAsync(AuthorIdParam param, [FromBody] UpdateAuthorsDto authorsDto)
        {
            var author = await _authorsService.UpdateAuthorAsync(int.Parse(param.Id), authorsDto);
            return StatusCode(200, author);
        }

        [HttpDelete("admin/authors/{id}")]
        public async Task<IActionResult> DeleteAuthorAsync(AuthorIdParam param)
        {
            await _authorsService.DeleteAuthorByIdAsync(int.Parse(param.Id));
            return NoContent();
        }
    }
}
