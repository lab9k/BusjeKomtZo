using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Busje_komt_zo.Classes.Model;
using Busje_komt_zo.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Busje_komt_zo.Classes
{
    /// <summary>
    /// PredictionProvider based on experimental data
    /// </summary>
    public class ExperimentalProvider : IPredictionProvider
    {
        private readonly List<PredictionMatch> _jacobToWeba;
        private readonly List<PredictionMatch> _webaToJacob;

        public ExperimentalProvider()
        {
            _jacobToWeba = ParseCsv("JacobToWeba.csv", BusStop.Weba);
            _webaToJacob = ParseCsv("WebaToJacob.csv", BusStop.Jacob);
        }



        public PredictionMatch GetTimeTillArrival(BusCoordinates currentBusCoordinates, BusStop LastStop)
        {
            List<PredictionMatch> toEvaluate = LastStop == BusStop.Jacob ? _jacobToWeba : _webaToJacob;
            double min = GetDistance(currentBusCoordinates, toEvaluate[0].Latitude, toEvaluate[0].Longitude);
            PredictionMatch closests = toEvaluate[0];

            for (int i = 1; i < toEvaluate.Count; i++)
            {
                double currentDistance = GetDistance(currentBusCoordinates, toEvaluate[i].Latitude, toEvaluate[i].Longitude);
                if (currentDistance < min)
                {
                    min = currentDistance;
                    closests = toEvaluate[i];
                }
            }
            return closests;

        }

        private double GetDistance(BusCoordinates from, double latTo, double longTo)
        {
            return Math.Sqrt((from.Latitude - latTo) * (from.Latitude - latTo) +
                             (from.Longitude - longTo) * (from.Longitude - longTo));
        }


        private List<PredictionMatch> ParseCsv(string fileName,BusStop destination)
        {
            try
            {
                List<PredictionMatch> result = new List<PredictionMatch>();
                string[] lines = System.IO.File.ReadAllLines(fileName);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(';');
                    result.Add(new PredictionMatch
                    {
                        Destination = destination,
                        Latitude = double.Parse(data[0], CultureInfo.InvariantCulture),
                        Longitude = double.Parse(data[1], CultureInfo.InvariantCulture),
                        MinutesTillArrival = int.Parse(data[2])
                    });
                }
                return result;
            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
