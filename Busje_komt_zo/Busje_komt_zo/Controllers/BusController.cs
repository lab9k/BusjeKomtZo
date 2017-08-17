using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Busje_komt_zo.Controllers
{
    [Produces("application/json")]
    [Route("api/bus")]
    public class BusController : Controller
    {

        public List<Bus> Get([FromServices] IBusManager busManager)
        {
            return busManager.Busses;
        }
    }
}