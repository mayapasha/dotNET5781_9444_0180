using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace dotNet_01_9444_0180
{
	public class BussList
    {
		public List<Buss> Busses;
		public BussList()// constractor
		{
			Busses = new List<Buss>();
		}
		public void AddNewBuss(string license,  DateTime date)//The func adding a new buss into the list of busses
		{
			Buss b = new Buss(license, date);
			Busses.Add(b);
		}

        public Buss FindBuss(string license)// get string , search the matching license and return this bus 
        {
            for (int i = 0; i < Busses.Count; i++)
            {
                if (Busses[i].LicenseNum == license)
                {
                    return Busses[i];
                }
            }  
            return null;       
        }
        public void PrintAllBusses()// print all the busses that in the list
        {
            foreach (Buss item in Busses)
            {
				item.Print();
            }
        }

	}
	
}
