using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
    public class BusLine:IComparable<BusLine>
    {    
        public int BusLineNumber { get; set; }// the bus line number
        public BusLineStation FirstStation { get; set; }// the first station 
        public BusLineStation LastStation { get; set; }// the last station
        public places Area { get; set; }// the area of the bus line
        public List<BusLineStation> Stations { get; set; }// list of all the stations in the bus line

        public BusLine()// constractor
        {

        }

        public int ReciveNumStation()// get the number station by string and return it by int
        {
            string s = Console.ReadLine();
            int tryToInt = 0;
            bool b = int.TryParse(s, out tryToInt);
            int intVersion = -1;
            if (b)
            {
                intVersion = int.Parse(s);
            }
            return intVersion;
        }
        public override string ToString()// return string of the information about the bus line
        {
            return "BusLinNumber:" + BusLineNumber + "Area:" + Area + "Stations:" + Stations.ToString();
        }
        public void AddStation(BusLineStation other)//the func gets a buss line and adding it to the stations's list
        {
            //Console.WriteLine("Enter bus station number that you want to add:");
            //int stationNum = ReciveNumStation();
            Console.WriteLine("Enter the index of the station number you want to add:");
            int stationIndex = System.Console.Read();
            foreach (var item in Stations)
            {
                if (item.BusStationKey == other.BusStationKey)
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
        public void DeleteStation()// delete station of the list
        {
            Console.WriteLine("Enter bus station number that you want to delete:");
            int stationNum = ReciveNumStation();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == stationNum && stationNum != -1)
                {
                    Stations.Remove(Stations[i]);
                }
            }

        }
        public bool FindStation()// return true if the station exist in the list
        {
            Console.WriteLine("Enter bus station number that you want to find:");
            int stationNum = ReciveNumStation();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == stationNum && stationNum != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public double DistanceBetweenTwoStations()// calculate the distance between two stations
        {
            int stationIndex1 = 0, stationIndex2 = 0;
            Console.WriteLine("Enter the two station number's of the stations you want to know what the distance between them:");
            int stationNum1 = ReciveNumStation();
            int stationNum2 = ReciveNumStation();
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == stationNum1)
                {
                    stationIndex1 = i;
                }
            }
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == stationNum2)
                {
                    stationIndex2 = i;
                }
            }
            return Stations[stationIndex1 - 1].distanceCalculation(Stations[stationIndex2 - 1]);
        }

        public BusLine CraeteSubRouteBusLine(BusStation b1, BusStation b2)// create sub bus line with the sub route of main bus line
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
            SubRouteLine.Area = Area;
            return SubRouteLine;
        }
     

        public int CompareTo(BusLine other)// return 0 if the time of the two bus lines is equals, return -1 if the time of the other bus line is bigger, else return 1
        {
            TimeSpan timeBusLine1 = new TimeSpan(0, 0, 0);
            TimeSpan timeBusLine2 = new TimeSpan(0, 0, 0); 
            foreach (var item in Stations)
            {
                timeBusLine1 += item.Time;
            }
            foreach (var item in other.Stations)
            {
                timeBusLine2 += item.Time;
            }
            if (timeBusLine1 == timeBusLine2)
            {
                return 0;
            }
               
            else if (timeBusLine1 < timeBusLine2)
            {
                return -1;
            }

            else
            {
                return 1;
            }               
        }
    }

    public class BusLineException : Exception
    {
        public BusLineException():base("custom message")
        {

        }
    }

    public class BusLineException2 : Exception
    {
        public BusLineException2(string message) : base(message)
        {

        }
    }
}

