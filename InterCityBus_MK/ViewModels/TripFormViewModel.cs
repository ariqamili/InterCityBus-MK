using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InterCityBus_MK.Models;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.ViewModels
{
    public class TripFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int FromStationId { get; set; }
        
        [Required]
        public int ToStationId { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeOnly DepartureTime { get; set; }

        [Required] 
        [DataType(DataType.Time)]
        public TimeOnly ArrivalTime { get; set; }


        [Required]
        [Range(1.00, 10000.00)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        public IEnumerable<Company> Companies { get; set; } = new List<Company>();
        public IEnumerable<Station> Stations { get; set; } = new List<Station>();
    }
}
