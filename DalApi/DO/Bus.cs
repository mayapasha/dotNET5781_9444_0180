using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FeulRemain { get; set; }
        public Enums.BusStatus Status { get; set; }
        public bool Is_Active { get; set; }
    }
}
