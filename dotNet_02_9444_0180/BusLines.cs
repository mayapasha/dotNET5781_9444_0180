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
            try
            {
                other.Area = (places)areaChoies;
            }
            catch(Exception ex)
            {
                throw new BusLineException();
            }
            
            other.FirstStation = other.Stations[0];
            other.LastStation = other.Stations[other.Stations.Count - 1];
            lines.Add(other);
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
