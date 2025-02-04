using dotnet_boilerplate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Entities
{
    public class Book : IntergerIdTrackable
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stock { get; set; }
        public int AuthorsId { get; set; }
        public Authors Authors { get; set; }
        public int CategorysId { get; set; }
        public Categories Categorys { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Cart> Carts { get; set; }

    }
}
