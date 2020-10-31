using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_9444_0180
{
	public Buss(string license, string startdate)
	{
		licenseNum = license;
		startDate = startdate;
		mileage = 0;
		fuelMileage = 0;
		thisMileage = 0;
	}
	public string licenseNum;
	public string startDate;
	public int mileage;// the main mileage
	public int fuelMileage;// counter how much mileage we pass from the last refeul
	public int thisMileage;// counter from the last treatment

	bool proper()
	{
		if (thisMileage < 20000)
			return true;
		// לבדוק אם עברה שנה מאז הטיפול האחרון
	}
	bool fuel()// return true if the buss need fuel
	{
		if (fuelMileage < 1200)
			return false;
		else
			return true;
	}
}
