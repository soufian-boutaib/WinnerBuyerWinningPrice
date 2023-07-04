using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Entities
{
    public class BuyerEntity
    {
        public string Name { get; set; }
        public List<int> Bids { get; set; }
    }
}
