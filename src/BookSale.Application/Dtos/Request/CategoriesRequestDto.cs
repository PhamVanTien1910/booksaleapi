using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos.Request
{
    public class CategoriesRequestDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null;
    }
}
