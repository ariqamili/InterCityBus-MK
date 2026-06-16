using System.ComponentModel.DataAnnotations;

namespace InterCityBus_MK.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [StringLength (80)]
        public string Name { get; set; }

        [EmailAddress] 
        [StringLength (40)]
        public string? ContactEmail { get; set; }

        public virtual ICollection<Trip>? Trips { get; set; }
    }
}
