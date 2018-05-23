using System;
using System.ComponentModel.DataAnnotations;

namespace TUIFlightManager.Models
{
    [MetadataType(typeof(AircraftMetadata))]
    public partial class Aircraft
    {
        public string DisplayName
        {
            get
            {
                return ManufacturerName + " - " + AircraftName ;
            }
        }
    }

    internal sealed class AircraftMetadata
    {
        [Display(Name = "Aircraft")]
        public string DisplayName { get; }
    }

}