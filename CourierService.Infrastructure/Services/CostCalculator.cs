using CourierService.Application.Interfaces;
using CourierService.Domain;

namespace CourierService.Infrastructure.Services
{
    public class CostCalculator : ICostCalculator
    {
        private readonly IEnumerable<IOfferStrategy> _offers;

        public CostCalculator(IEnumerable<IOfferStrategy> offers)
        {
            _offers = offers;
        }
        public void Calculate(Package package, int baseCost)
        {
            var deliveryCost = baseCost +
                           (package.Weight * 10) +
                           (package.Distance * 5);

            var offer = _offers.FirstOrDefault(o => o.Code == package.OfferCode);

            var discount = (offer != null && offer.IsApplicable(package))
                ? offer.CalculateDiscount(deliveryCost)
                : 0;

            package.Discount = discount;
            package.TotalCost = deliveryCost - discount;
        }
    }
}
