using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        public string? Email { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RoleDto? Role { get; set; }
    }
}
