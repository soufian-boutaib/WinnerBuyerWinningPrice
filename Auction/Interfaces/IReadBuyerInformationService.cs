using AuctionApp.Auction.Models;
using AuctionApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Auction.Interfaces
{
    public interface IReadBuyerInformationService
    {
        void WriteTableHeader();
        void DisplayWinner(WinnerWinningPriceResult winner);
        void DisplayBuyerInformation();
    }
}