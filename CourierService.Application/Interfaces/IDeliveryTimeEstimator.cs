using CourierService.Domain;

namespace CourierService.Application.Interfaces
{
    public interface IDeliveryTimeEstimator
    {
        void Estimate(
        IList<Package> packages,
        int vehicleCount,
        decimal maxSpeed,
        decimal maxWeight);
    }
}
