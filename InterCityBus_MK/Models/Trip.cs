using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InterCityBus_MK.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [Required]
        public int FromStationId { get; set; }

        [ForeignKey("FromStationId")]
        public Station FromStation { get; set; }
        
        [Required]
        public int ToStationId { get; set; }

        [ForeignKey("ToStationId")]
        public Station ToStation { get; set; }

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

        public virtual ICollection<Stop> Stops { get; set; } = new List<Stop>();
    }
}
