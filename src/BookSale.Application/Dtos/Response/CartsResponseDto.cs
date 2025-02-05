using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos.Response
{
    public class CartItemResponseDto
    {
        public int Id { get; set; }

        [JsonPropertyName("book_name")]
        public string BookName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
    public class CartResponseDto
    {
        public int UserId { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public int TotalItems => Items.Sum(i => i.Quantity);
        public decimal TotalCartPrice => Items.Sum(i => i.TotalPrice);
        public string Currency { get; set; } = "VND";
    
    }
}
