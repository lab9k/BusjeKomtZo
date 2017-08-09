using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SessionFetcher;

namespace Busje_komt_zo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index([FromServices] ILocationGetter getter, int id)
        {
            Console.WriteLine("ID: " + id);
            BusCoordinates response = getter.GetJsonResult();
            Console.WriteLine("Lat: " + response.Latitude);
            if (GeoFencing.IsInRange(response.Latitude,
                response.Longitude))
            {
                ViewData["lat"] = response.Latitude;
                ViewData["lon"] = response.Longitude;
                ViewData["weba"] = id == 1 ? "U bent hier." : "P + R Weba / Decathlon";
                ViewData["jacob"] = id == 2 ? "U bent hier." : "Sint-Jacobs";
                ViewData["id"] = id;
                ViewData["message"] = getter.Tracker.GetMovementMessage(); 


                return View(response);
            }
            else
            {
                return View();
            }
        }
        
    }
}
