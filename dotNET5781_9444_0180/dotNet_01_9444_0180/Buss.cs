using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_9444_0180
{
	class Buss
    {
		public DateTime DateOfTreatment = DateTime.Now;
		public DateTime startdate;
		public string LicenseNum;
		public int Mileage;// the main mileage
		public int FuelMileage;// counter how much mileage we pass from the last refeul(max 1200)
		public int ThisMileage;// counter from the last treatment(max 20000)

	
     
       public Buss()
		{ }
        public Buss(string license, DateTime date)
		{

			LicenseNum = license;
			startdate = date;
			Mileage = 0;
			FuelMileage = 0;
			ThisMileage = 0;
		}
		public bool Prepare()
		{
			DateTime today = DateTime.Now;
			if (ThisMileage < 20000 &&  today.AddYears(-1)<= DateOfTreatment )
				return true;
			return false;
		}
		bool fuel()// return true if the buss need fuel
		{
			if (FuelMileage < 1200)
				return false;
			else
				return true;
		}

		public bool ReadyForDrive(int val)
		{
			if (FuelMileage + val > 1200)//if the buss need fuel for the drive
				return false;
			else if (Prepare() == false)//if the buss not preper for drive
				return false;
			else
			{
				FuelMileage += val;
				ThisMileage += val;
				Mileage += val;
				return true;
			}
		}
		public void Refuel()//
        {
			FuelMileage = 0;
		}

		public void treatment()
        {
			ThisMileage = 0;
			DateOfTreatment = DateTime.Now;

		}

        public void Print()
        {
            Console.WriteLine("License:" + LicenseNum + " Mileage:" + ThisMileage);
        }
    }

	
}
