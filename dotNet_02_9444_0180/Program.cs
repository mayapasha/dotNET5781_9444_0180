using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
    enum places { General,North,South,Center,Jerusalem};
    class Program
    {
        static void Main(string[] args)
        {

            BusLines LinesCollection = new BusLines();
            BusLineStation[] allStations = new BusLineStation[40];

            string choise = null;
            for (int i = 0; i < 40; i++)
            {
                allStations[i].BusStationKey = i+1;
            }
            for (int i = 0; i < 9; i++)
            {

                BusLine other = new BusLine() { BusLineNumber = i + 41, Area = places.General };
                if (i < 9)
                {
                    for (int j = 0 + i, k = 0; k < 10 || j < 40; j += 1 + i, k++)
                    {
                        other.Stations.Insert(k, allStations[j]);
                    }
                }
                else
                {
                    other.Stations.Insert(9, allStations[10]);//10,12,16,18,21,22,25,28,30,32,33,36,37,38
                    other.Stations.Insert(9, allStations[12]);
                    other.Stations.Insert(9, allStations[16]);
                    other.Stations.Insert(9, allStations[18]);
                    other.Stations.Insert(9, allStations[21]);
                    other.Stations.Insert(9, allStations[22]);
                    other.Stations.Insert(9, allStations[25]);
                    other.Stations.Insert(9, allStations[28]);
                    other.Stations.Insert(9, allStations[30]);
                    other.Stations.Insert(9, allStations[32]);
                    other.Stations.Insert(9, allStations[33]);
                    other.Stations.Insert(9, allStations[36]);
                    other.Stations.Insert(9, allStations[37]);
                    other.Stations.Insert(9, allStations[38]);
                }
                    LinesCollection.lines.Insert(i, other);
            }
            
            do
            {

                Console.WriteLine("press");
                Console.WriteLine("a: to add ");
                Console.WriteLine("d: to delete ");
                Console.WriteLine("s: to search ");
                Console.WriteLine("p: to print ");
                Console.WriteLine("e: to exit ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "a":
                        Console.WriteLine("press");
                        Console.WriteLine("1: to add a new bus line ");
                        Console.WriteLine("2: to add a station to a bus line ");
                        choise = Console.ReadLine();
                        switch (choise)
                        {
                            case "1":
                                LinesCollection.AddBusLine(allStations);
                                break;
                            case "2":

                                break;
                            default:
                                break;
                        }
                        break;
                    case "d":
                        break;
                    case "s":
                        break;
                    case "p":
                        break;
                    default:
                        break;
                }
            } while (choise != "e");
        }
    }
}
