using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 

namespace dotNet5781_01_9444_0180
{
    
    class Program
    {
        static void Main(string[] args)
        {
            List<Buss> b;
            char ch;

            do
            {
                ch= (char)System.Console.Read();

                switch (ch)
                {
                    case 'a':
                        break;
                    case 'b':
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
