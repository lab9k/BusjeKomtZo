using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo.Classes.Model
{
    public class Bus
    {
        private BusCoordinates _lastLocation;
        private BusCoordinates _lastStopVisited;
        private int _itemId;

        public BusCoordinates Position { get; set; }

        public bool IsActive()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }


    }
}
