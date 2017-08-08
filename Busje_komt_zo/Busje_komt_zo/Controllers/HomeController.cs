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
        public IActionResult Index([FromServices] ILocationGetter getter)
        {
            MessageResponse response = getter.GetJsonResult();
            ViewData["lat"] = response.units[0].msgs.last.lat;
            ViewData["lon"] = response.units[0].msgs.last.lon;

            return View(response.units[0].msgs.last);
        }
        
    }
}
