using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Services
{
    public class ShipmentPlanner : IShipmentPlanner
    {
        public IList<Package> PlanShipment(IList<Package> packages, decimal maxWeight)
        {
            var allCombos = GetAllValidCombinations(packages, maxWeight);

            return allCombos
                .OrderByDescending(c => c.Sum(p => p.Weight))         // 1 max weight            
                .ThenByDescending(c => c.Count)                      // 2 max packages
                .ThenBy(c => c.Max(p => p.Distance))                // 3 earliest delivery
                .First();
        }

        private static List<List<Package>> GetAllValidCombinations(
            IList<Package> packages,
            decimal maxWeight)
        {
            var results = new List<List<Package>>();
            int n = packages.Count;

            for (int mask = 1; mask < (1 << n); mask++)
            {
                var combo = new List<Package>();
                decimal weight = 0;

                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        weight += packages[i].Weight;
                        if (weight > maxWeight)
                            break;

                        combo.Add(packages[i]);
                    }
                }

                if (weight <= maxWeight && combo.Any())
                    results.Add(combo);
            }

            return results;
        }



        //public Shipment CreateShipment(List<Package> packages, decimal maxWeight)
        //{
        //    var allCombos = GetAllValidCombinations(packages, maxWeight);

        //    var selected = new List<Package>();
        //    decimal currentWeight = 0.00m;

        //    //foreach (var pkg in packages.OrderBy(p => p.Weight))
        //    //{
        //    //    if (currentWeight + pkg.Weight <= maxWeight)
        //    //    {
        //    //        selected.Add(pkg);
        //    //        currentWeight += pkg.Weight;
        //    //    }
        //    //}

        //    //return new Shipment(selected);

        //    return allCombos
        //    .OrderByDescending(c => c.Count)                     // 1️⃣ max packages
        //    .ThenByDescending(c => c.Sum(p => p.Weight))         // 2️⃣ max weight
        //    .ThenBy(c => c.Max(p => p.Distance))                 // 3️⃣ earliest delivery
        //    .First();
        //}

        //private static List<List<Package>> GetAllValidCombinations(
        //IList<Package> packages,
        //decimal maxWeight)
        //{
        //    var results = new List<List<Package>>();
        //    int n = packages.Count;

        //    for (int mask = 1; mask < (1 << n); mask++)
        //    {
        //        var combo = new List<Package>();
        //        decimal weight = 0;

        //        for (int i = 0; i < n; i++)
        //        {
        //            if ((mask & (1 << i)) != 0)
        //            {
        //                weight += packages[i].Weight;
        //                if (weight > maxWeight)
        //                    break;

        //                combo.Add(packages[i]);
        //            }
        //        }

        //        if (weight <= maxWeight && combo.Any())
        //            results.Add(combo);
        //    }

        //    return results;
        //}
    }
}

