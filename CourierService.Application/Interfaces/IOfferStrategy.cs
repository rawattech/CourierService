using CourierService.Domain;

namespace CourierService.Application.Interfaces
{
    public interface IOfferStrategy
    {
        string Code { get; }
        bool IsApplicable(Package package);
        decimal CalculateDiscount(decimal deliveryCost);
    }
}
