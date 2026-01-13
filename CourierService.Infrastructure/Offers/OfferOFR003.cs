using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Offers
{
    public class OfferOFR003 : IOfferStrategy
    {
        public string Code => "OFR003";

        public bool IsApplicable(Package p) =>
            p.Distance >= 50 && p.Distance <= 250 &&
            p.Weight >= 10 && p.Weight <= 150;

        public decimal CalculateDiscount(decimal cost) => cost * 0.05m;
    }
}
