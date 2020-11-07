using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
    class BusLine
    {
        enum MyEnum { General, North, South, Center, Jerusalem };

        public int BusLineNumber { get; set; }
        public BusLineStation FirstStation { get; set; }
        public BusLineStation LastStation { get; set; }
        public int Area { get; set; }
        public List<BusLineStation> Stations { get; set; }

        public BusLine()
        {

        }

        public int ReciveNumStation()
        {
            string s = Console.ReadLine();
            int x = 0;
            bool b = int.TryParse(s, out x);
            int y = -1;
            if (b)
            {
                y = int.Parse(s);
            }
            return y;
        }
        public override string ToString()
        {
            return "BusLinNumber:" + BusLineNumber + "Area:" + Area + "Stations:" + Stations.ToString();
        }
        public void AddStation(BusLineStation other)//the func gets a buss line and adding it to the stations's list
        {
            Console.WriteLine("Enter bus station number that you want to add:");
            int y = ReciveNumStation();
            Console.WriteLine("Enter the station number for the new station:");
            int stationIndex = System.Console.Read();
            foreach (var item in Stations)
            {
                if (item.BusStationKey == y)
                {
                    Console.WriteLine("ERROR");
                    return;
                }
            }
            if (stationIndex == 1)
            {
                FirstStation = other;
            }
            else if (stationIndex == Stations.Count)
            {
                LastStation = other;
            }
            Stations.Insert(stationIndex - 1, other);
        }
        public void DeleteStation()
        {
            Console.WriteLine("Enter bus station number that you want to delete:");
            int y = ReciveNumStation();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == y && y != -1)
                {
                    Stations.Remove(Stations[i]);
                }
            }

        }
        public bool FindStation()
        {
            Console.WriteLine("Enter bus station number that you want to find:");
            string s = Console.ReadLine();
            int x = 0;
            bool b = int.TryParse(s, out x);
            int y = -1;
            if (b)
            {
                y = int.Parse(s);
            }
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == y && y != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public double DistanceBetweenTwoStations()
        {
            int stationIndex1 = 0, stationIndex2 = 0;
            Console.WriteLine("Enter the two station number's of the stations you want to know what the distance between them:");
            int y = ReciveNumStation();
            int x = ReciveNumStation();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == y)
                {
                    stationIndex1 = i;
                }
            }
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == y)
                {
                    stationIndex2 = i;
                }
            }
            return Stations[stationIndex1 - 1].distanceCalculation(Stations[stationIndex2 - 1]);
        }

        public BusLine CraeteSubRouteBusLine(BusStation b1, BusStation b2)
        {
            BusLine SubRouteLine = new BusLine();
            int index1 = -1, index2 = -1;
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == b1.BusStationKey)
                {
                    index1 = i;
                }
            }
            if (index1 == -1)
            {
                Console.WriteLine("ERROR");
                return null;
            }
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == b1.BusStationKey)
                {
                    index2 = i;
                }
            }
            if (index2 == -1)
            {
                Console.WriteLine("ERROR");
                return null;

            }
            int tmp = index1;
            index1 = Math.Min(index1, index2);
            index2 = Math.Max(tmp, index2);
            for (int i = index1, j = index2, k = 0; i <= j; i++, k++)
            {
                SubRouteLine.Stations[k] = Stations[i];
            }
            SubRouteLine.FirstStation = Stations[index1];
            SubRouteLine.LastStation = Stations[index2];
            SubRouteLine.Area = Area;// לבדוק
            return SubRouteLine;
        }

    }
}

