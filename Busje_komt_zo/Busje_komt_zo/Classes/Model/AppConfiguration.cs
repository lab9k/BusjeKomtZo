using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo.Classes.Model
{
    public class AppConfiguration
    {
        public Credentials Login { get; set; }
        public ApiUrls Api { get; set; }
        public string ChromeDriverLocation { get; set; }
        public int[] BusIds { get; set; }
        public string[] BusIcons { get; set; }
        public int UpdateIntervalMs { get; set; }
        public string[] BusStatusMsgs { get; set; }
        public GeoFence GeoFencing { get; set; }

        public class Credentials
        {
            public string User { get; set; }
            public string Password { get; set; }
            
        }

        public class ApiUrls
        {
            public string Locations { get; set; }
            public string Events { get; set; }
            public string SiteUrl { get; set; }
        }

        public class GeoFence
        {
            //string to prevent wrong localization reading
            public string[] PRLocation { get; set; }
            public string[] CentreLocation { get; set; }
            public string StopTolerance { get; set; }
            public string ActiveTolerance { get; set; }
        }
    }
}
