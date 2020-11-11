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
        public int BusStationKey { get; set; } // the station number       
        public double Latitude { get; set; }// רוחב
        public double Longitude { get; set; }// אורך
        public string StationAdress { get; set; }// the station adress
        public BusStation()// constractor
        {
            Latitude = r.NextDouble() * (2.3) + 31; // set randon number between 31-33.3         
            Longitude = r.NextDouble()*(1.2)+34;  //  set randon number between 34.3-35.5        
        }
        public override string ToString()// return string with the information
        {
            return "Bus Station Code:" + BusStationKey + " , " + Latitude + "°N " + Longitude + "°E";
        }
    }
}
