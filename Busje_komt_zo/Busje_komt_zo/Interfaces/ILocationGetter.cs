﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo.Interfaces
{
    public interface ILocationGetter
    {
        BusCoordinates GetLocation(int busId);
    }
}
