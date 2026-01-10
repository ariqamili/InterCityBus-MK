namespace InterCityBus_MK.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}
