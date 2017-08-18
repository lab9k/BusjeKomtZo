using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo.Interfaces
{
    public interface IGeoFence
    {
        bool IsInRange(double lat, double lon);
        bool IsAtWeba(double lat, double lon);
        bool IsAtJacobs(double lat, double lon);
    }
}
