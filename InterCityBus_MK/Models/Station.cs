using System.ComponentModel.DataAnnotations;

namespace InterCityBus_MK.Models
{
    public class Station
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string City { get; set; }
    }
}