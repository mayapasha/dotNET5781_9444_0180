using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Lihi Barlev 324129444
//Maya Pasha 322290180
namespace dotNet_02_9444_0180
{
    public enum Places { General,North,South,Center,Jerusalem};
    class Program
    {
        static void Main(string[] args)
        {


            BusLines LinesCollection = new BusLines();
            
            BusLineStation[] allStations = new BusLineStation[40];
            string choise = null;

            for (int i = 0; i < 40; i++)// set information to stations array
            {
                allStations[i] = new BusLineStation();
                allStations[i].BusStationKey = i + 1;

            }
            LinesCollection.lines = new List<BusLine>();
            for (int i = 0; i < 10; i++)// set info to bus lines list
            {
                BusLine other = new BusLine() { BusLineNumber = i + 41, Area = Places.General };
                other.Stations = new List<BusLineStation>();
                if (i < 9)
                {
                    for (int j = 0 + i, k = 0; k < 10 && j < 40; j += 1 + i, k++)
                    {
                        other.Stations.Add(allStations[j]);
                    }
                }
                else
                {
                    other.Stations.Add(allStations[10]);
                    other.Stations.Add(allStations[12]);
                    other.Stations.Add(allStations[16]);
                    other.Stations.Add(allStations[18]);
                    other.Stations.Add(allStations[21]);
                    other.Stations.Add(allStations[22]);
                    other.Stations.Add(allStations[25]);
                    other.Stations.Add(allStations[28]);
                    other.Stations.Add(allStations[30]);
                    other.Stations.Add(allStations[32]);
                    other.Stations.Add(allStations[33]);
                    other.Stations.Add(allStations[36]);
                    other.Stations.Add(allStations[37]);
                    other.Stations.Add(allStations[38]);
                }
                LinesCollection.lines.Add(other);
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
                try
                {
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
                                    Console.WriteLine("SUCCESS!");

                                    break;
                                case "2":
                                    LinesCollection.addStationToBusLine(allStations);
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "d":
                            Console.WriteLine("press");
                            Console.WriteLine("1: to delete a bus line ");
                            Console.WriteLine("2: to delete a station from a bus line ");
                            choise = Console.ReadLine();
                            switch (choise)
                            {
                                case "1":
                                    LinesCollection.DeleteBusLine();
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                case "2":
                                    LinesCollection.DeleteStationFronBusLine();
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                default:
                                    break;
                            }

                            break;
                        case "s":
                            Console.WriteLine("press");
                            Console.WriteLine("1: to seach bus lines that pass near spesific station: ");
                            Console.WriteLine("2: to print all options to drive between two stations: ");
                            choise = Console.ReadLine();
                            switch (choise)
                            {
                                case "1":
                                    LinesCollection.PrintBusLinesInStation();
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                case "2":
                                    LinesCollection.PrintTheOptionsToDrive();
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "p":
                            Console.WriteLine("press");
                            Console.WriteLine("1: to print all the buses: ");
                            Console.WriteLine("2: to print all the station and the lines that pass them: ");
                            choise = Console.ReadLine();
                            switch (choise)
                            {
                                case "1":
                                    LinesCollection.PrintAllBuses();
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                case "2":
                                    LinesCollection.PrintTheStations(allStations);
                                    Console.WriteLine("SUCCESS!");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Did not enter a number, please enter a number next time!");
                }
                catch (BusLineException)
                {
                    Console.WriteLine("Can not repeat station in bus line");
                }
                catch (FindStationIndexExeption)
                {
                    Console.WriteLine("Can not find station on the station list ");
                }
                catch (AddExistBuslineException)
                {
                    Console.WriteLine("Can not add existed bus line");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The number out of the range");
                }
                catch (EmptyStationException)
                {
                    Console.WriteLine("There are no bus lines that pass this station");
                }

                catch (AddWrongBuslineExceptiton)
                {
                    Console.WriteLine("Can not add this bus the information is wrong");
                }
                catch(NotExistBuslineException)
                {
                    Console.WriteLine(" bus not exists");
                }
                catch(DeleteException)
                {
                    Console.WriteLine("Can not delete this information");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR");
                }

            } while (choise != "e");

          
           
        }
    }
}
