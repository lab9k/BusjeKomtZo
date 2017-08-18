using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;

namespace Busje_komt_zo.Interfaces
{
    /// <summary>
    /// Specifies format in which prediction data should be delivered and accessed
    /// </summary>
    public interface IPredictionProvider
    {
        PredictionMatch GetTimeTillArrival(BusCoordinates currentBusCoordinates, BusStop LastStop);
    }
}
