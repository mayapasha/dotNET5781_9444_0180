using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_9444_0180
{
    class Program
    {

        public static Random r = new Random();
        static void Main(string[] args)
        {
            DateTime date;
            int val;
            BussList b1 = new BussList();
            Buss b;
          
            //string License;
            string startnum;
            string License;
            string ch;
            bool flag=true;
            do
            {
                Console.WriteLine("a: to add a bus");
                Console.WriteLine("b: to choose a buss for driving");
                Console.WriteLine("c: to refulling or give Treatment");
                Console.WriteLine("d: to see the this Mileage");
                Console.WriteLine("e: to exit");
                ch = Console.ReadLine();

                switch (ch)
                {
                    case "a":
                        do {
                            Console.WriteLine("Enter the license number:");
                            License = Console.ReadLine();
                            Console.WriteLine("Enter the starting date of the buss:");
                            startnum = Console.ReadLine();
                            DateTime.TryParse(startnum, out date);
                            if (date.Year >= 2018 && License.Length != 10 || date.Year < 2018 && License.Length != 9)
                            {
                                Console.WriteLine("ERROR");
                                flag = false;
                            }
                            else
                            {
                                flag = true;
                            }
                        } while (flag == false);
                        b1.AddNewBuss(License, date);
                        break;
                    case "b":
                        Console.WriteLine("Enter the license number:");
                        License = System.Console.ReadLine();
                        val = r.Next(0, 1200);
                        Console.WriteLine("Travel length:"+val);
                        b = b1.FindBuss(License);//searching for the chosen buss 
                        if (b != null)// the buss not in the list
                        {
                            if (b.ReadyForDrive(val))//the chosen buss can do the drive
                                Console.WriteLine("Success");
                            else
                                Console.WriteLine("Cant go to drive");
                        }
                        else
                            Console.WriteLine("Could not be found");
                        break;
                    case "c":
                        Console.WriteLine("Enter the license number:");
                        License = System.Console.ReadLine();
                        b = b1.FindBuss(License);
                        if (b != null)
                        {
                            Console.WriteLine("enter t: for treatment");
                            Console.WriteLine("enter r: to refuel");
                            string choise = System.Console.ReadLine();
                            switch (choise)
                            {
                                case "t":
                                    b.treatment();
                                    Console.WriteLine("success");
                                    break;
                                case "r":
                                    b.Refuel();
                                    Console.WriteLine("success");
                                    break;
                                default:
                                    Console.WriteLine("ERROR");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Could not be found");
                        break;
                    case "d":
                        b1.PrintAllBusses();
                        break;
                    case "e":
                        Console.WriteLine("bye");
                        break;
                    default:
                        break;
                }
            } while (ch != "e");


        }
    }
}
