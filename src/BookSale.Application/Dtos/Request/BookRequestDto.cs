using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos.Request
{
    public class BookRequestDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stock { get; set; }
        [JsonPropertyName("authors_id")]
        public int AuthorsId { get; set; }
        [JsonPropertyName("categorys_id")]
        public int CategorysId { get; set; }
        public decimal Price { get; set; }
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; } = null!;
    }
}
