using System.ComponentModel.DataAnnotations;
using InterCityBus_MK.Models;

namespace InterCityBus_MK.ViewModels
{
    public class TripSearchViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Please select a departure station")]
        public int? FromStationId { get; set; }

        [Required(ErrorMessage = "Please select a destination station")]
        public int? ToStationId { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? TravelDate { get; set; }

        public bool HasSearched { get; set; } = false;

        public List<TripDisplayViewModel> Results { get; set; } = new();

        public IEnumerable<Station> Stations { get; set; } = new List<Station>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FromStationId.HasValue &&
                ToStationId.HasValue &&
                FromStationId == ToStationId)
            {
                yield return new ValidationResult(
                    "Departure and destination stations must be different.",
                    new[] { nameof(FromStationId), nameof(ToStationId) }
                );
            }
        }
    }
}
