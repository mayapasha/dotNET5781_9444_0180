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
        public string Find_LineStation_Name(int SCode)
        {
            BO.Station s = new Station();
               s= Get_All_Stations().ToList().Find(item => item.Code == SCode);
            return s.Name;
            
        }
        public IEnumerable<BO.LineTrip> Find_All_LineTrip_Of_Line(int LId)
        {
            IEnumerable<BO.LineTrip> ltBO = Get_All_LineTrips().ToList().Where(item => item.LineId == LId).Select(item => item).ToList();
            return ltBO;
        }
        
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
            dl.Update_LineStation(new DO.LineStation { Is_Active = true, LineId = ls.LineId, LineStationIndex = ls.LineStationIndex, NextStation = ls.NextStation, PrevStation = ls.PrevStation, Station = ls.Station });
            int counter = 0;
            foreach (var item in l.List_Of_LineStation)
            {
                if (counter == ind + 1)
                {
                    lsnew.NextStation = item.Station;
                    item.LineStationIndex = ind + 2;
                    item.PrevStation = lsnew.Station;
                    dl.Update_LineStation(new DO.LineStation { Is_Active = true, LineId = item.LineId, LineStationIndex = item.LineStationIndex, NextStation = item.NextStation, PrevStation = item.PrevStation, Station = item.Station });
                }
                else if (counter >= ind + 2)
                {
                    item.LineStationIndex += 1;
                    dl.Update_LineStation(new DO.LineStation { Is_Active = true, LineId = item.LineId, LineStationIndex = item.LineStationIndex, NextStation = item.NextStation, PrevStation = item.PrevStation, Station = item.Station });
                }
                counter++;
               
            }

            dl.Add_LineStation(LineStationBoDoAdapter(lsnew));

        }
        public IEnumerable<BO.Line> FindAllLinesOfThisStation(BO.Station stationBO)
        {
            return from item in Get_All_Lines()
                   from item1 in item.List_Of_LineStation
                   where item1.Station == stationBO.Code
                   select new BO.Line { Area = item.Area, Code = item.Code, LastStation = item.LastStation, FirstStation = item.FirstStation, Id = item.Id, List_Of_LineStation = item.List_Of_LineStation };
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

        public IEnumerable<BO.Line> Get_All_Lines()
        {

            try
            {
                IEnumerable<DO.Line> l = dl.Get_All_Lines().ToList();
                IEnumerable<BO.Line> lb = l.Select(item => new BO.Line { Area = (BO.Enums.Areas)item.Area, Code = item.Code, Id = item.Id, FirstStation = item.LastStation, LastStation = item.LastStation }).ToList();
                foreach (var item1 in lb)
                {

                    IEnumerable<BO.LineStation> LBitem = Get_All_LineStations().ToList().Where(item => item.LineId == item1.Id).Select(item => new BO.LineStation { LineId = item.LineId, Station = item.Station, LineStationIndex = item.LineStationIndex, PrevStation = item.PrevStation, NextStation = item.NextStation }).ToList();
                    item1.List_Of_LineStation = LBitem;
                    /*from item1 in Get_All_LineStations();

                                           where item1.LineId == item.Id
                                           select new BO.LineStation { Distance = item1.Distance, LineId = item1.LineId, LineStationIndex = item1.LineStationIndex, NextStation = item1.NextStation, PrevStation = item1.PrevStation, Station = item1.Station, Time = item1.Time };*/
                }
                return lb;
            }
            catch (BO.Exceptions.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }

        }

        public void Add_Line(BO.Line line)
        {
            try
            {
                BO.LineStation lSFirst = new BO.LineStation { LineStationIndex = 0, Distance = 0, LineId = line.Code, PrevStation = -1, Station = line.FirstStation, NextStation = line.LastStation, Time = new TimeSpan(0, 0, 0) };
                lSFirst.Name = Find_LineStation_Name(lSFirst.Station);
                Add_LineStaton(lSFirst);
                BO.LineStation lSLast = new BO.LineStation { LineStationIndex = 1, Distance = 0, LineId = line.Code, PrevStation = line.FirstStation, Station = line.LastStation, NextStation = -1, Time = new TimeSpan(0, 0, 0) };
                lSLast.Name = Find_LineStation_Name(lSLast.Station);
                Add_LineStaton(lSLast);
                DO.Line lineDO = new DO.Line { Area= (DO.Enums.Areas)line.Area, LastStation=line.LastStation, Code=line.Code, FirstStation=line.FirstStation, Id=line.Id, Is_Active=true };
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
            try
            {
                BO.LineStation b = LineStationDoBoAdapter(dl.Get_LineStation(lineStationIndex));
                BO.AdjacentStations a = Get_AdjacentStation(b.Station, b.NextStation);
                b.Distance = a.Distance;
                b.Time = a.Time;
                b.Name = Find_LineStation_Name(b.Station);
                return b;
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }

        public IEnumerable<LineStation> Get_All_LineStations()
        {
            IEnumerable<DO.LineStation> l = dl.Get_All_LineStations().ToList();
            IEnumerable<BO.LineStation> ls = l.Select(item => new BO.LineStation { LineId = item.LineId, Station = item.Station, LineStationIndex = item.LineStationIndex, PrevStation = item.PrevStation, NextStation = item.NextStation }).ToList();
            // IEnumerable<BO.LineStation> ls = from item in dl.Get_All_LineStations()
            // select new BO.LineStation { LineId=item.LineId, Station=item.Station, LineStationIndex=item.LineStationIndex, PrevStation=item.PrevStation, NextStation=item.NextStation };
            foreach (var item in ls)
            {
                try
                {
                    if (item.NextStation != -1)
                    {
                        BO.AdjacentStations a = Get_AdjacentStation(item.Station, item.NextStation);
                        item.Distance = a.Distance;
                        item.Time = a.Time;
                        item.Name = Find_LineStation_Name(item.Station);
                    }
                    else
                    {
                        item.Distance = 0;
                        item.Time = new TimeSpan(0, 0, 0);
                        item.Name = Find_LineStation_Name(item.Station);
                    }
                }
                catch (DO.Item_not_found_Exception ex)
                {
                    throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
                }
                catch (BO.Exceptions.Item_not_found_Exception ex)
                {
                    throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
                }
            }
            return ls;
        }

        public void Add_LineStaton(LineStation lineStation)
        {
            try
            {
                DO.LineStation lineStationDO = new DO.LineStation { Is_Active = true, LineId=lineStation.LineId, LineStationIndex=lineStation.LineStationIndex, NextStation=lineStation.NextStation, PrevStation=lineStation.PrevStation, Station=lineStation.Station };
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
                BO.AdjacentStations adjacentBO = Get_AdjacentStation(lineStation.Station, lineStation.NextStation);
                DO.AdjacentStations adjacentDO = new DO.AdjacentStations { Time = adjacentBO.Time, Distance = adjacentBO.Distance, Is_Active = true, Station1 = adjacentBO.Station1, Station2 = adjacentBO.Station2 };
                dl.UpdateAdjecentStation(adjacentDO);
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
            StationBO.Name = StationDO.Name;
            StationBO.Longitude = StationDO.Longitude;
            StationBO.Lattitude = StationDO.Lattitude;
            StationBO.Code = StationDO.Code;
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

        IEnumerable<BO.Station> IBL.Get_All_Stations()
        {
            var a = dl.Get_All_Stations();
            return from item in a
                   select new BO.Station { Code = item.Code, Name = item.Name, Lattitude = item.Lattitude, Longitude = item.Longitude };

            //return stationsBO;

        }
        public Station GetStation(int n)
        {
            try
            {
                return StationDoBoAdapter(dl.Get_Station(n));
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }

        }
        public void AddStation(Station s)
        {
            try
            {
                dl.Add_Station(StationBoDoAdapter(s));
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }

        public void DeleteStation(Station s)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(Station s)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region AdjacentStations
        public BO.AdjacentStations AdjacentStationDoToBo(DO.AdjacentStations a)
        {
            BO.AdjacentStations b = new AdjacentStations();
            b.Distance = a.Distance;
            b.Station1 = a.Station1;
            b.Station2 = a.Station2;
            b.Time = a.Time;
            return b;
        }
        public DO.AdjacentStations AdjacentStationBoToDo(BO.AdjacentStations a)
        {
            DO.AdjacentStations b = new DO.AdjacentStations();
            b.Distance = a.Distance;
            b.Station1 = a.Station1;
            b.Station2 = a.Station2;
            b.Time = a.Time;
            return b;
        }

        public AdjacentStations Get_AdjacentStation(int x, int y)
        {
            try
            {
                DO.AdjacentStations adjacentDP = dl.GetAdjacentStations(x, y);
                return new BO.AdjacentStations { Distance = adjacentDP.Distance, Station1 = adjacentDP.Station1, Station2 = adjacentDP.Station2, Time = adjacentDP.Time };
            }
            catch (DO.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }
        public void AddAdjacentStation(BO.AdjacentStations s)
        {
            try
            {
                dl.AddAdjacentStation(AdjacentStationBoToDo(s));
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }
        public void DeleteAdjacentStation(BO.AdjacentStations s)
        {
            try
            {
                dl.DeleteAdjacentStations(AdjacentStationBoToDo(s));
            }
            catch (Exceptions.Item_not_found_Exception ex)
            {

                throw new Exceptions.Item_not_found_Exception(ex.Message);
            }
        }

        public void UpdateAdjacentStation(BO.AdjacentStations s)
        {
            try
            {
                dl.UpdateAdjecentStation(AdjacentStationBoToDo(s));
            }
            catch (Exceptions.Item_not_found_Exception ex)
            {
                throw new Exceptions.Item_not_found_Exception(ex.Message);
            }
        }





        #endregion
        #region line trip 
        public IEnumerable<LineTrip> Get_All_LineTrips()
        {

            try
            {
                IEnumerable<BO.LineTrip> linetripBO = dl.Get_All_LineTrip().ToList().Select(item => new BO.LineTrip { Id = item.Id, LineId = item.LineId, StartAt = item.StartAt }).ToList();
                return linetripBO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

       
        #endregion
    }

};
