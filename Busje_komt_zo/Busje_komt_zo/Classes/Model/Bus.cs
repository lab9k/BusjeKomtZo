using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo.Classes.Model
{
    public class Bus
    {
        private BusCoordinates _lastLocation;
        public int Counter { get; set; }//Counter wil increment if previous location is the same as the current location

        

        public bool IsAtStop { get; set; }
        public BusStop LastStopVisited { get; set; }

        public int Id { get; set; }

        public BusCoordinates Position => _lastLocation;

        public string Message
        {
            get
            {
                if (!IsActive())
                {
                    return "Inactief";
                }
                else if (IsAtStop)
                {
                    if (LastStopVisited == BusStop.Jacob)
                    {
                        return "Aan Sint-Jacobs";
                    }
                    else
                    {
                        return "Aan P+R Weba-Decathlon";
                    }
                }
                else
                {
                    if (LastStopVisited == BusStop.Weba)
                    {
                        return "Onderweg naar Sint-Jacobs";
                    }
                    else
                    {
                        return "Onderweg naar P+R Weba-Decathlon";
                    }
                }
            }
        }

        public bool IsActive()
        {
            return Counter < 5; //Bus is inactive if counter goes above 5
        }

        public void Update(BusCoordinates coords)
        {
            if (coords != null && !coords.Equals(Position))
            {
                Counter = Counter / 2;

                _lastLocation = coords;
                if (GeoFencing.IsAtWeba(coords.Latitude, coords.Longitude))
                {
                    Console.WriteLine($"{Id} is at Weba");
                    IsAtStop = true;
                    LastStopVisited = BusStop.Weba;
                }
                else if (GeoFencing.IsAtJacobs(coords.Latitude, coords.Longitude))
                {
                    Console.WriteLine($"{Id} is at Jacob");
                    IsAtStop = true;
                    LastStopVisited = BusStop.Jacob;
                }
                else
                {
                    IsAtStop = false;
                }
            }
            else
            {
                _lastLocation = coords;
                Counter = Counter < 10 ? Counter + 1 : Counter;
            }
        }


    }
}
