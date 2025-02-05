using BookSale.Application.Dtos.Request;
using BookSale.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Carts
{
    public interface ICartService
    {
        Task<CartItemResponseDto> CreateAsync(CartsRequest cartsRequest);
        Task<CartResponseDto> GetAsync();
    }
}
