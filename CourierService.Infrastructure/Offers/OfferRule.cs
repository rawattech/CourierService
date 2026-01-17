namespace CourierService.Infrastructure.Offers
{
    public class OfferRule
    {
        public string Code { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public int MinDistance {  get; set; }
        public int MaxDistance { get; set; }
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; }
        

    }
}
