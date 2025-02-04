using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Domain.Payloads
{
    public class TokenPayLoad
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("access_token")]
        public string? Access {  get; set; }

        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("refresh_token")]
        public string? Refresh {  get; set; }
    }
}
