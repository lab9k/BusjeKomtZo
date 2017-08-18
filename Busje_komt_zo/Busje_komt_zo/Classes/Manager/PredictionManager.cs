using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;

namespace Busje_komt_zo.Classes.Manager
{
    public class PredictionManager
    {
        private readonly IPredictionProvider _provider;

        public PredictionManager()
        {
            _provider = new ExperimentalProvider();
        }


        public int GetMinutesTillArival(Bus bus)
        {
            if (_provider != null)
            {
                if (bus.IsAtStop)
                {
                    return -1;
                }
                else
                {
                    return _provider.GetTimeTillArrival(bus.Position, bus.LastStopVisited).MinutesTillArrival;
                }
            }
            else
            {
                return -2;
            }
        }
    

    }
}
