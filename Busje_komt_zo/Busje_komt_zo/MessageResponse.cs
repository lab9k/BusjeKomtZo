using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo
{
    public class MessageResponse
    {
        public string name { get; set; }
        public List<double> bounds { get; set; }
        public List<Unit> units { get; set; }

        public class First
        {
            public int time { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
        }

        public class Last
        {
            public int time { get; set; }
            public string lat { get; set; }
            public string lon { get; set; }
        }

        public class Msgs
        {
            public int count { get; set; }
            public First first { get; set; }
            public Last last { get; set; }
        }

        public class Unit
        {
            public int id { get; set; }
            public Msgs msgs { get; set; }
            public double mileage { get; set; }
            public int max_speed { get; set; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
