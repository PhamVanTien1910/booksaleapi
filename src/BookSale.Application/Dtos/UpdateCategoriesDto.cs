using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Dtos
{
    public class UpdateCategoriesDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null;
    }
}
