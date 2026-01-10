namespace InterCityBus_MK.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int StationId { get; set; }
        public int StopOrder { get; set; }
    }
}
