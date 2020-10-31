using System;

public class Class1
{
	public Class1()
	{
	}
	string licenseNum;
	string startDate;
	int mileage;
	int fuelMileage;
	int thisMileage;
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
