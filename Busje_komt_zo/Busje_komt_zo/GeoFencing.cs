using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo
{
    public class GeoFencing
    {
        private static double dlat = 0.03118975;
        private static double dlon = 0.03118975;
        private static double reflat = 51.07421875;
        private static double reflon = 3.74382209778;


        public static bool IsInRange(double lat, double lon)
        {
            Console.WriteLine($"{lat}   {lon}");
            Console.WriteLine($"{reflat - lat}   {reflon - lon}");
            return Math.Abs(reflat - lat) < dlat && Math.Abs(reflon - lon) < dlon;
        }

    }
}
