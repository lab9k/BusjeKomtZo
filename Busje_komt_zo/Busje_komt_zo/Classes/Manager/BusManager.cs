using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;
using Microsoft.Extensions.Options;

namespace Busje_komt_zo.Classes.Manager
{
    public class BusManager : IBusManager
    {

        private readonly ILocationGetter _locationGetter;
        private Timer _timer;
        private readonly AppConfiguration _config;
        private readonly PredictionManager _predictionManager;

        public List<Bus> Busses { get; private set; }

        public BusManager(ILocationGetter locationGetter, IOptions<AppConfiguration> config, IGeoFence geoFence)
        {
            _predictionManager = new PredictionManager();
            _config = config.Value;
            _locationGetter = locationGetter;
            Busses = new List<Bus>();
            int seqCounter = 1;
            foreach (var id in _config.BusIds)
            {
                Busses.Add(new Bus(_config.BusStatusMsgs,id, geoFence,seqCounter++, @"https://d30y9cdsu7xlg0.cloudfront.net/png/7892-200.png",_predictionManager));
            }

            _timer = new Timer(UpdateBusses, null,1000,_config.UpdateIntervalMs);
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
