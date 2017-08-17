using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;

namespace Busje_komt_zo.Interfaces
{
    public interface IBusManager
    {
        List<Bus> Busses { get; }
    }
}
