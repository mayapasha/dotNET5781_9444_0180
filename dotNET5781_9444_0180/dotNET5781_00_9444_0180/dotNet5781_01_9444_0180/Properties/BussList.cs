using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

public class BussList
{
	public List<Buss> Busses;
	public BussList()
	{
	}
	public void addNewBuss(string license, string startdate)
    {
		Buss b = new Buss(license, startdate);
		Busses.Add(b);		
    }
	public Buss* find(string license)
    {
		Busses.FindIndex.license == license;
		
	
	}
}
