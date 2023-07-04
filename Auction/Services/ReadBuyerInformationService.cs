using AuctionApp.Auction.Configuration;
using AuctionApp.Auction.Interfaces;
using AuctionApp.Auction.Models;
using AuctionApp.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuctionApp.Auction.Services
{
    public class ReadBuyerInformationService : IReadBuyerInformationService
    {
        private readonly List<BuyerEntity> _buyers;

        public ReadBuyerInformationService(AuctionSettings auctionSettingsOptions)
        {
            _buyers = auctionSettingsOptions.Buyers
                .Select(x => new BuyerEntity { Name = x.Name, Bids = x.Bids}).ToList();
        }

        public void DisplayBuyerInformation()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Buyer\t\tBids");
            Console.WriteLine("--------------------------------");
            Console.ResetColor();

            foreach (var buyer in _buyers)
            {
                Console.Write($"{buyer.Name}\t\t");
                if (buyer.Bids?.Count() > 0)
                    Console.WriteLine(string.Join(", ", buyer.Bids));
                else
                    Console.WriteLine();
            }
        }


        public void WriteTableHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Buyer\t\tBids");
            Console.WriteLine("--------------------------------");
            Console.ResetColor();
        }


        public void DisplayWinner(WinnerWinningPriceResult winner)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"The buyer {winner.Winner} wins the auction at the price of {winner.WinningPrice} euros.");
        }
    }
}