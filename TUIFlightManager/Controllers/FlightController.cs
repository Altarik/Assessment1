using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TUIFlightManager.Models;
using TUIFlightManager.Models.BLL;
using TUIFlightManager.ViewModels;

namespace TUIFlightManager.Controllers
{
    public class FlightController : Controller
    {

        private FlightBusiness flightBusiness = new FlightBusiness();

        public ActionResult Index()
        {
            var flights = flightBusiness.GetAllFlights();
            return View(flights.ToList());
        }
        
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flight flight = flightBusiness.GetFlight(id.Value);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }
        
        public ActionResult Create()
        {
            FlightViewModel viewmodel = new FlightViewModel();
            viewmodel.Airports = flightBusiness.GetAllAirports();
            viewmodel.Aircrafts = flightBusiness.GetAllAircrafts();
            return View(viewmodel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightViewModel flightViewModel)
        {
            if (ModelState.IsValid)
            {
                flightBusiness.SaveFlight(flightViewModel.MyFlight);
                return RedirectToAction("Index");
            }

            return View();
        }
        
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = flightBusiness.GetFlight(id.Value);
            if (flight == null)
            {
                return HttpNotFound();
            }

            FlightViewModel viewmodel = new FlightViewModel();
            viewmodel.Airports = flightBusiness.GetAllAirports();
            viewmodel.Aircrafts = flightBusiness.GetAllAircrafts();
            viewmodel.MyFlight = flight;
            return View(viewmodel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FlightViewModel flightViewModel)
        {
            if (ModelState.IsValid)
            {
                flightBusiness.SaveFlight(flightViewModel.MyFlight);
                return RedirectToAction("Index");
            }
            return View(flightViewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flight flight = flightBusiness.GetFlight(id.Value);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            flightBusiness.DeleteFlight(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                flightBusiness.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
