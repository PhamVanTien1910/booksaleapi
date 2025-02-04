using dotnet_boilerplate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Entities
{
    public class PaymentMethod : IntergerIdTrackable
    {
        public string Name { get; set; }
    }
}
