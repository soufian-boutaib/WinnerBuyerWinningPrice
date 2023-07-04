using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using AuctionApp.Auction.Configuration;
using AuctionApp.Auction.Interfaces;
using AuctionApp.Auction.Services;
using Microsoft.Extensions.Options;

public class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        ConfigureServices(serviceCollection, configuration);

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Resolve the services you need
        var readBuyerInformationService = serviceProvider.GetService<IReadBuyerInformationService>();
        var winnerWinningPriceService = serviceProvider.GetService<IWinnerWinningPriceService>();

        // Example usage:
        readBuyerInformationService.WriteTableHeader();
        readBuyerInformationService.DisplayBuyerInformation();
        var winner = winnerWinningPriceService.FindWinner();
        readBuyerInformationService.DisplayWinner(winner);

        // Wait for user input to close the console
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Configure AuctionSettings from appsettings.json
        var auctionSettings = new AuctionSettings();
        configuration.GetSection("AuctionSettings").Bind(auctionSettings);
        services.AddSingleton(auctionSettings);

        services.AddScoped<IReadBuyerInformationService, ReadBuyerInformationService>();
        services.AddScoped<IWinnerWinningPriceService, WinnerWinningPriceService>();
    }
}