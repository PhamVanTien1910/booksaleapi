using BookSale.Api.Params;
using BookSale.Application.Dtos;
using BookSale.Application.Services.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.Admin
{
    [Route("v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUserByIdAsync(UserIdParam param)
        {
            await _adminService.DeleteUserByIdAsync(int.Parse(param.Id));
            return NoContent();
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUserByIdAsync(UserIdParam param, [FromBody] AdminUpdateUserDto updateUserDto)
        {
            var user = await _adminService.UpdateUserByIdAsync(int.Parse(param.Id), updateUserDto);
            return Ok(user);
        }
    }
}
