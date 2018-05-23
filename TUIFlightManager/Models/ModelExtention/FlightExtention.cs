using System;
using System.ComponentModel.DataAnnotations;

namespace TUIFlightManager.Models
{
    [MetadataType(typeof(FlightMetadata))]
    public partial class Flight
    {
        public string DepartureName
        {
            get
            {
                return DepartureAirport?.Name;
            }
        }

        public string DestinationName
        {
            get
            {
                return DestinationAirport?.Name;
            }
        }
    }

    internal sealed class FlightMetadata
    {
        [Required(ErrorMessage = "Flight Code is required.")]
        [StringLength(4, ErrorMessage = "Flight Code should be on 4 characters.")]
        [Display(Name = "Code")]
        public string CodeName { get; set; }

        [Required(ErrorMessage = "Departure Airport is required.")]
        [Display(Name = "Departure")]
        public Guid DepartureAirportId { get; set; }

        [Required(ErrorMessage = "Destination Airport is required.")]
        [Display(Name = "Destination")]
        public Guid DestinationAirportId { get; set; }

        [Required(ErrorMessage = "Aircraft is required.")]
        public Guid AircraftId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Flight Date")]
        public Nullable<System.DateTime> FlightDate { get; set; }

        [Display(Name = "Fuel Consumption")]
        [Range(typeof(decimal), "0", "99999", ErrorMessage = "Fuel consumption can only be positive.")]
        public decimal FuelConsumption { get; set; }
        
        [Display(Name = "Departure Airport")]
        public string DepartureName { get; }

        [Display(Name = "Destination Airport")]
        public string DestinationName { get; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Latest Modification")]
        public Nullable<System.DateTime> ModificatonDate { get; set; }

    }

}