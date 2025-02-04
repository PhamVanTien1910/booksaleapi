using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos
{
    public class AdminUpdateUserDto
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }
    }
}
