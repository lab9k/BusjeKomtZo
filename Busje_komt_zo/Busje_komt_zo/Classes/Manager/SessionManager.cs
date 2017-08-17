using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Busje_komt_zo.Interfaces;
using SessionFetcher;

namespace Busje_komt_zo.Classes.Manager
{
    /// <summary>
    /// Fetches SID from server and continiously checks if SID is still valid
    /// </summary>
    public class SessionManager : ISessionManager
    {
        private const string _filePath = "SID.txt";
        private Timer _timer;
        private SidFetcher _fetcher;
        private bool IsUpdating = false;

        public string Sid { get; private set; }

        public SessionManager()
        {
            Sid = ReadSidFromFile(_filePath);
            _fetcher = new SidFetcher();
            _timer = new Timer(CheckSession,null,TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));
        }

        public void CheckSession(Object stateInfo)
        {
            Console.WriteLine("Checking session");
            while (true)
            {
                if (Sid != null && TestSid(Sid).Result)
                {
                    Console.WriteLine("Sid is still valid");
                }
                else if (IsUpdating)
                {
                    Console.WriteLine("Busy updating Sid");
                }
                else
                {
                    Console.WriteLine($"Sid: {Sid} is no longer valid");
                    IsUpdating = true;
                    Sid = UpdateSid(_filePath);
                    IsUpdating = false;
                    continue;
                }
                break;
            }
        }

        private string ReadSidFromFile(string path)
        {
            return File.Exists(path) ? System.IO.File.ReadAllText(path) : null;
        }

        private string UpdateSid(string writePath)
        {
            string res = _fetcher.GetSid();
            System.IO.File.WriteAllText(writePath,res);
            return res;
        }


        private static async Task<bool> TestSid(string sid)
        {
            try
            {
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("sid", sid));

                var req = new HttpRequestMessage(HttpMethod.Post, @"https://hst-api.wialon.com/avl_evts")
                {
                    Content = new FormUrlEncodedContent(nvc)
                };
                var client = new HttpClient();
                HttpResponseMessage res = await client.SendAsync(req);
                string result = res.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Checking sid: {result}");
                return !result.Contains("error");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

    }
}
