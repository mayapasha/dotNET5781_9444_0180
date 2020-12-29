using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BusOnTrip
    {
        public int Id { get; set; }
        public int LicenseNum { get; set; }
        public TimeSpan PlannedTakeOff { get; set; }
        public TimeSpan AtcualTakeOff { get; set; }
        public int PrevStation { get; set; }
        public TimeSpan PrevStationAt { get; set; }
        public TimeSpan NextStationAt { get; set; }
    }
}
