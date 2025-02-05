using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos.Request
{
    public class CartsRequest
    {
        [JsonPropertyName("book_id")]
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
