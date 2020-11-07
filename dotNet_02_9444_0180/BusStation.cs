using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_02_9444_0180
{
    class BusStation
    {
        public static Random r = new Random();
        public int BusStationKey { get; set; }        
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string StationAdress { get; set; }
        public BusStation()
        {
            Latitude = r.NextDouble() * (2.3) + 31;
            //Latitude += r.NextDouble();
            Longitude = r.NextDouble()*(1.2)+34;           
            //Longitude += r.NextDouble();
        }
        public override string ToString()
        {
            return "Bus Station Code:" + BusStationKey + " , " + Latitude + "°N " + Longitude + "°E";
        }
    }
}
