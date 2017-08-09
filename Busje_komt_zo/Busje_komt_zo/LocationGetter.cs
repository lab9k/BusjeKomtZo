using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Busje_komt_zo.Interfaces;
using Newtonsoft.Json;
using SessionFetcher;

namespace Busje_komt_zo
{
    public class LocationGetter : ILocationGetter
    {
        private ISessionGetter SGetter;
        private string Sid;
        private static readonly string FilePath = "SID.txt";

        public LocationTracker Tracker { get; set; }

        public LocationGetter()
        {
            SGetter = new SidFetcher();
            Tracker = new LocationTracker();;
            ReadSid();
        }

        public BusCoordinates GetJsonResult()
        {
            int now = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int from = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 2))).TotalSeconds; //yesterday
            if (Sid == null)
            {
                UpdateSid();
            }
            try
            {
                string json =  GetLocationMessage(Sid, from, now).Result;
            Console.WriteLine(json);
            
                if (!json.Contains("error"))
                {
                    BusCoordinates coords = new BusCoordinates(JsonConvert.DeserializeObject<MessageResponse>(json));
                    Tracker.Add(coords);
                    return coords ;
                }
            }
            catch (Exception e)
            {
                //retry
                UpdateSid();
                return GetJsonResult();
            }
            UpdateSid();
            return GetJsonResult();

        }

        private void ReadSid()
        {
            if(File.Exists(FilePath))
                Sid = System.IO.File.ReadAllText(FilePath);
        }

        private void UpdateSid()
        {
            Sid = SGetter.GetSid();
            System.IO.File.WriteAllText(FilePath, Sid);
        }

        private static async Task<string> GetLocationMessage(string sid,int from, int to)
        {

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("sid",sid));
            nvc.Add(new KeyValuePair<string, string>("params",$"{{\"layerName\":\"messages\",\"itemId\":15698388,\"timeFrom\":{from},\"timeTo\":{to},\"tripDetector\":0,\"flags\":0,\"trackWidth\":4,\"trackColor\":\"cc0000ff\",\"annotations\":0,\"points\":1,\"pointColor\":\"cc0000ff\",\"arrows\":1}}"));

            var req = new HttpRequestMessage(HttpMethod.Post, $"https://hst-api.wialon.com/wialon/ajax.html?svc=render/create_messages_layer&sid={sid}") { Content = new FormUrlEncodedContent(nvc) };
            var client = new HttpClient();
            HttpResponseMessage res = await client.SendAsync(req);
            return res.Content.ReadAsStringAsync().Result;

        }
    }
}
