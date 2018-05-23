using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TUIFlightManager.Models;

namespace TUIFlightManager.ViewModels
{
    public class FlightViewModel
    {
        public FlightViewModel()
        {
            MyFlight = new Flight();
        }

        public Flight MyFlight { get; set; }
        public IEnumerable<Aircraft> Aircrafts { get; set; }
        public IEnumerable<Airport> Airports { get; set; }
    }
}