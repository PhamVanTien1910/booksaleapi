using dotnet_boilerplate.Domain.Common;
using dotnet_boilerplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Entities
{
    public class Review : IntergerIdTrackable
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; } = 0;
        public string Comment { get; set; }

    }
}
