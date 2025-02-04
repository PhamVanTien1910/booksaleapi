using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos
{
    public class UpdateAuthorsDto
    {
        public string Name { get; set; } = null!;
        public string Bio { get; set; } = null;
    }
}
