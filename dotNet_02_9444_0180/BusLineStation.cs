using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet_02_9444_0180
{
    class BusLineStation:BusStation
    {       
        public double Distance { get; set; }// 
        public TimeSpan Time { get; set; }
        
        public BusLineStation()// constractor
        {
            Time = new TimeSpan(0, 0, 0);
        }      
        public double distanceCalculation(BusStation b)// the func get bus station and calculate the distance between the last station and this station
        {
            double x = b.Latitude - this.Latitude;
            double y = b.Longitude - this.Longitude;
            x=Math.Pow(x, 2);
            y = Math.Pow(y, 2);
            return this.Distance = Math.Sqrt(x + y);
        }
    }
}
