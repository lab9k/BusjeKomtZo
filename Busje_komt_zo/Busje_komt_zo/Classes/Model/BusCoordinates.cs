using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo
{
    public class BusCoordinates
    {

        public double Latitude { get; set; } 
        public double Longitude { get; set; }

        public BusCoordinates()
        {
                
        }

        public BusCoordinates(MessageResponse response)
        {
            var responseUnit = response?.units?[0];
            if (responseUnit?.msgs?.last != null)
            {
                Latitude = (double) responseUnit?.msgs?.last?.lat;
                Longitude = (double)responseUnit?.msgs?.last?.lon;
            }
        }

        protected bool Equals(BusCoordinates other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BusCoordinates) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }
    }
}
