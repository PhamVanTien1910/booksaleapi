using BookSale.Api.Params;
using BookSale.Application.Dtos;
using BookSale.Application.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.User
{

    [ApiController]
    [Route("v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsers(PaginationParams paginationParams)
        {
            var users = await _userService.getPaginationUserAsync(int.Parse(paginationParams.Page), int.Parse(paginationParams.PageSize));
            return Ok(users);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var updatedUser = await _userService.UpdateUserByIdAsysc(updateUserDto);
            return Ok(updatedUser);
        }
    }
}
