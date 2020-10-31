using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_01_9444_0180
{
    
    class Program
    {
        static void Main(string[] args)
        {

            BussList b1;
            Buss b;
            char ch;
            string License;
            string startnum;
            Console.WriteLine("a: to add a bus");
            Console.WriteLine("b: to choose a buss for driving");
            Console.WriteLine("c: to refulling or give Treatment");
            Console.WriteLine("d: to see the this Mileage");
            Console.WriteLine("e: to exit");

            do
            {
                ch= (char)System.Console.Read();

                switch (ch)
                {
                    case 'a':
                    Console.WriteLine("Enter the license number:");
                    License=System.Console.ReadLine();
                    Console.WriteLine("Enter the starting date of the buss:");
                    startnum=System.Console.ReadLine();
                    b1.addNewBuss(License,startnum);
                        break;
                    case 'b':
                        Console.WriteLine("Enter the license number:");
                    License=System.Console.ReadLine();
                        Random r = new random();
                       r.Next(0,1200);

                        break;
                    case 'c':
                        break;
                    case 'd':
                        break;
                    case 'e':
                        Console.WriteLine("bye");
                        break;
                    default:
                        break;
                }
            } while (ch != 'e');
           

        }
    }
}
