namespace InterCityBus_MK.Models // or .ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalStops { get; set; }
        public int TotalStations { get; set; }
        public int TotalTrips { get; set; }
        public int TotalCompanies { get; set; }

        // We can add "Latest Activity" or "Recent Trips" here later
    }
}