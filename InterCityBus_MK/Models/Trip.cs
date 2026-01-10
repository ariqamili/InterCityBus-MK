namespace InterCityBus_MK.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
