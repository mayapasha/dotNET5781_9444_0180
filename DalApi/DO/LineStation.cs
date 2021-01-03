using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class LineStation
    {
        public int LineId { get; set; }
        public int Station { get; set; }
        public int LineStationIndex { get { return LineStationIndex; } set { LineStationIndex = RunNumbers.Run_Number_Line_Station; RunNumbers.Run_Number_Line_Station++; } }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
        public bool Is_Active { get; set; }
    }
}
