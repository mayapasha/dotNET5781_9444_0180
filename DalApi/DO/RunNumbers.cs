using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    static public class RunNumbers
    {
        public static int Run_Number_Line_Station { get; set; } 
    }

    static public class RnumNumbersInitialization
    {
        public static void Init()
        {
            DO.RunNumbers.Run_Number_Line_Station = 0;
        }
       
    }
}
