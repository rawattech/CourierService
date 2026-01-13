using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Offers
{
    public class OfferOFR001 : IOfferStrategy
    {
        public string Code => "OFR001";

        public bool IsApplicable(Package p) =>
            p.Distance < 200 && p.Weight >= 70 && p.Weight <= 200;

        public decimal CalculateDiscount(decimal cost) => cost * 0.10m;
    }
}
