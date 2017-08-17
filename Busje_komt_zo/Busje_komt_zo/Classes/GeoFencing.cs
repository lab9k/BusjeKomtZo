using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo
{
    public class GeoFencing
    {
        private const double Dlat = 0.03118975;
        private const double Dlon = 0.03118975;
        private const double Reflat = 51.07421875;
        private const double Reflon = 3.74382209778;

        private const double Tolerance = 0.005;

        public static bool IsInRange(double lat, double lon)
        {
            Console.WriteLine($"{lat}   {lon}");
            Console.WriteLine($"{Reflat - lat}   {Reflon - lon}");
            return Math.Abs(Reflat - lat) < Dlat && Math.Abs(Reflon - lon) < Dlon;
        }

        public static bool IsAtWeba(double lat, double lon)
        {
            return Math.Abs(BusCoordinates.Weba_Lat - lat) < Tolerance &&
                   Math.Abs(BusCoordinates.Weba_Lon - lon) < Tolerance;
        }

        public static bool IsAtJacobs(double lat, double lon)
        {
            return Math.Abs(BusCoordinates.Jacob_Lat - lat) < Tolerance &&
                   Math.Abs(BusCoordinates.Jacob_Lon - lon) < Tolerance;
        }
    }
}
