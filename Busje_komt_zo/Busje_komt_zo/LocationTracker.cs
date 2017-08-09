using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Busje_komt_zo
{
    public class LocationTracker
    {
        //List that contains the 3 last locations
        private List<BusCoordinates> latestCoordinates;

        //Array that will be used for circular data storage
        private Orientation[] previousOrientations;
        private int index;

        public LocationTracker()
        {
            latestCoordinates = new List<BusCoordinates>();
            previousOrientations = new Orientation[5];
            index = 0;
        }

        public void Add(BusCoordinates coord)
        {
            latestCoordinates.Add(coord);
            if (latestCoordinates.Count > 3)
            {
                latestCoordinates.RemoveAt(0); //remove first element if there are more than 3 elements
            }
        }

        public Orientation GetOrientation()
        {
            if (latestCoordinates.Count >= 3)
            {
                MovementComparer comparer = new MovementComparer();
                int previous =  comparer.Compare(latestCoordinates[0],
                    latestCoordinates[2]); // compare the last and first element in the list 
                SetLastOrientation(GetOrientationValue(previous));

                return GetMostPropableOrientation();
            }
            else
            {
                return Orientation.CALCULATING;
            }
        }

        public void SetLastOrientation(Orientation orientation)
        {
            previousOrientations[index++] = orientation;
            index = index % previousOrientations.Length;
        }

        public Orientation GetMostPropableOrientation()
        {
            Dictionary<Orientation, int> frequencies = new Dictionary<Orientation, int>();
            foreach (Orientation o in previousOrientations)
            {
                if (frequencies.ContainsKey(o))
                    frequencies[o] = frequencies[o] + 1;
                else
                    frequencies[o] = 1;
            }

            Orientation mostProbable = previousOrientations[(index + previousOrientations.Length - 1) % previousOrientations.Length]; //last orientation gets the priority
            int max = frequencies[mostProbable];

            foreach (Orientation o in frequencies.Keys)
            {
                if (o != mostProbable && frequencies[o] > max)
                {
                    max = frequencies[o];
                    mostProbable = o;
                }
            }
            return mostProbable;
        }

        public string GetMovementMessage()
        {
            try
            {
                Orientation status = GetOrientation();
                if (status == Orientation.CENTRE)
                {
                    return "Busje is onderweg naar Sint-Jacobs";
                }
                else if (status == Orientation.WEBA)
                {
                    return "Busje is onderweg naar P+R Weba-Decathlon";
                }
                else if(status == Orientation.STATIONARY)
                {
                    return "Het busje staat eventjes stil";
                }
                else if (status == Orientation.CALCULATING)
                {
                    return "Bezig met berekenen, eventjes geduld";
                }
                else
                {
                    return "Er is een fout opgetreden, probeer het later opnieuw";
                }
            }
            catch (NotImplementedException ex)
            {
                return ex.Message;
            }
        }

        private Orientation GetOrientationValue(int value)
        {
            switch (value)
            {
                case 0:
                    return Orientation.STATIONARY;
                case 1:
                    return Orientation.CENTRE;
                case -1:
                    return Orientation.WEBA;
                default:
                    return Orientation.UNKNOWN;

            }
        }
    }

    public enum Orientation
    {
        WEBA,CENTRE,STATIONARY,UNKNOWN,CALCULATING
    }
}
