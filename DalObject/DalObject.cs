using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using DalApi;
using DO;

namespace Dal
{
    sealed class DalObject : IDL
    {       
        #region user
        public void Add_User(DO.User user)
        {
            if (DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName) != -1 && DataSource.ListUsers[DataSource.ListUsers.FindIndex(u => u.UserName == user.UserName)].Is_Active==true)// if the user that we want to add exist and active throw exeption
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
            if(DataSource.ListUsers.FindIndex(u => u.UserName == Username) != -1 && DataSource.ListUsers[DataSource.ListUsers.FindIndex(u => u.UserName == Username)].Is_Active == true)
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
            if(DataSource.ListBuses.FindIndex(b=>b.LicenseNum==bus.LicenseNum)!=-1 && DataSource.ListBuses[DataSource.ListBuses.FindIndex(b => b.LicenseNum == bus.LicenseNum)].Is_Active==true)
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
            //DO.Bus other = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum);
            //if (other != null)
            //{
            //    DataSource.ListBuses.Remove(other);
            //    DataSource.ListBuses.Add(other.Clone());
            //}
            //else
            //{
            //    throw new Item_not_found_Exception("the bus " + bus.LicenseNum + " was not found");
            //}
            
        }
        public Bus Get_Bus(int license)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Bus> Get_All_Buses()
        {
            throw new NotImplementedException();
        }
        public void Delete_Bus(Bus bus)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region line
        public void Update_Line(Line line)
        {
            throw new NotImplementedException();
        }
        public Line Get_Line(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Line> Get_All_Lines()
        {
            throw new NotImplementedException();
        }
        public void Delete_Line(Line line)
        {
            throw new NotImplementedException();
        }
        public void Add_Line(Line line)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region station
        public Station Get_Station(int id)
        {
            throw new NotImplementedException();
        }
        public void Update_Station(Station station)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Station> Get_All_Stations()
        {
            throw new NotImplementedException();
        }
        public void Delete_Station(Station station)
        {
            throw new NotImplementedException();
        }
        public void Add_Station(Station station)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region linestation
public void Update_LineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }
        public LineStation Get_LineStation(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineStation> Get_All_LineStations()
        {
            throw new NotImplementedException();
        }
        public void Delete_LineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }
        public void Add_LineStation(LineStation lineStation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
