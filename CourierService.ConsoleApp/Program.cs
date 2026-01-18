using Microsoft.Extensions.DependencyInjection;
using CourierService.Application.Interfaces;
using CourierService.Domain;
using CourierService.Infrastructure.Offers;
using CourierService.Infrastructure.Services;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Core services
builder.Services.AddScoped<ICostCalculator, CostCalculator>();
builder.Services.AddScoped<IDeliveryTimeEstimator, DeliveryTimeEstimator>();
builder.Services.AddScoped<IShipmentPlanner, ShipmentPlanner>();

// Offers
var offers = OfferRuleProvider.Load(builder.Configuration);
foreach (var offer in offers)
{
    builder.Services.AddSingleton(typeof(IOfferStrategy), offer);
}

var provider = builder.Build();

var calculator = provider.Services.GetRequiredService<ICostCalculator>();
var estimator = provider.Services.GetRequiredService<IDeliveryTimeEstimator>();
var planner = provider.Services.GetRequiredService<IShipmentPlanner>();


var firstLine = Console.ReadLine()!.Split();
int baseCost = int.Parse(firstLine[0]);
int n = int.Parse(firstLine[1]);

var packages = new List<Package>();

for (int i = 0; i < n; i++)
{
    var input = Console.ReadLine()!.Split();
    packages.Add(new Package(
        input[0],
        int.Parse(input[1]),
        int.Parse(input[2]),
        input[3]
    ));
}

var vehicleInput = Console.ReadLine()!.Split();
int vehicleCount = int.Parse(vehicleInput[0]);
decimal speed = decimal.Parse(vehicleInput[1]);
decimal maxWeight = decimal.Parse(vehicleInput[2]);

foreach (var pkg in packages)
    calculator.Calculate(pkg, baseCost);

estimator.Estimate(packages, vehicleCount, speed, maxWeight);

foreach (var pkg in packages)
{
    Console.WriteLine($"{pkg.Id} {pkg.Discount:0} {pkg.TotalCost:0} {pkg.DeliveryTime:0.00}");
}
