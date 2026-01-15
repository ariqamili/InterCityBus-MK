namespace InterCityBus_MK.ViewModels
{
    public class TripDisplayViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FromStationName { get; set; }
        public string ToStationName { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }
}
