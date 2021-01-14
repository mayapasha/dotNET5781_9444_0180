
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DalApi;
using DO;

namespace DL
{
    sealed class DLObject : IDL
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion
        #region user
        public void Add_User(DO.User user)
        {
            if (DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName) != -1 && DataSource.ListUsers[DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName)].Is_Active == true)// if the user that we want to add exist and active throw exeption
            {

                throw new Add_Existing_Item_Exception("the user " + user.UserName + " that you wand to add is already exist");
            }
            else
            {
                DataSource.ListUsers.Add(user.Clone());
            }
        }
        public void Delete_User(DO.User user)
        {
            if (DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName) != -1 && DataSource.ListUsers[DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName)].Is_Active == true)// if the user that we want to add exist and active throw exeption
            {
                DataSource.ListUsers[DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName)].Is_Active = false;
            }
            else
            {
                throw new Item_not_found_Exception("the user " + user.UserName + " not exist");
            }
        }
        public IEnumerable<DO.User> Get_All_Users()
        {
            return from User in DataSource.ListUsers
                   select User.Clone();
        }
        public DO.User Get_User(string Username)
        {
            if (DataSource.ListUsers.FindIndex(u => u.UserName == Username) != -1 && DataSource.ListUsers[DataSource.ListUsers.FindIndex(u => u.UserName == Username)].Is_Active == true)
            {
                DO.User user = DataSource.ListUsers.Find(u => u.UserName == Username);
                return user.Clone();
            }
            else
            {
                throw new Item_not_found_Exception("the user " + Username + " not exist");
            }
        }
        #endregion
        #region bus
        public void Add_Bus(Bus bus)
        {
            if (DataSource.ListBuses.FindIndex(b => b.LicenseNum == bus.LicenseNum) != -1 && DataSource.ListBuses[DataSource.ListBuses.FindIndex(b => b.LicenseNum == bus.LicenseNum)].Is_Active == true)
            {
                throw new Add_Existing_Item_Exception("the bus " + bus.LicenseNum + " is alrady exist");
            }
            else
            {
                DataSource.ListBuses.Add(bus.Clone());
            }
        }
        public void Update_Bus(Bus bus)
        {
            DO.Bus other = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum);
            if (other != null)
            {
                DataSource.ListBuses.Remove(other);//????
                DataSource.ListBuses.Add(other.Clone());//?????
            }
            else
            {
                throw new Item_not_found_Exception("the bus " + bus.LicenseNum + " was not found");
            }
        }
        public Bus Get_Bus(int license)
        {
            if (DataSource.ListBuses.FindIndex(b => b.LicenseNum == license) != -1 && DataSource.ListBuses[DataSource.ListBuses.FindIndex(b => b.LicenseNum == license)].Is_Active == true)
            {
                DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == license);
                return bus.Clone();
            }
            else
            {
                throw new Item_not_found_Exception("the bus " + license + " not exist");
            }
        }
        public IEnumerable<Bus> Get_All_Buses()
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
        }
        public void Delete_Bus(Bus bus)
        {
            if (DataSource.ListBuses.FindIndex(b => b.LicenseNum == bus.LicenseNum) != -1 && DataSource.ListBuses[DataSource.ListBuses.FindIndex(b => b.LicenseNum == bus.LicenseNum)].Is_Active == true)
            {
                DataSource.ListBuses[DataSource.ListBuses.FindIndex(b => b.LicenseNum == bus.LicenseNum)].Is_Active = false;
            }
            else
            {
                throw new Item_not_found_Exception("the bus " + bus.LicenseNum + " not exist");
            }
        }
        #endregion
        #region line
        public void Update_Line(Line line)
        {
            DO.Line other = DataSource.ListLines.Find(l => l.Id == line.Id);
            if (other != null && other.Is_Active == true)
            {
                DataSource.ListLines.Remove(other);//????
                DataSource.ListLines.Add(other.Clone());//?????
            }
            else
            {
                throw new Item_not_found_Exception("the line " + line.Id + " was not found");
            }
        }
        public Line Get_Line(int id)
        {
            if (DataSource.ListLines.FindIndex(l => l.Id == id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == id)].Is_Active == true)
            {
                DO.Line line = DataSource.ListLines.Find(l => l.Id == id);
                return line.Clone();
            }
            else
            {
                throw new Item_not_found_Exception("the line " + id + " not exist");
            }
        }
        public IEnumerable<Line> Get_All_Lines()
        {
           
             return from line in DataSource.ListLines
                     select line.Clone();
        }
        public void Delete_Line(Line line)
        {
            if (DataSource.ListLines.FindIndex(l => l.Id == line.Id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id)].Is_Active == true)
            {
                DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id)].Is_Active = false;
            }
            else
            {
                throw new Item_not_found_Exception("the line " + line.Id + " not exist");
            }
        }
        public void Add_Line(Line line)
        {
            if (DataSource.ListLines.FindIndex(l => l.Id == line.Id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id)].Is_Active == true)
            {
                throw new Add_Existing_Item_Exception("the line " + line.Id + " is alrady exist");
            }
            else
            {
                line.Is_Active = true;
                DataSource.ListLines.Add(line.Clone());
            }
        }
        #endregion
        #region station
        public Station Get_Station(int code)
        {
            if (DataSource.ListStations.FindIndex(s => s.Code == code) != -1 && DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == code)].Is_Active == true)
            {
                DO.Station station = DataSource.ListStations.Find(s => s.Code == code);
                return station.Clone();
            }
            else
            {
                throw new Item_not_found_Exception("the station " + code + " not exist");
            }
        }
        public void Update_Station(Station station)
        {
            DO.Station other = DataSource.ListStations.Find(s => s.Code == station.Code);
            if (other != null && other.Is_Active == true)
            {
                DataSource.ListStations.Remove(other);//????
                DataSource.ListStations.Add(other.Clone());//?????
            }
            else
            {
                throw new Item_not_found_Exception("the station " + station.Code + " was not found");
            }
        }
        public IEnumerable<Station> Get_All_Stations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();
        }
        public void Delete_Station(Station station)
        {
            if (DataSource.ListStations.FindIndex(s => s.Code == station.Code) != -1 && DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == station.Code)].Is_Active == true)
            {
                DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == station.Code)].Is_Active = false;
            }
            else
            {
                throw new Item_not_found_Exception("the station " + station.Code + " not exist");
            }
        }
        public void Add_Station(Station station)
        {
            if (DataSource.ListStations.FindIndex(s => s.Code == station.Code) != -1 && DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == station.Code)].Is_Active == true)
            {
                throw new Add_Existing_Item_Exception("the station " + station.Code + " is alrady exist");
            }
            else
            {
                station.Is_Active = true;
                DataSource.ListStations.Add(station.Clone());
            }
        }
        #endregion
        #region linestation
        public void Update_LineStation(LineStation lineStation)
        {
            DO.LineStation other = DataSource.ListLineStations.Find(l => l.LineStationIndex == lineStation.LineStationIndex);
            if (other != null && other.Is_Active == true)
            {
                DataSource.ListLineStations.Remove(other);//????
                DataSource.ListLineStations.Add(other.Clone());//?????
            }
            else
            {
                throw new Item_not_found_Exception("the line station " + lineStation.LineStationIndex + " was not found");
            }
        }
        public LineStation Get_LineStation(int linestationIndex)
        {
            if (DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == linestationIndex) != -1 && DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == linestationIndex)].Is_Active == true)
            {
                DO.LineStation linestation = DataSource.ListLineStations.Find(l => l.LineStationIndex == linestationIndex);
                return linestation.Clone();
            }
            else
            {
                throw new Item_not_found_Exception("the line station " + linestationIndex + " not exist");
            }
        }
        public IEnumerable<LineStation> Get_All_LineStations()
        {
            return from linestation in DataSource.ListLineStations
                   select linestation.Clone();
        }
        public void Delete_LineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == lineStation.LineStationIndex) != -1 && DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == lineStation.LineStationIndex)].Is_Active == true)
            {
                DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == lineStation.LineStationIndex)].Is_Active = false;
            }
            else
            {
                throw new Item_not_found_Exception("the line station " + lineStation.LineStationIndex + " not exist");
            }
        }
        public void Add_LineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == lineStation.LineStationIndex) != -1 && DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.LineStationIndex == lineStation.LineStationIndex)].Is_Active == true)
            {
                throw new Add_Existing_Item_Exception("the line station " + lineStation.LineStationIndex + " is alrady exist");
            }
            else
            {
                var doLineStation = lineStation.Clone();
                doLineStation.LineStationIndex = ++RunNumbers.Run_Number_Line_Station;
                doLineStation.Is_Active = true;
                DataSource.ListLineStations.Add(doLineStation);
            }
        }


        #endregion
        #region AdjacentStations
        public DO.AdjacentStations GetAdjacentStations(int x, int y)
        {
            for (int i = 0; i < DataSource.ListAdjacentStations.Count; i++)
            {
                if (DataSource.ListAdjacentStations[i].Station1 == x && DataSource.ListAdjacentStations[i].Station2 == y && DataSource.ListAdjacentStations[i].Is_Active==true)
                {
                    return DataSource.ListAdjacentStations[i];
                }
            }
            throw new Item_not_found_Exception("the Adjacent between the Stations " + x + ", " + y + "is not found");
        }

        public void DeleteAdjacentStations(DO.AdjacentStations x)
        {
            for (int i = 0; i < DataSource.ListAdjacentStations.Count; i++)
            {
                if (DataSource.ListAdjacentStations[i] == x && DataSource.ListAdjacentStations[i].Is_Active==true)
                {
                    DataSource.ListAdjacentStations[i].Is_Active = false;
                    return;
                }
            }
            throw new Item_not_found_Exception("the adjacent between the stations"+x.Station1+""+x.Station2+"can not be found so it can not be deleted");
        }

        public void AddAdjacentStation(AdjacentStations x)
        {
            for (int i = 0; i < DataSource.ListAdjacentStations.Count; i++)
            {
                if (DataSource.ListAdjacentStations[i] == x && DataSource.ListAdjacentStations[i].Is_Active == true)
                {
                    throw new Add_Existing_Item_Exception("the item that you want to add is already exist");
                }
            }
            x.Is_Active = true;
            DataSource.ListAdjacentStations.Add(x.Clone());
        }

        public void UpdateAdjecentStation(AdjacentStations x)
        {
            DO.AdjacentStations other = DataSource.ListAdjacentStations.Find(a => a.Station1 == x.Station1 && a.Station2 ==x.Station2);
            if (other != null && other.Is_Active == true)
            {
                DataSource.ListAdjacentStations.Remove(other);//????
                DataSource.ListAdjacentStations.Add(other.Clone());//?????
            }
            else
            {
                throw new Item_not_found_Exception("the item is not exist so it can not be updated");
            }
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   select adjacentStations.Clone();
        }
        #endregion
    }
}


