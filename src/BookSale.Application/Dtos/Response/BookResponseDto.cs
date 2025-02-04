using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos.Response
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stock { get; set; }
        [JsonPropertyName("authors_name")]
        public string AuthorsName { get; set; }
        [JsonPropertyName("categorys_name")]
        public string CategorysName { get; set; }
        public decimal Price { get; set; }
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; } = null!;
    }
}
