namespace CourierService.Domain
{
    public class Vehicle
    {
        public int Id { get; }
        public decimal AvailableAt { get; set; }

        public Vehicle(int id)
        {
            Id = id;
            AvailableAt = 0;
        }

        public void Assign(decimal availableAt)
        {
            AvailableAt = availableAt;
        }
    }
}
