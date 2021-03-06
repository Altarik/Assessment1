//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TUIFlightManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Aircraft
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aircraft()
        {
            this.Flight = new HashSet<Flight>();
        }
    
        public System.Guid Id { get; set; }
        public string ManufacturerName { get; set; }
        public string AircraftName { get; set; }
        public decimal SeatsAmount { get; set; }
        public decimal FuelConsumptionPerDistance { get; set; }
        public decimal TakeoffEffort { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flight> Flight { get; set; }
    }
}
