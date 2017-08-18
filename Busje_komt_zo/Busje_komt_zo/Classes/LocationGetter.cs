using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Manager;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SessionFetcher;

namespace Busje_komt_zo
{
    public class LocationGetter : ILocationGetter
    {
        private readonly SessionManager _sGetter;
        private readonly AppConfiguration config;

        public LocationGetter(IOptions<AppConfiguration> config)
        {
            this.config = config.Value;
            _sGetter = new SessionManager(this.config);
            
        }

        public BusCoordinates GetLocation(int busId)
        {
            int now = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int from = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 2))).TotalSeconds; //yesterday

            string json = GetLocationMessage(_sGetter.Sid, from, now, busId).Result;

                if (!json.Contains("error"))
                {
                    BusCoordinates coords = new BusCoordinates(JsonConvert.DeserializeObject<MessageResponse>(json));
                    return coords ;
                }
            return null;

        }
        /// <summary>
        /// TODO: Change to proper API Call
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="busId"></param>
        /// <returns></returns>
        private async Task<string> GetLocationMessage(string sid,int from, int to, int busId)
        {

            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("sid", sid),
                new KeyValuePair<string, string>("params",
                    $"{{\"layerName\":\"messages\",\"itemId\":{busId},\"timeFrom\":{from},\"timeTo\":{to},\"tripDetector\":0,\"flags\":0,\"trackWidth\":4,\"trackColor\":\"cc0000ff\",\"annotations\":0,\"points\":1,\"pointColor\":\"cc0000ff\",\"arrows\":1}}")
            };

            var req = new HttpRequestMessage(HttpMethod.Post, string.Format(config.Api.Locations,sid)) { Content = new FormUrlEncodedContent(nvc) };
            var client = new HttpClient();
            HttpResponseMessage res = await client.SendAsync(req);
            return res.Content.ReadAsStringAsync().Result;

        }
    }
}
