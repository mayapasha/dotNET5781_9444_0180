using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet_02_9444_0180
{
    public class BusLineStation:BusStation
    {       
        public double Distance { get; set; } 
        public TimeSpan Time { get; set; }

        public BusLineStation()// constractor
        {
            Random randtime = new Random();
            Time = new TimeSpan(randtime.Next(0,1), randtime.Next(0, 59), randtime.Next(0, 59));
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
