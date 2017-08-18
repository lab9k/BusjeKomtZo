using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;
using Microsoft.Extensions.Options;

namespace Busje_komt_zo
{
    public class GeoFencing : IGeoFence
    {

        private readonly AppConfiguration _config;

        public GeoFencing(IOptions<AppConfiguration> config)
        {
            _config = config.Value;
        }

        public bool IsInRange(double lat, double lon)
        {
            return Math.Abs(double.Parse(_config.GeoFencing.CentreLocation[0], CultureInfo.InvariantCulture) - lat) < double.Parse(_config.GeoFencing.ActiveTolerance, CultureInfo.InvariantCulture) && Math.Abs(double.Parse(_config.GeoFencing.CentreLocation[1], CultureInfo.InvariantCulture) - lon) < double.Parse(_config.GeoFencing.ActiveTolerance, CultureInfo.InvariantCulture);
        }

        public bool IsAtWeba(double lat, double lon)
        {
            return Math.Abs(double.Parse(_config.GeoFencing.PRLocation[0], CultureInfo.InvariantCulture) - lat) < double.Parse(_config.GeoFencing.StopTolerance, CultureInfo.InvariantCulture) &&
                   Math.Abs(double.Parse(_config.GeoFencing.PRLocation[1], CultureInfo.InvariantCulture) - lon) < double.Parse(_config.GeoFencing.StopTolerance, CultureInfo.InvariantCulture);
        }

        public bool IsAtJacobs(double lat, double lon)
        {
            return Math.Abs(double.Parse(_config.GeoFencing.CentreLocation[0], CultureInfo.InvariantCulture) - lat) < double.Parse(_config.GeoFencing.StopTolerance, CultureInfo.InvariantCulture) &&
                   Math.Abs(double.Parse(_config.GeoFencing.CentreLocation[1], CultureInfo.InvariantCulture) - lon) < double.Parse(_config.GeoFencing.StopTolerance, CultureInfo.InvariantCulture);
        }
    }
}
