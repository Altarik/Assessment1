using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using TUIFlightManager.Models;

namespace TUIFlightManager.Models.BLL
{
    /// <summary>
    /// Flight Business Logic
    /// </summary>
    public class FlightBusiness
    {
        private TuiDbEntities db = new TuiDbEntities();
        
        public IEnumerable<Flight> GetAllFlights()
        {
            IEnumerable<Flight> flights = null;
            try
            {
                flights = db.Flight.Include(f => f.Aircraft).Include(f => f.DestinationAirport).Include(f => f.DepartureAirport).OrderBy(f => f.CreationDate);
            }
            catch
            {
                //Some logs for real prod to put here
                return null;
            }

            return flights;
        }

        public Flight GetFlight(Guid id)
        {
            Flight flight = null;
            try
            {
                flight = db.Flight.Find(id);
            }
            catch
            {
                return null;
            }

            return flight;
        }

        public void SaveFlight(Flight flight)
        {
            //Case : null input
            if(flight == null)
            {
                return;
            }
            try
            {
                //Distance should be computed before Fuel consumption (distance is required for Fuel consumption computation)
                ComputeDistance(flight);
                ComputeFuelConsumption(flight);

                //Case : Create new flight
                if (flight.Id == Guid.Empty)
                {
                    flight.Id = Guid.NewGuid();
                    flight.CreationDate = DateTime.Now;
                    db.Flight.Add(flight);
                }
                //Case : Edit existing flight
                else
                {
                    flight.ModificatonDate = DateTime.Now;
                    db.Entry(flight).State = EntityState.Modified;
                }
                
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return;
            }
            
        }

        private void ComputeFuelConsumption(Flight flight)
        {
            //Case : if aircraft didn't take off, no fuel consumption
            if(flight.Distance == 0m)
            {
                flight.FuelConsumption = 0m;
            }
            else
            {
                var aircraft = db.Aircraft.Find(flight.AircraftId);
                var unroundedFuelConsumption = (flight.Distance * aircraft.FuelConsumptionPerDistance) + aircraft.TakeoffEffort; // Formula : Flight Distance (km) * FuelConsumption(Galons/km) + takeoffEffort (Galons)
                flight.FuelConsumption = decimal.Round(unroundedFuelConsumption, 2, MidpointRounding.AwayFromZero);
            }
        }

        private void ComputeDistance(Flight flight)
        {
            var departureAirport = db.Airport.Find(flight.DepartureAirportId);
            var destinationAirport = db.Airport.Find(flight.DestinationAirportId);

            //Compute distance between the 2 gps positions (dbgeography coordinates)
            double distance = destinationAirport.Location.Distance(departureAirport.Location)/1000 ?? 0d; //DbGeography's distances are expressed in meters, we vivice by 1000 to get Kilometers

            var unRoundedDistance = Convert.ToDecimal(distance); //Distance unrounded
            flight.Distance = decimal.Round(unRoundedDistance, 2, MidpointRounding.AwayFromZero);
        }

        public void DeleteFlight(Guid id)
        {
            try
            {
                Flight flight = GetFlight(id);
                db.Flight.Remove(flight);
                db.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        public IEnumerable<Airport> GetAllAirports()
        {
            try
            {
                return db.Airport;
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<Aircraft> GetAllAircrafts()
        {
            try
            {
                return db.Aircraft;
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}