using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
    class BusLines : IEnumerable
    {
        public List<BusLine> lines { get; set; }
        public int size { get; set; }

        public void AddBusLine(BusLineStation[] stationsCollection)
        {
            BusLine other = new BusLine();
            Console.WriteLine("please enter the new bus number:");
            other.BusLineNumber = Console.Read();
            int counter = 0;
            foreach (BusLine item in lines)
            {
                if (item.BusLineNumber == other.BusLineNumber)
                {
                    counter++;
                }
            }
            if (counter == 2)
            {
                return;//זריקת חריגה
            }
            int indexStations = 0;
            bool reqouest = true;
            while (reqouest == true)
            {
                Console.WriteLine("from 1-40 :which station you want to add?|press 0 to stop adding stations");
                indexStations = Console.Read();
                if (indexStations == 0)
                {
                    reqouest = false;
                }
                else
                {
                    other.AddStation(stationsCollection[indexStations - 1]);
                }
            }
            Console.WriteLine("which area you want your bus line?");
            Console.WriteLine("prees 1:to General 2:to North 3:to South 4:to Center 5: to Jerusalem");
            int areaChoies = Console.Read();
            //try
            //{
            //    other.Area = (places)areaChoies;
            //}
            //catch(Exception ex)
            //{
            //    throw new BusLineException();
            //}
            other.Area = (places)areaChoies;
            other.FirstStation = other.Stations[0];
            other.LastStation = other.Stations[other.Stations.Count - 1];
            if (counter == 1)
            {
                int indexBus = FindIndex(other);
                if (!(other.FirstStation == lines[indexBus].LastStation) && (other.LastStation == lines[indexBus].FirstStation))
                {
                    //זריקת חריגה!
                }

            }
            lines.Add(other);
        }

        public void DeleteBusLine()
        {
            Console.WriteLine("please enter the bus number you want to delete:");
            string s = Console.ReadLine();
            int tryToInt = 0;
            bool b = int.TryParse(s, out tryToInt);
            int intVersion = -1;
            if (b)
            {
                intVersion = int.Parse(s);
            }
            else
            {
                //זריקת חריגה
            }
            int idBusToDelete = intVersion;
            foreach (var item in lines)
            {
                if (item.BusLineNumber == idBusToDelete)
                {
                    lines.Remove(item);
                }
            }
        }

        public List<BusLine> printAllBusLineStation()
        {
            Console.WriteLine("please enter the bus station number you want to print:");
            string s = Console.ReadLine();
            int tryToInt = 0;
            bool b = int.TryParse(s, out tryToInt);
            int intVersion = -1;
            if (b)
            {
                intVersion = int.Parse(s);
            }
            else
            {
                //זריקת חריגה
            }
            int idStationToFind = intVersion;
            int counter = 0;
            List<BusLine> theReturnList = new List<BusLine>();
            foreach (var item in lines)
            {
                foreach (var itemStation in item.Stations)
                {
                    if (itemStation.BusStationKey == idStationToFind)
                    {
                        counter++;
                        theReturnList.Add(item);
                    }
                }
            }
            if (counter == 0)
            {
                //זרוק חריגה
            }
            return theReturnList;
        }

        public int FindIndex(BusLine current)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].BusLineNumber == current.BusLineNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<BusLine> Sort()
        {
            lines.Sort();
            return lines;
        }

        public BusLine this[int i]//indexer
        {
            get { if (i >= lines.Count) {/*זריקת חריגה*/ }; return lines[i]; }
            set { if (i >= lines.Count) {/*זריקת חריגה*/ }; lines[i] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            IEnumerator e = new Ibuslineinumerator(lines);
            return e;
        }


        //public IEnumerator GetEnumerator()
        //{
        //    var IEnumerator = new BusLineEnumerator(lines);
        //    return IEnumerator;
        //}

        //System.Collections.IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator<BusLine> IEnumerable<BusLine>.GetEnumerator()
        //{
        //    var IEnumerator = new BusLineEnumerator(lines);
        //    return IEnumerator;
        //}
    }

    //class BusLineEnumerator : IEnumerator
    //{
    //    public object Current
    //    {
    //        get{
    //            return Lines[CurrentIndex];
    //        }
    //    }


    //    public List<BusLine> Lines { get; set; }

    //    public int CurrentIndex = -1;


    //    public BusLineEnumerator(List<BusLine> lines)
    //    {
    //        Lines = lines;
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public bool MoveNext()
    //    {
    //       if(Lines[++CurrentIndex]!=null)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public void Reset()
    //    {
    //        CurrentIndex = -1;
    //    }
    //}
}    
