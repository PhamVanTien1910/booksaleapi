using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos
{
    public class UpdateUserDto
    {
        public string? Email { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
    }
}
