using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
   public class BusLines : IEnumerable
    {
        public List<BusLine> lines { get; set; }




        public void AddBusLine(BusLineStation[] stationsCollection)// the func gets an array of bus line stations and add a new bus 
        {
            BusLine other = new BusLine();
            Console.WriteLine("please enter the new bus number:");
            other.BusLineNumber = ReciveStringToInt();
            int counter = 0;
            int indexBus = 0;
            foreach (BusLine item in lines)// check how many times the line exist
            {
                if (item.BusLineNumber == other.BusLineNumber)
                {
                    indexBus = FindIndex(item);
                    counter += 1;
                }
            }

            if (counter == 2)// if the line exist 2 times
            {
                throw new AddExistBuslineException();
            }
            int indexStations = 0;
            bool reqouest = true;
            other.Stations = new List<BusLineStation>();
            while (reqouest == true)//adding stations until the user want to stop
            {
                Console.WriteLine("from 1-40 :which station you want to add?|press 0 to stop adding stations");
                indexStations = ReciveStringToInt();
                if (indexStations == 0)
                {
                    reqouest = false;
                }
                else
                {
                    foreach (var item in other.Stations)//find if the station allreday exist
                    {
                        if (item.BusStationKey == indexStations)
                        {
                            throw new BusLineException();
                        }
                    }
                    other.Stations.Add(stationsCollection[indexStations - 1]);
                }
            }

            if (counter == 1)//if the line exsit one time 
            {

                if ((other.FirstStation != lines[indexBus].LastStation) || (other.LastStation != lines[indexBus].FirstStation))// check if the bus line to add is the revere bus line
                {
                    throw new AddWrongBuslineExceptiton();
                }

            }
            Console.WriteLine("which area you want your bus line?");
            Console.WriteLine("prees 0:to General 1:to North 2:to South 3:to Center 4: to Jerusalem");
            int areaChoies = ReciveStringToInt();
            if (areaChoies > 4 || areaChoies < 0)
            {
                throw new IndexOutOfRangeException();
            }
            other.Area = (Places)areaChoies;
            lines.Add(other);
        }

        public void DeleteBusLine()// the func deletes a bus line
        {
            Console.WriteLine("please enter the bus number you want to delete:");
            int idBusToDelete = ReciveStringToInt();
            foreach (var item in lines)//sreach the bus line that the user want to delete
            {
                if (item.BusLineNumber == idBusToDelete)
                {
                    foreach (var item1 in item.Stations)// check all the station if there is some station that can not be deleted
                    {
                        TryDeleteStation(item1.BusStationKey);
                    }
                    lines.Remove(item);
                    return;
                }               
            }
            throw new IndexOutOfRangeException();
        }

        public List<BusLine> SearchAllBusLineStation()// the func get from the user a station and return list of all the buses that pass this station
        {
            Console.WriteLine("please enter the bus station number you want to seach:");
           
            int idStationToFind = ReciveStringToInt();
            int counter = 0;
            List<BusLine> theReturnList = new List<BusLine>();
            foreach (var item in lines)// pass all the buses
            {
                foreach (var itemStation in item.Stations)// pass all the stations in each bus
                {
                    if (itemStation.BusStationKey == idStationToFind)// if the station found add the bus to the return list
                    {
                        counter++;
                        theReturnList.Add(item);
                    }
                }
            }
            if (counter == 0)// throw exception if there are no buses that pass this station 
            {
                throw new EmptyStationException(); 
            }
            return theReturnList;
        }

        public int FindIndex(BusLine current)// find the index of the bus line in the list
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

        public List<BusLine> Sort()//the func sort the bus line list by the fastest to the slowest
        {
            lines.Sort();
            return lines;
        }

        public BusLine this[int i]//indexer
        {
            get { if (i >= lines.Count) { throw new IndexOutOfRangeException(); }; return lines[i]; }
            set { if (i >= lines.Count) { throw new IndexOutOfRangeException(); }; lines[i] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            IEnumerator e = new Ibuslineinumerator(lines);
            return e;
        }

        public void addStationToBusLine(BusLineStation[] stationsCollection)//the func get array of the existed stations and add a station to the bus line that the user set
        {
            Console.WriteLine("Enter the bus line number you want to add it the satation");
            int idBusToAddStation = ReciveStringToInt();
            bool flag = false;
            foreach (var item in lines)// check if the bus line exist
            {
                if (idBusToAddStation == item.BusLineNumber)
                    flag = true;
            }
            if(flag==false)
            {
                throw new NotExistBuslineException();
            }
            Console.WriteLine("from 1-40 :which station you want to add?");
            int indexStations = ReciveStringToInt();
            foreach (var item in lines)// add the station to the bus
            {
                if(item.BusLineNumber==idBusToAddStation)
                {
                    item.AddStation(stationsCollection[indexStations-1]);
                }
            }

        }

        public void DeleteStationFronBusLine()//delete a station from bus line that the user set 
        {
            Console.WriteLine("Enter the bus line number that you want to delete a station from:");
            int intVersion = ReciveStringToInt();
            Console.WriteLine("Enter bus station number that you want to delete:");
            int stationNum = ReciveStringToInt();
            foreach (var item in lines)// serch if the line exist 
            {
                if (item.BusLineNumber == intVersion)
                {
                    if (TryDeleteStation(stationNum))// check if the station can be deleted
                    {
                        item.DeleteStation(stationNum);
                        return;
                    }
                }
            }
            throw new NotExistBuslineException();
        }

        public int ReciveStringToInt()//the func get a string and return it by int
        {
            string input = Console.ReadLine();
            int tryToInt = 0;
            bool b = int.TryParse(input, out tryToInt);
            int intVersion = -1;
            if (b)
            {
                return intVersion = int.Parse(input);
            }
            else
            {
                throw new FormatException("did not enter number!");
            }

        }

        public void PrintTheOptionsToDrive()// the func get from the user 2 stations and print all the paths from the fastest to the slowest 
        {
            BusLines MyBuses = new BusLines();
            MyBuses.lines = new List<BusLine>();
            Console.WriteLine("Enter the station number that you want to start from:");
            int StartStationKEY = ReciveStringToInt();
            Console.WriteLine("Enter the station number that you want to stop there:");
            int LastStationKEY = ReciveStringToInt();        
            int indexToHelp = 0;
            int indexToHelp2 = 0;
            foreach (var item in lines)// pass all the lines
            {
                indexToHelp = 0;
                indexToHelp2 = 0;
                foreach (var item1 in item.Stations)// pass all the stations in each bus
                {                   
                    
                    if (item1.BusStationKey == StartStationKEY)// check if the first station exist
                    {                                                          
                        foreach (var item2 in item.Stations)// check if the last station is after the first one
                        {
                            if(indexToHelp2>indexToHelp)
                            {
                                if(item2.BusStationKey==LastStationKEY)
                                {
                                    MyBuses.lines.Add(item.CraeteSubRouteBusLine(item1, item2));// add this route to the list the we print
                                }
                            }
                                indexToHelp2++; 
                        }                        
                    }
                     indexToHelp++;                    
                }
            }
            if(MyBuses.lines.Count==0)// if there is no way
            {
                throw new NotExistBuslineException();
            }
            MyBuses.Sort();
            IEnumerator NewIenumerator = MyBuses.GetEnumerator();
            while (NewIenumerator.MoveNext())// print
            {
                Console.WriteLine(NewIenumerator.Current);
            }
        }
        public void PrintAllBuses()// print all buses on the lines
        {
            IEnumerator NewIenumerator = lines.GetEnumerator();
            while (NewIenumerator.MoveNext())
            {
                Console.WriteLine(NewIenumerator.Current);
            }
        }
        public void PrintTheStations(BusLineStation[] b)// print all the buses that pass in each station
        {
            for (int i = 0; i < 40; i++)
            {
                Console.WriteLine("the lines that pass in station number "+b[i].BusStationKey+  " are:");
                foreach (var item in lines)
                {                    
                    if(item.FindStation(b[i].BusStationKey))
                    {   
                        Console.WriteLine(item);
                    }
                }
            }
        }
        public void PrintBusLinesInStation()// print all the buses that pass in spesific station
        {
            List <BusLine> other = new List<BusLine>();
            other = SearchAllBusLineStation();
            IEnumerator NewIenumerator = other.GetEnumerator();
            while (NewIenumerator.MoveNext())
            {
                Console.WriteLine(NewIenumerator.Current);
            }
        }
       public bool TryDeleteStation(int otherStationKey)// get station key and return true if the station exist at least in two bus lines
        {
            int counter = 0;
            foreach (var item in lines)
            {
                foreach (var item1 in item.Stations)
                {
                    if(item1.BusStationKey== otherStationKey)
                    {
                        counter += 1;
                    }
                }
            }
            if(counter<=1)
            {
                throw new DeleteException();
            }
            return true;
            
        }
        
    }
    // excepsions classes:
    public class AddExistBuslineException : Exception
    {
        public AddExistBuslineException() : base("Can not add exist bus line")
        {
        }
    }
    public class NotExistBuslineException : Exception
    {
        public NotExistBuslineException() : base(" bus not exists")
        {
        }
    }

    public class EmptyStationException:Exception
    {
        public EmptyStationException():base("There are no bus lines that pass this station")
        {

        }
    }

    public class AddWrongBuslineExceptiton : Exception
    {
        public AddWrongBuslineExceptiton() : base("Can not add this bus the information is wrong")
        {

        }
    }
    public class DeleteException : Exception
    {
        public DeleteException() : base("Can not delete this information")
        {

        }
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
