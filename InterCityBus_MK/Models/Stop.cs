using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterCityBus_MK.Models
{
    public class Stop
    {
        public int Id { get; set; }
        
        [Required]
        public int TripId { get; set; }
        
        [ForeignKey("TripId")]
        public virtual Trip? Trip { get; set; }

        [Required]
        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station? Station { get; set; }

        [Required]
        [Range(1, 40)]
        public int StopOrder { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeOnly ArrivalTime { get; set; }
    }
}
