using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo.Classes.Model
{
    public class PredictionMatch
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public BusStop Destination { get; set; }
        public int MinutesTillArrival { get; set; }
    }
}
