using CourierService.Domain;

namespace CourierService.Application.Interfaces
{
    public interface IShipmentPlanner
    {
        IList<Package> PlanShipment(IList<Package> availablePackages,
                                    decimal maxWeight);
    }
}
