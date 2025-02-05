using BookSale.Application.Dtos.Request;
using BookSale.Application.Services.Carts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.Cart
{
    [Route("v1/api")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) 
        { 
            _cartService = cartService;
        }

        [HttpPost("/cart")]
        public async Task<IActionResult> CreateCartAsync([FromBody] CartsRequest cartsRequest)
        {
            var cartDto = await _cartService.CreateAsync(cartsRequest);
            return StatusCode(201, cartDto);
        }

        [HttpGet("/cart")]
        public async Task<IActionResult> GetCartAsync()
        {
            var cartDto = await _cartService.GetAsync();
            return StatusCode(200, cartDto);
        }
    }
}
