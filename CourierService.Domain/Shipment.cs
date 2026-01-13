namespace CourierService.Domain
{
    public class Shipment
    {
        public List<Package> Packages { get; }

        public Shipment(List<Package> packages)
        {
            Packages = packages;
        }

        public int TotalWeight => Packages.Sum(p => p.Weight);
        public int MaxDistance => Packages.Max(p => p.Distance);
    }
}
