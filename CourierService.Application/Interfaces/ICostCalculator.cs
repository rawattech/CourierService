using CourierService.Domain;

namespace CourierService.Application.Interfaces
{
    public interface ICostCalculator
    {
        void Calculate(Package package, int baseCost);
    }
}
