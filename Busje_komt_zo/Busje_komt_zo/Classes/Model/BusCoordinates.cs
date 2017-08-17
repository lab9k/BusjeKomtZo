using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo
{
    public class BusCoordinates
    {
        public static readonly double Weba_Lat = 51.07421875;
        public static readonly double Weba_Lon = 3.74382209778;
        public static readonly double Jacob_Lat = 51.0565109253;
        public static readonly double Jacob_Lon = 3.72703361511;

        public double Latitude { get; set; } 
        public double Longitude { get; set; }

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

    public class MovementComparer : IComparer<BusCoordinates>
    {

        /// <summary>
        /// Compares 2 coordinates according to distance to a certain point
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// 0: Both coordinates are on the same position
        /// 1: First coordinate is closer to Weba than second => moving away from weba
        /// -1: second coordinate is closer to Weba => moving to weba
        /// </returns>
        public int Compare(BusCoordinates x, BusCoordinates y)
        {
            double[] xDistance = DistanceToPoints(x.Latitude, x.Longitude);
            double[] yDistance = DistanceToPoints(y.Latitude, y.Longitude);
            if (xDistance[0] < yDistance[0] || xDistance[1] > yDistance[0])
            {
                Console.WriteLine("Bus is moving towards Jacobs");
                return 1;
            }
            else if (xDistance[0] > yDistance[0] || xDistance[1] < yDistance[0])
            {
                Console.WriteLine("Bus is moving towards Weba");
                return -1;

            }
            else if (xDistance[0] == yDistance[0] || yDistance[1] == xDistance[1])
            {
                return 0;
            }
            else
            {
                throw new NotImplementedException("This situation is not yet implemented.");
            }
          }

        /// <summary>
        /// Calculates distance to both Weba stop and Jacob Stop and returns both values in an array with index 0 = Weba and 1 = Jacobs
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        private double[] DistanceToPoints(double lon, double lat)
        {
            double dWeba = Math.Sqrt((BusCoordinates.Weba_Lat  - lat) * (BusCoordinates.Weba_Lat - lat) + (BusCoordinates.Weba_Lon - lon) * (BusCoordinates.Weba_Lon - lon));
            double dJacob = Math.Sqrt((BusCoordinates.Jacob_Lat  - lat) * (BusCoordinates.Jacob_Lat - lat) + (BusCoordinates.Jacob_Lon - lon) * (BusCoordinates.Jacob_Lon - lon));
            return new double[] {dWeba , dJacob};
        }
    }
}
