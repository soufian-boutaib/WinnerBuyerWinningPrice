using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Auction.Configuration
{
    public class AuctionSettings
    {
        public int ReservePrice { get; set; }
        public List<Buyer> Buyers { get; set; }
    }

 
    public class Buyer
    {
        public string Name { get; set; }
        public List<int> Bids { get; set; }
    }
}