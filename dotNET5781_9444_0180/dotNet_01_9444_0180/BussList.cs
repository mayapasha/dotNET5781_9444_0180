using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace dotNet_01_9444_0180
{
	class BussList
    {
		public List<Buss> Busses;
		public BussList()
		{
			Busses = new List<Buss>();
		}
		public void AddNewBuss(string license,  DateTime date)//The func adding a new buss into the list of busses
		{
			Buss b = new Buss(license, date);
			Busses.Add(b);
		}

        public Buss FindBuss(string license)
        {
            for (int i = 0; i < Busses.Count; i++)
            {
                if (Busses[i].LicenseNum == license)
                {
                    return Busses[i];
                }
            }
            //foreach (Buss item in Busses)
            //{
            //	if (item.LicenseNum == license)
            //		return item;

            //}
            return null;
            ////return Busses.Find((Buss) => Buss.LicenseNum == license);
        }
        public void PrintAllBusses()
        {
            foreach (Buss item in Busses)
            {
				item.Print();
            }
        }

	}
	
}
