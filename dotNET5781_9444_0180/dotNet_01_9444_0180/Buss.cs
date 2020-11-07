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

	
     
       public Buss()// defalt constractor
		{ }
        public Buss(string license, DateTime date)// constractor that get arguments
		{

			LicenseNum = license;
			startdate = date;
			Mileage = 0;
			FuelMileage = 0;
			ThisMileage = 0;
		}
		public bool Prepare()// return true if the buss no dangerous for a drive
		{
			DateTime today = DateTime.Now;
			if (ThisMileage < 20000 &&  today.AddYears(-1)<= DateOfTreatment )
				return true;
			return false;
		}
		

		public bool ReadyForDrive(int val)// return true if the buss can go for a drive 
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
		public void Refuel()// refeul the buss
        {
			FuelMileage = 0;
		}

		public void treatment()// give the buss a treatment
        {
			ThisMileage = 0;
			DateOfTreatment = DateTime.Now;

		}

        public void Print()// print the data of the buss
        {
            Console.WriteLine("License:" + LicenseNum + " Mileage:" + ThisMileage);
        }
    }

	
}
