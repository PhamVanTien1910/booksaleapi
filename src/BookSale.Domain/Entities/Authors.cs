using dotnet_boilerplate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Entities
{
    public class Authors : IntergerIdTrackable
    {
        public string Name { get; set; } = null!;
        public string Bio { get; set; } = null;
        public ICollection<Book> Books { get; set; }
    }
}
