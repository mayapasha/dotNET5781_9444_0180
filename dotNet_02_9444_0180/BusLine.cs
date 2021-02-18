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
        public BusLineStation FirstStation { get {return Stations[0]; } }// the first station 
        public BusLineStation LastStation { get { return Stations[Stations.Count-1]; } }// the last station
        public Places Area { get; set; }// the area of the bus line
        public List<BusLineStation> Stations { get; set; }// list of all the stations in the bus line

        public BusLine()// constractor
        {
            
        }
         
        public int ReciveNumStation()// get the number station by string and return it by int
        {
            string s = Console.ReadLine();
            int tryToInt = 0;
            bool b = int.TryParse(s, out tryToInt);
            int intVersion = 0;
            if (b)
            {
                intVersion = int.Parse(s);
            }
            else
            {
                throw new FormatException();
            }
            return intVersion;
        }
        public override string ToString()// return string of the information about the bus line
        {
            string s = "";
            foreach (var item in Stations)
            {
                s += '\n' + item.ToString();
            }
            return  "BusLineNumber: " + BusLineNumber + '\n'+" Area: " + Area + '\n' + " Stations: " + '\n' + s + '\n';
        }
        public void AddStation(BusLineStation other)//the func gets station and adding it to the stations's list
        {
            Console.WriteLine("Enter the index of the station number you want to add between " + 0 + " to " + Stations.Count);
            int stationIndex = ReciveNumStation();
            if(stationIndex<0||stationIndex>Stations.Count)
            {
                throw new IndexOutOfRangeException();
            }
            foreach (var item in Stations)
            {
                if (item.BusStationKey == other.BusStationKey)
                {
                    throw new BusLineException();
                }
            }
            Stations.Insert(stationIndex - 1, other);
        }
        public void DeleteStation(int stationNum)// delete a station from the list stations
        { 
            for (int i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey == stationNum )
                {
                    Stations.Remove(Stations[i]);
                    return;
                }
            }
            throw new IndexOutOfRangeException();
        }
        public bool FindStation(int stationNum)// return true if the station exist in the list
        {
          
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

        public BusLine CraeteSubRouteBusLine(BusStation b1, BusStation b2)// create sub bus line between two stations that we get and return it
        {
            BusLine SubRouteLine = new BusLine();
            SubRouteLine.Stations = new List<BusLineStation>();
            int index1 = -1, index2 = -1;
            for (int i = 0; i < Stations.Count; i++)// find the index of the the first station
            {
                if (Stations[i].BusStationKey == b1.BusStationKey)
                {
                    index1 = i;
                }
            }
            if (index1 == -1)// if the station did not found
            {
                throw new FindStationIndexExeption();
            }
            for (int i = 0; i < Stations.Count; i++)// find the index of the the last station
            {
                if (Stations[i].BusStationKey == b2.BusStationKey)
                {
                    index2 = i;
                }
            }
            if (index2 == -1)// if the station did not found
            {
                throw new FindStationIndexExeption();

            }
            int tmp = index1;
            index1 = Math.Min(tmp, index2);
            index2 = Math.Max(tmp, index2);
            for (int i = index1, j = index2; i <= j; i++)// add the stations between the two stations to the list of the bus line that we return
            {
                SubRouteLine.Stations.Add(Stations[i]);
            }           
            SubRouteLine.Area = Area;
            SubRouteLine.BusLineNumber = BusLineNumber;
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
    // exceptions classes:
    public class BusLineException : Exception
    {
        public BusLineException():base ("Can not repeat station in bus line")
        {
           
        }
       
    }
    public class FindStationIndexExeption:Exception
    {
        public FindStationIndexExeption() : base("Can not find station on the station list ")
        {

        }
    }
    
}

