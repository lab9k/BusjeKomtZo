using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;

namespace Busje_komt_zo.Classes.Manager
{
    public class BusManager : IBusManager
    {

        private static readonly int[] BusIds = { 15698388, 15698393, 15698364 };
        private readonly ILocationGetter _locationGetter;
        private Timer _timer;

        public List<Bus> Busses { get; private set; }

        public BusManager(ILocationGetter locationGetter)
        {
            _locationGetter = locationGetter;
            Busses = new List<Bus>();
            foreach (var id in BusIds)
            {
                Busses.Add(new Bus{Id = id});
            }

            _timer = new Timer(UpdateBusses, null,1000,10 * 1000);
        }

        private void UpdateBusses(Object model)
        {
            Console.WriteLine("Updating bus location");
            foreach (var bus in Busses)
            {
                var location = _locationGetter.GetLocation(bus.Id);
                bus.Update(location);
            }
        }


    }
}
