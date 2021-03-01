
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

        #region line
        /// <summary>
        /// the func get a DO line and update this line in the data source if it exist
        /// </summary>
        /// <param name="line"></param>
        public void Update_Line(Line line)
        {
            DO.Line other = DataSource.ListLines.Find(l => l.Id == line.Id);//find the line in data source
            if (other != null && other.Is_Active == true)//checks if the line is exist
            {
                DataSource.ListLines.Remove(other);// remove the nun updated line from the list
                line.Is_Active = true;
                DataSource.ListLines.Add(other.Clone());// adding the updated line to list
            }
            else// the line is not exist in the list
            {
                throw new Item_not_found_Exception("the line " + line.Id + " was not found");
            }
        }
        /// <summary>
        /// the func get an id of a line and return the line itself, else it will throw an exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Line Get_Line(int id)
        {
            if (DataSource.ListLines.FindIndex(l => l.Id == id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == id)].Is_Active == true)// checks if there is a line with this id 
            {
                DO.Line line = DataSource.ListLines.Find(l => l.Id == id && l.Is_Active == true);//find the currect line from the id
                return line.Clone();//return a clone of the line
            }
            else// there is no line in the data source with this id
            {
                throw new Item_not_found_Exception("the line " + id + " not exist");
            }
        }
        /// <summary>
        /// the func return an IEnumerable with all the lines in data souce
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Line> Get_All_Lines()
        {

            return from line in DataSource.ListLines// link that clone all the lines in data source and return an IEnumerable of them
                   where line.Is_Active == true
                   select line.Clone();
        }
        /// <summary>
        /// the func get a line and deletE it from the list in data source(change the is active to false), or it will throw an Exception
        /// </summary>
        /// <param name="line"></param>
        public void Delete_Line(Line line)
        {
            if (DataSource.ListLines.FindIndex(l => l.Id == line.Id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id)].Is_Active == true)// checks if the line is exist in the list
            {
                DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id && l.Is_Active == true)].Is_Active = false;// delete the line-> is_active=false
            }
            else//htere is no line like it in the list in data source
            {
                throw new Item_not_found_Exception("the line " + line.Id + " not exist");
            }
        }
        /// <summary>
        /// the  func get a line and add it to the list if it not exist in the list in data source, or it thoew an Exception
        /// </summary>
        /// <param name="line"></param>
        public void Add_Line(Line line)
        {
            // while (DataSource.ListLines.FindIndex(l => l.Id == line.Id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id)].Is_Active == true)
            //{
            //    line.Id = RunNumbers.Run_Number_Line_Id;
            //    RunNumbers.Run_Number_Line_Id++;
            //}
            int max = 0;
            for (int i = 0; i < DataSource.ListLines.Count(); i++) { if (DataSource.ListLines[i].Id > max) max = DataSource.ListLines[i].Id; }
            RunNumbers.Run_Number_Line_Id = max + 1;
            line.Id = RunNumbers.Run_Number_Line_Id;//make an id to the new line
                                                    //            RunNumbers.Run_Number_Line_Id++;
            if (DataSource.ListLines.FindIndex(l => l.Id == line.Id) != -1 && DataSource.ListLines[DataSource.ListLines.FindIndex(l => l.Id == line.Id)].Is_Active == true)// check if the line is already exist in the data source
            {
                throw new Add_Existing_Item_Exception("the line " + line.Code + " is alrady exist" + line.Id);
            }
            line.Is_Active = true;// the line is exist 
            DataSource.ListLines.Add(line.Clone());// adding a clone to the list in data source

        }
        #endregion
        #region station
        /// <summary>
        /// get code of station and return this station, throw exception if the station is not exist
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Station Get_Station(int code)
        {
            if (DataSource.ListStations.FindIndex(s => s.Code == code) != -1 && DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == code)].Is_Active == true)
            {// check if the station is exist
                DO.Station station = DataSource.ListStations.Find(s => s.Code == code && s.Is_Active == true);
                return station.Clone();// return clone of this station
            }
            else// if the station not exist
            {
                throw new Item_not_found_Exception("the station " + code + " not exist");
            }
        }
        /// <summary>
        /// get updated station and replace it with its old station, if the station wasnt exist before- throw exception
        /// </summary>
        /// <param name="station"></param>
        public void Update_Station(Station station)
        {
            DO.Station other = DataSource.ListStations.Find(s => s.Code == station.Code);// get the old station
            if (other != null && other.Is_Active == true)//if the station is exist and active
            {
                DataSource.ListStations.Remove(other);// remove the old station
                station.Is_Active = true;
                DataSource.ListStations.Add(other.Clone());// add the new station
            }
            else// if the station wasnt exist
            {
                throw new Item_not_found_Exception("the station " + station.Code + " was not found");
            }
        }
        /// <summary>
        /// return all the lines in the data source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> Get_All_Stations()
        {
            return from station in DataSource.ListStations
                   where station.Is_Active == true
                   select station.Clone();
        }
        /// <summary>
        /// get station and delete it if its exist, if its not throw exception
        /// </summary>
        /// <param name="station"></param>
        public void Delete_Station(Station station)
        {
            if (DataSource.ListStations.FindIndex(s => s.Code == station.Code) != -1 && DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == station.Code)].Is_Active == true)
            {// if the station exist, change the flag to false
                DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == station.Code)].Is_Active = false;
            }
            else// if the station wasnt exist
            {
                throw new Item_not_found_Exception("the station " + station.Code + " not exist");
            }
        }
        /// <summary>
        /// get station and if the station not exist add it, if its exist throw exception
        /// </summary>
        /// <param name="station"></param>
        public void Add_Station(Station station)
        {
            if (DataSource.ListStations.FindIndex(s => s.Code == station.Code) != -1 && DataSource.ListStations[DataSource.ListStations.FindIndex(s => s.Code == station.Code)].Is_Active == true)
            {// if the station exist
                throw new Add_Existing_Item_Exception("the station " + station.Code + " is alrady exist");
            }
            else//add the station
            {
                station.Is_Active = true;
                DataSource.ListStations.Add(station.Clone());
            }
        }
        #endregion
        #region linestation
        /// <summary>
        /// get updated line station, remove the old one and add the new one, if the line station wasnt exist before throw exception 
        /// </summary>
        /// <param name="lineStation"></param>
        public void Update_LineStation(LineStation lineStation)
        {
            DO.LineStation other = DataSource.ListLineStations.Find(l => l.Station == lineStation.Station && l.LineId == lineStation.LineId && l.Is_Active == true);
            if (other != null && other.Is_Active == true)// check if its exist
            {
                DataSource.ListLineStations.Remove(other);// remove the old one
                lineStation.Is_Active = true;
                DataSource.ListLineStations.Insert(other.LineStationIndex, other.Clone());// add the new one

            }
            else // if its not exist
            {
                throw new Item_not_found_Exception("the line station " + lineStation.LineStationIndex + " was not found");
            }
        }
        /*public LineStation Get_LineStation(int linestationIndex)
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
        }*/
        /// <summary>
        /// return all the line stations in the data source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineStation> Get_All_LineStations()
        {
            return from linestation in DataSource.ListLineStations
                   where linestation.Is_Active == true
                   select linestation.Clone();
        }

        /// <summary>
        /// get line station and add it, if it is exist throw exception
        /// </summary>
        /// <param name="lineStation"></param>
        public void Add_LineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FindIndex(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station) != -1 && DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station)].Is_Active == true)
            {// if it is exist
                throw new Add_Existing_Item_Exception("the line station " + lineStation.LineStationIndex + " is alrady exist");
            }
            else// add the new line station
            {
                var doLineStation = lineStation.Clone();
                doLineStation.Is_Active = true;
                DataSource.ListLineStations.Insert(doLineStation.LineStationIndex, doLineStation);
            }
        }


        #endregion
        #region AdjacentStations
        /// <summary>
        /// the func get to codes of staions and find the adj station of them, or thorw an exceptin
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public DO.AdjacentStations GetAdjacentStations(int x, int y)
        {
            foreach (var item in DataSource.ListAdjacentStations)// go over all the adj station in data source
            {
                if (item.Station1 == x && item.Station2 == y && item.Is_Active == true)// if it exists return it
                {
                    return item.Clone();
                }
            }
            //if there is no adj station of this codes
            throw new Item_not_found_Exception("the Adjacent between the Stations " + x + ", " + y + "is not found");
        }
        /// <summary>
        /// get an adj station and delete it or throw an exception if it not exist
        /// </summary>
        /// <param name="x"></param>
        public void DeleteAdjacentStations(DO.AdjacentStations x)
        {
            for (int i = 0; i < DataSource.ListAdjacentStations.Count; i++)// go over all the adj stations
            {
                if (DataSource.ListAdjacentStations[i] == x && DataSource.ListAdjacentStations[i].Is_Active == true)// the adj station is exist
                {
                    DataSource.ListAdjacentStations[i].Is_Active = false;// delete it=> is _active=false
                    return;
                }
            }
            //the adj station is not exist
            throw new Item_not_found_Exception("the adjacent between the stations" + x.Station1 + "" + x.Station2 + "can not be found so it can not be deleted");
        }
        /// <summary>
        /// get an adj station the add it to the list , if it exist throw an exception
        /// </summary>
        /// <param name="x"></param>
        public void AddAdjacentStation(AdjacentStations x)
        {
            for (int i = 0; i < DataSource.ListAdjacentStations.Count; i++)
            {
                if (DataSource.ListAdjacentStations[i].Station1 == x.Station1 && DataSource.ListAdjacentStations[i].Station2 == x.Station2 && DataSource.ListAdjacentStations[i].Is_Active == true)// the adj station is already exist
                {
                    throw new Add_Existing_Item_Exception("the item that you want to add is already exist");
                }
            }//add the new adj station
            x.Is_Active = true;
            DataSource.ListAdjacentStations.Add(x.Clone());
        }
        /// <summary>
        /// get an updated adj station and add it and remove the old one, if it not already exist thorw an excpation
        /// </summary>
        /// <param name="x"></param>
        public void UpdateAdjecentStation(AdjacentStations x)
        {
            DO.AdjacentStations other = DataSource.ListAdjacentStations.Find(a => a.Station1 == x.Station1 && a.Station2 == x.Station2 && a.Is_Active == true);
            if (other != null && other.Is_Active == true)//the old adj staiton is exist
            {
                DataSource.ListAdjacentStations.Remove(other);//remove the old one
                x.Is_Active = true;
                DataSource.ListAdjacentStations.Add(other.Clone());//adding the new adj station
            }
            else// the old adj station is not exist in list
            {
                throw new Item_not_found_Exception("the item is not exist so it can not be updated");
            }
        }
        /// <summary>
        /// the func return all the adj station in data source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   where adjacentStations.Is_Active = true
                   select adjacentStations.Clone();
        }


        #endregion

        #region line trip
        /// <summary>
        /// get line id and int i which is the id of the line trip, and return this line trip, if its not exist throw exception
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public LineTrip GetLineTrip(int lineId, int i)
        {
            foreach (var item in DataSource.listLineTrip)
            {
                if (item.LineId == lineId && item.Id == i && item.Is_Active == true)// if its exist and active
                {
                    return item.Clone();// return clone of the line trip
                }
            }
            throw new DO.Item_not_found_Exception("the line trip that you want to find is not exist");
        }
        /// <summary>
        /// get line trip and delete it if its exist, if its not throw exception
        /// </summary>
        /// <param name="lineTrip"></param>
        public void DeleteLineTrip(LineTrip lineTrip)
        {
            foreach (var item in DataSource.listLineTrip)
            {
                if (item.LineId == lineTrip.LineId && item.Id == lineTrip.Id && item.Is_Active == true)
                {// if its not exist
                 //  item.Is_Active = false;
                    DataSource.listLineTrip.Find(lt => item.LineId == lineTrip.LineId && lt.Id == lineTrip.Id && lt.Is_Active == true).Is_Active = false;
                    return;
                }
            }
            throw new DO.Item_not_found_Exception("this line trip can not be deleted because it is not exists");
        }
        /// <summary>
        /// get updated line trip, if its exist remove the old one and add the new one, if its not throw exception
        /// </summary>
        /// <param name="lineTrip"></param>
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            DO.LineTrip other = DataSource.listLineTrip.Find(l => l.Id == lineTrip.Id && l.LineId == lineTrip.LineId && l.Is_Active == true);
            if (other != null && other.Is_Active == true)// if its exist
            {
                DataSource.listLineTrip.Remove(other);//remove the old one
                lineTrip.Is_Active = true;
                DataSource.listLineTrip.Add(other.Clone());//add the new one
            }
            else// if its not exist
            {
                throw new Item_not_found_Exception("the line trip of " + lineTrip.LineId + " has not found");
            }
        }
        /// <summary>
        /// get line trip, and if it wasnt exist before add it, if it was throw exception
        /// </summary>
        /// <param name="lineTrip"></param>
        public void AddLineTrip(LineTrip lineTrip)
        {
            foreach (var item in DataSource.listLineTrip)
            {
                if (item.Id == lineTrip.Id && item.LineId == lineTrip.LineId)
                {
                    if (item.Is_Active == true)
                    {// if its exist and acive
                        throw new Add_Existing_Item_Exception("this line trip is allready exists");
                    }
                    if (item.Is_Active == false)// if it wasnt exist
                    {
                        item.Is_Active = true;
                        DataSource.listLineTrip.Add(item);
                        return;
                        // UpdateLineTrip(lineTrip);
                    }
                }
            }
            //throw new Add_Existing_Item_Exception("this line trip is allready exists");
        }
        /// <summary>
        /// return all the line trips that exist in data source
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineTrip> Get_All_LineTrip()
        {
            IEnumerable<DO.LineTrip> ltDO = DataSource.listLineTrip.ToList().Where(item => item.Is_Active == true).Select(item => item.Clone());
            return ltDO;
        }
        #endregion


        public void Delete_LineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FindIndex(l => l.Station == lineStation.Station && l.LineId == lineStation.LineId) != -1 && DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.Station == lineStation.Station && l.LineId == lineStation.LineId)].Is_Active == true)
            {
                DataSource.ListLineStations[DataSource.ListLineStations.FindIndex(l => l.Station == lineStation.Station && l.LineId == lineStation.LineId && l.Is_Active == true)].Is_Active = false;
            }
            else
            {
                throw new Item_not_found_Exception("the line station " + lineStation.LineStationIndex + " not exist");
            }
        }
    }
}


