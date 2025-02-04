using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos.Request
{
    public class AuthorsRequestDto
    {
        public string Name { get; set; } = null!;
        public string Bio { get; set; } = null;
    }
}
