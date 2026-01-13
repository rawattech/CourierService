namespace CourierService.Domain
{
    public class Package
    {
        public string Id { get; }
        public int Weight { get; }
        public int Distance { get; }
        public string OfferCode { get; }

        public decimal Discount { get; set; }
        public decimal TotalCost { get; set; }
        public decimal DeliveryTime { get; set; }

        public Package(string id, int weight, int distance, string offerCode)
        {
            Id = id;
            Weight = weight;
            Distance = distance;
            OfferCode = offerCode;
        }
    }
}


