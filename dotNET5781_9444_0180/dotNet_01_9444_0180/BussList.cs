using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet_01_9444_0180
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
