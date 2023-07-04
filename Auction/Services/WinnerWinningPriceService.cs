using AuctionApp.Auction.Configuration;
using AuctionApp.Auction.Interfaces;
using AuctionApp.Auction.Models;
using AuctionApp.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Auction.Services
{
    public class WinnerWinningPriceService : IWinnerWinningPriceService
    {
        private readonly int _reservedPrice;
        private readonly List<BuyerEntity> _buyers;
        public WinnerWinningPriceService(AuctionSettings auctionSettingsOptions)
        {
            _reservedPrice = auctionSettingsOptions.ReservePrice;
            _buyers = auctionSettingsOptions.Buyers
                .Select(x => new BuyerEntity { Name = x.Name, Bids = x.Bids }).ToList();
        }

        public WinnerWinningPriceResult FindWinner()
        {
            BuyerEntity winner = null;
            int reservePrice = _reservedPrice;
            int winningPrice = reservePrice;
            List<int> nonWinningBids = new List<int>();

            foreach (var buyer in _buyers.Where(x => x.Bids?.Count > 0) )
            {
                if (buyer.Bids.Any(bid => bid >= reservePrice))
                {
                    if (winner == null || buyer.Bids.Max() > winner.Bids.Max())
                    {
                        nonWinningBids.AddRange(winner?.Bids ?? Enumerable.Empty<int>());
                        winner = buyer;
                        winningPrice = buyer.Bids.Max();
                    }
                    else
                    {
                        nonWinningBids.AddRange(buyer.Bids);
                    }
                }
                else
                {
                    nonWinningBids.AddRange(buyer.Bids);
                }
            }

            return new WinnerWinningPriceResult
            {
                Winner = winner.Name,
                WinningPrice = nonWinningBids.Where(bid => bid > reservePrice).DefaultIfEmpty(reservePrice).Max()
            };

        }
    }
}