using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using BLAPI;
using System.Threading;
using BO;
namespace BL
{
    internal class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();
        //BO.AdjacentStations
        void IBL.AddStationToLine(BO.Station s, BO.Line l, BO.LineStation ls)
        {
            foreach (var item in l.List_Of_LineStation)
            {
                if (item.Station == s.Code)
                {
                    throw new Exceptions.Add_Existing_Item_Exception("the station already exist in this line");
                }
            }
            int ind = ls.LineStationIndex;
            ls.NextStation = s.Code;
            BO.LineStation lsnew = new LineStation();
            lsnew.LineId = l.Id;
            lsnew.Station = s.Code;
            lsnew.PrevStation = ls.Station;
            lsnew.LineStationIndex = ind + 1;
            int counter = 0;
            foreach (var item in l.List_Of_LineStation)
            {
                if (counter == ind + 1)
                {
                    lsnew.NextStation = item.Station;
                    item.LineStationIndex = ind + 2;
                    item.PrevStation = lsnew.Station;

                }
                else if (counter > ind + 2)
                {
                    item.LineStationIndex += 1;
                }
                counter++;
            }
            dl.Add_LineStation(LineStationBoDoAdapter(lsnew));
        }
        #region user
        /// <summary>
        /// the func transform user type do to user type bo
        /// </summary>
        /// <param name="userDO"></param>
        /// <returns> userBO </returns>
        BO.User UsertDoBoAdapter(DO.User userDO)
        {
            BO.User userBO = new BO.User();

            string name = userDO.UserName;
            try
            {
                userDO = dl.Get_User(name);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            userDO.CopyPropertiesTo(userBO);

            return userBO;
        }
        DO.User UsertBoDoAdapter(BO.User userBO)
        {
            DO.User userDO = new DO.User();

            string name = userBO.Name;
            //try
            //{
            //    userBO = dl.Get_User(name);
            //}
            //catch (BO.Item_not_found_Exception ex)
            //{
            //    throw new DO.Exceptions.Item_not_found_Exception(ex.Message);
            //}
            userBO.CopyPropertiesTo(userDO);

            return userDO;
        }
        public void Add_User(BO.User user)
        {
            try
            {
                DO.User userDO = UsertBoDoAdapter(user);
                dl.Add_User(userDO);
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }

        public void Delete_user(BO.User user)
        {
            try
            {
                DO.User userDO = UsertBoDoAdapter(user);
                dl.Delete_User(userDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {

                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }

        public IEnumerable<BO.User> Get_All_Users()
        {
            return from item in dl.Get_All_Users()
                   select UsertDoBoAdapter(item);
        }

        public BO.User Get_User(string username)
        {
            DO.User userDO;
            try
            {
                userDO = dl.Get_User(username);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            return UsertDoBoAdapter(userDO);
        }


        #endregion
        #region bus
        BO.Bus BusDoBoAdapter(DO.Bus BusDO)
        {
            BO.Bus BusBO = new BO.Bus();

            int license = BusDO.LicenseNum;
            try
            {
                BusDO = dl.Get_Bus(license);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            BusDO.CopyPropertiesTo(BusBO);

            return BusBO;
        }
        DO.Bus BusBoDoAdapter(BO.Bus BusBO)
        {
            DO.Bus BusDO = new DO.Bus();

            int license = BusBO.LicenseNum;

            BusBO.CopyPropertiesTo(BusDO);

            return BusDO;
        }
        public Bus Get_Bus(int license)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.Get_Bus(license);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            return BusDoBoAdapter(busDO);
        }

        public IEnumerable<Bus> Get_All_Buses()
        {
            return from item in dl.Get_All_Buses()
                   select BusDoBoAdapter(item);
        }

        public void Add_Bus(BO.Bus bus)
        {
            try
            {
                DO.Bus busDO = BusBoDoAdapter(bus);
                dl.Add_Bus(busDO);
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }

        public void Delete_Bus(BO.Bus bus)
        {
            try
            {
                DO.Bus busDO = BusBoDoAdapter(bus);
                dl.Delete_Bus(busDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {

                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }

        public void update_Bus(BO.Bus bus)
        {
            try
            {
                DO.Bus busDO = BusBoDoAdapter(bus);

                dl.Update_Bus(busDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {

                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }


        #endregion
        #region line
        BO.Line LineDoBoAdapter(DO.Line lineDO)
        {
            BO.Line lineBO = new BO.Line();

            int id = lineDO.Id;
            try
            {
                lineDO = dl.Get_Line(id);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            lineDO.CopyPropertiesTo(lineBO);

            return lineBO;
        }
        DO.Line LineBoDoAdapter(BO.Line lineBO)
        {
            DO.Line lineDO = new DO.Line();

            int id = lineBO.Id;

            lineBO.CopyPropertiesTo(lineDO);

            return lineDO;
        }
        public Line Get_Line(int id)
        {
            DO.Line lineDO;
            try
            {
                lineDO = dl.Get_Line(id);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            return LineDoBoAdapter(lineDO);
        }

        public IEnumerable<Line> Get_All_Lines()
        {
            return from item in dl.Get_All_Lines()
                   select LineDoBoAdapter(item);
        }

        public void Add_Line(BO.Line line)
        {
            try
            {
                DO.Line lineDO = LineBoDoAdapter(line);
                dl.Add_Line(lineDO);
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }

        public void Delete_Line(BO.Line line)
        {
            try
            {
                foreach (var item in line.List_Of_LineStation)
                {
                    DO.LineStation lineStationDO = LineStationBoDoAdapter(item);
                    //Delete_LineStation(lineStationDO);
                }
                DO.Line lineDO = LineBoDoAdapter(line);
                dl.Delete_Line(lineDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }

        public void update_Line(Line line)
        {
            try
            {
                DO.Line lineDO = LineBoDoAdapter(line);
                dl.Update_Line(lineDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }
        public IEnumerable<BO.Station> Line_Station_To_Station(IEnumerable<LineStation> lineStations, int id)
        {
            IEnumerable<DO.Station> s = dl.Get_All_Stations();
            IEnumerable<BO.Station> stationsBO = from item in s
                                                 select StationDoBoAdapter(item);

            IEnumerable<BO.Station> stations = from item in lineStations
                                               from item1 in stationsBO
                                               where item.Station == item1.Code
                                               select item1;
            return stations;
        }
        #endregion
        #region line station
        BO.LineStation LineStationDoBoAdapter(DO.LineStation lineStationDO)
        {
            BO.LineStation lineStationBO = new BO.LineStation();

            int lineStationIndex = lineStationDO.LineStationIndex;
            try
            {
                lineStationDO = dl.Get_LineStation(lineStationDO.LineStationIndex);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            lineStationDO.CopyPropertiesTo(lineStationBO);

            return lineStationBO;
        }
        DO.LineStation LineStationBoDoAdapter(BO.LineStation lineStationBO)
        {
            DO.LineStation lineStationDO = new DO.LineStation();

            int lineStationIndex = lineStationBO.LineStationIndex;

            lineStationBO.CopyPropertiesTo(lineStationDO);

            return lineStationDO;
        }
        public LineStation Get_LineStation(int lineStationIndex)
        {
            DO.LineStation lineStationDO;
            try
            {
                lineStationDO = dl.Get_LineStation(lineStationIndex);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            return LineStationDoBoAdapter(lineStationDO);
        }

        public IEnumerable<LineStation> Get_All_LineStations()
        {
            return from item in dl.Get_All_LineStations()
                   select LineStationDoBoAdapter(item);
        }

        public void Add_LineStaton(LineStation lineStation)
        {
            try
            {
                DO.LineStation lineStationDO = LineStationBoDoAdapter(lineStation);
                dl.Add_LineStation(lineStationDO);
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }

        public void Delete_LineStation(LineStation lineStation)
        {
            try
            {
                DO.LineStation lineStationDO = LineStationBoDoAdapter(lineStation);
                dl.Delete_LineStation(lineStationDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }

        public void update_LineStation(LineStation lineStation)
        {
            try
            {
                DO.LineStation lineStationDO = LineStationBoDoAdapter(lineStation);
                dl.Update_LineStation(lineStationDO);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }




        #endregion
        #region station
        BO.Station StationDoBoAdapter(DO.Station StationDO)
        {
            BO.Station StationBO = new BO.Station();

            int StationIndex = StationDO.Code;
            try
            {
                StationDO = dl.Get_Station(StationDO.Code);
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
            StationDO.CopyPropertiesTo(StationBO);

            return StationBO;
        }
        DO.Station StationBoDoAdapter(BO.Station StationBO)
        {
            DO.Station StationDO = new DO.Station();

            int StationIndex = StationBO.Code;

            StationBO.CopyPropertiesTo(StationDO);

            return StationDO;
        }
        IEnumerable<Station> Get_All_Stations()
        {
            return from item in dl.Get_All_Stations()
                   select StationDoBoAdapter(item);
        }

        IEnumerable<Station> IBL.Get_All_Stations()
        {
            throw new NotImplementedException();
        }

        #endregion
        #region AdjacentStations

        public AdjacentStations Get_AdjacentStation(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void AddAdjacentStation(AdjacentStations s)
        {
            throw new NotImplementedException();
        }

        public void DeleteAdjacentStation(AdjacentStations s)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdjacentStation(AdjacentStations s)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
