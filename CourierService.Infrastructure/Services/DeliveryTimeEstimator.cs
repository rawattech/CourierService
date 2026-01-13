using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Services
{
    public class DeliveryTimeEstimator : IDeliveryTimeEstimator
    {
        private readonly IShipmentPlanner _shipmentPlanner;

        public DeliveryTimeEstimator(IShipmentPlanner shipmentPlanner)
        {
            _shipmentPlanner = shipmentPlanner;
        }
        public void Estimate(IList<Package> packages, int vehicleCount, decimal speed, decimal maxWeight)
        {
            var vehicles = Enumerable.Range(1, vehicleCount)
            .Select(id => new Vehicle(id))
            .ToList();

            var remaining = new List<Package>(packages);

            while (remaining.Any())
            {
                var vehicle = vehicles.OrderBy(v => v.AvailableAt).First();
                var currentTime = vehicle.AvailableAt;

                var shipment = _shipmentPlanner.PlanShipment(remaining, maxWeight);

                var maxDistance = shipment.Max(p => p.Distance);
                var tripTime = Truncate(maxDistance / speed);

                foreach (var pkg in shipment)
                {
                    pkg.DeliveryTime = 
                        currentTime + Truncate(pkg.Distance / speed);
                }

                vehicle.Assign(currentTime + (2 * tripTime));

                foreach (var pkg in shipment)
                {
                    remaining.Remove(pkg);
                }                
            }
        }

        private static decimal Truncate(decimal value)
       => Math.Floor(value * 100) / 100;
    }
}
