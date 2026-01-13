using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Offers
{
    public class OfferOFR002 : IOfferStrategy
    {
        public string Code => "OFR002";

        public bool IsApplicable(Package p) =>
            p.Distance >= 50 && p.Distance <= 150 &&
            p.Weight >= 100 && p.Weight <= 250;

        public decimal CalculateDiscount(decimal cost) => cost * 0.07m;
    }
}
