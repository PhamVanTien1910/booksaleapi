using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos
{
    public class RegistrationDto
    {
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        [JsonPropertyName("confirm_password")]
        public string? ConfirmPassword { get; set; }
    }
}
