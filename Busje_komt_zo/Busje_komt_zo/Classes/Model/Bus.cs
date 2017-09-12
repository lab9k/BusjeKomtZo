using System;
using System.IO;
using Busje_komt_zo.Classes.Manager;
using Busje_komt_zo.Interfaces;

namespace Busje_komt_zo.Classes.Model
{
    public class Bus
    {
        private readonly IGeoFence _geoFence;
        private readonly string[] _messages;
        private readonly PredictionManager _predictionManager;

        public Bus(string[] messages, int id, IGeoFence fence, int SequenceNr, string IconUrl, PredictionManager predictionManager = null)
        {
            _predictionManager = predictionManager;
            _geoFence = fence;
            _messages = messages;
            Id = id;
            SequenceNumber = SequenceNr;
            this.IconUrl = IconUrl;
        }

        public bool IsInRange
        {
            get {
                if (Position != null)
                {
                    return _geoFence.IsInRange(Position.Latitude, Position.Longitude);
                }
                else
                {
                    return false;
                }}
        }

        public int SequenceNumber { get; set; }
        public string IconUrl  { get; set; }

        public int
            Counter { get; set; } //Counter wil increment if previous location is the same as the current location

        public int MinutesTillArrival
        {
            get
            {
                if (_predictionManager != null && Position != null)
                {
                    return _predictionManager.GetMinutesTillArival(this);
                }
                else
                {
                    return -2;
                }
            }
        }

        public bool IsAtStop { get; set; }
        public BusStop LastStopVisited { get; set; }

        public int Id { get; set; }

        public BusCoordinates Position { get; private set; }

        public string Message
        {
            get
            {
                if (!IsActive())
                    return _messages[0] + ((LastStopVisited == BusStop.Weba) ? " - PR Weba-Decathlon" : "Sint-Jacobs"); // first message in array
                if (IsAtStop)
                    if (LastStopVisited == BusStop.Jacob)
                    {
                        return _messages[1];
                        ;
                    }
                    else
                    {
                        return _messages[2];
                        ;
                    }
                if (LastStopVisited == BusStop.Weba)
                    return _messages[3];
                return _messages[4]; //last message in array
            }
        }

        public bool IsActive()
        {
            return Counter < 20; //Bus is inactive if counter goes above 20
        }

        public void Update(BusCoordinates coords)
        {
            if (coords != null && !coords.Equals(Position))
            {
                Counter = Counter / 4;

                Position = coords;
                if (_geoFence.IsAtWeba(coords.Latitude, coords.Longitude))
                {
                    Console.WriteLine($"{Id} is at Weba");
                    IsAtStop = true;
                    LastStopVisited = BusStop.Weba;
                }
                else if (_geoFence.IsAtJacobs(coords.Latitude, coords.Longitude))
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
                Position = coords;
                Counter = Counter < 30 ? Counter + 1 : Counter;
            }
        }
    }
}