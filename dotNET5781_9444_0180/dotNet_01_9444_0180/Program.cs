﻿using System;
using System.Collections.Generic;
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

             BussList b1;
            Buss* b;
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
                ch = (char)System.Console.Read();

                switch (ch)
                {
                    case 'a':
                        Console.WriteLine("Enter the license number:");
                        License = System.Console.ReadLine();
                        Console.WriteLine("Enter the starting date of the buss:");
                        startnum = System.Console.ReadLine();
                        b1.addNewBuss(License, startnum);
                        break;
                    case 'b':

                        Console.WriteLine("Enter the license number:");
                        License = System.Console.ReadLine();
                        r.Next(0, 1200);

                        break;
                    case 'c':
                        Console.WriteLine("Enter the license number:");
                        License = System.Console.ReadLine();
                       b= b1.find(License);
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