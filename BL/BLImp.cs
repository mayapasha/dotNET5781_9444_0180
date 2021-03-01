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

        static readonly BLImp instance = new BLImp();
        #region singelton
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp()
        {
        }
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion
        IDL dl = DLFactory.GetDL();

        /// <summary>
        /// get code of station and return the name of the station, if its not exist throw exception
        /// </summary>
        /// <param name="SCode"></param>
        /// <returns></returns>
        public string Find_LineStation_Name(int SCode)
        {
            BO.Station s = new Station();
            s = Get_All_Stations().ToList().Find(item => item.Code == SCode);
            if (s != null)
                return s.Name;
            else
                throw new BO.Exceptions.Item_not_found_Exception("the station not exist");

        }
        /// <summary>
        /// get line id and return all line trips of this line
        /// </summary>
        /// <param name="LId"></param>
        /// <returns></returns>
        public IEnumerable<BO.LineTrip> Find_All_LineTrip_Of_Line(int LId)
        {
            IEnumerable<BO.LineTrip> ltBO = Get_All_LineTrips().ToList().Where(item => item.LineId == LId).Select(item => item).ToList();
            return ltBO.OrderBy(x => x.Id);
        }
        /// <summary>
        /// get station, line, line station and add station to the line and throw exception if the station is already exist in the line
        /// </summary>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <param name="ls"></param>
        void IBL.AddStationToLine(BO.Station s, BO.Line l, BO.LineStation ls)
        {
            foreach (var item in l.List_Of_LineStation)
            {//check if the station is already exist in this line
                if (item.Station == s.Code)
                {
                    throw new Exceptions.Add_Existing_Item_Exception("the station already exist in this line");
                }
            }



            int ind = ls.LineStationIndex;
            ls.NextStation = s.Code;
            BO.LineStation lsnew = new LineStation();//cearte new line station
            lsnew.LineId = l.Id;
            lsnew.Station = s.Code;
            lsnew.PrevStation = ls.Station;
            lsnew.LineStationIndex = ind + 1;
            //update the prev line station
            dl.Update_LineStation(new DO.LineStation { Is_Active = true, LineId = ls.LineId, LineStationIndex = ls.LineStationIndex, NextStation = ls.NextStation, PrevStation = ls.PrevStation, Station = ls.Station });
            int counter = 0;
            foreach (var item in l.List_Of_LineStation)
            {//update all the next line stations
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

            dl.Add_LineStation(LineStationBoDoAdapter(lsnew));//add the new line station

        }
        /// <summary>
        /// get station and return all the lines that pass this station
        /// </summary>
        /// <param name="stationBO"></param>
        /// <returns></returns>
        public IEnumerable<BO.Line> FindAllLinesOfThisStation(BO.Station stationBO)
        {
            return from item in Get_All_Lines()
                   from item1 in item.List_Of_LineStation
                   where item1.Station == stationBO.Code
                   select new BO.Line { Area = item.Area, Code = item.Code, LastStation = item.LastStation, FirstStation = item.FirstStation, Id = item.Id, List_Of_LineStation = item.List_Of_LineStation };
        }

        #region line
        /// <summary>
        /// get DO line and return BO line, and catch exeption from do and throw new one
        /// </summary>
        /// <param name="lineDO"></param>
        /// <returns></returns>
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
        /// <summary>
        /// get bo line and return do line
        /// </summary>
        /// <param name="lineBO"></param>
        /// <returns></returns>
        DO.Line LineBoDoAdapter(BO.Line lineBO)
        {
            DO.Line lineDO = new DO.Line();

            int id = lineBO.Id;

            lineBO.CopyPropertiesTo(lineDO);

            return lineDO;
        }
        /// <summary>
        /// get id and return the line, and throw exception if its not exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// return all the lines from data source, and catch exception from other function and throw new one
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Line> Get_All_Lines()
        {

            try
            {
                IEnumerable<DO.Line> l = dl.Get_All_Lines().ToList();// get all line from the data source by DO line
                //change it to bo line
                IEnumerable<BO.Line> lb = l.Select(item => new BO.Line { Area = (BO.Enums.Areas)item.Area, Code = item.Code, Id = item.Id, FirstStation = item.LastStation, LastStation = item.LastStation }).ToList();
                foreach (var item1 in lb)
                {
                    //collect all the line stations in each line 
                    IEnumerable<BO.LineStation> LBitem = Get_All_LineStations().ToList().Where(item => item.LineId == item1.Id).Select(item => new BO.LineStation { LineId = item.LineId, Station = item.Station, LineStationIndex = item.LineStationIndex, PrevStation = item.PrevStation, NextStation = item.NextStation }).ToList();
                    item1.List_Of_LineStation = LBitem;

                }
                return lb;
            }
            catch (BO.Exceptions.Item_not_found_Exception ex)
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }

        }
        /// <summary>
        /// get line and add it if it wasnt exist before, else throw exception
        /// </summary>
        /// <param name="line"></param>
        public void Add_Line(BO.Line line)
        {
            try
            {
                //make a new line station for the first station in line
                BO.LineStation lSFirst = new BO.LineStation { LineStationIndex = 0, Distance = 0, LineId = line.Code, PrevStation = -1, Station = line.FirstStation, NextStation = line.LastStation, Time = new TimeSpan(0, 0, 0) };
                lSFirst.Name = Find_LineStation_Name(lSFirst.Station);
                Add_LineStaton(lSFirst);
                //make a new line station for the last station in line
                BO.LineStation lSLast = new BO.LineStation { LineStationIndex = 1, Distance = 0, LineId = line.Code, PrevStation = line.FirstStation, Station = line.LastStation, NextStation = -1, Time = new TimeSpan(0, 0, 0) };
                lSLast.Name = Find_LineStation_Name(lSLast.Station);
                Add_LineStaton(lSLast);
                //make a DO line of the new line we want to add
                DO.Line lineDO = new DO.Line { Area = (DO.Enums.Areas)line.Area, LastStation = line.LastStation, Code = line.Code, FirstStation = line.FirstStation, Id = line.Id, Is_Active = true };
                dl.Add_Line(lineDO);// adding the new line to the data source
            }
            catch (DO.Add_Existing_Item_Exception ex)// throw exception if we cant add it
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
            }
        }
        /// <summary>
        /// the func get BO line and delete it if it exist, else throw an exception
        /// </summary>
        /// <param name="line"></param>
        public void Delete_Line(BO.Line line)
        {
            try
            {
                foreach (var item in line.List_Of_LineStation)
                {
                    DO.LineStation lineStationDO = LineStationBoDoAdapter(item);
                    //Delete_LineStation(lineStationDO);
                }
                // make it a DO line 
                DO.Line lineDO = LineBoDoAdapter(line);
                dl.Delete_Line(lineDO);// deiete it 
            }
            catch (DO.Item_not_found_Exception ex)// it we cant delete it
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }
        /// <summary>
        /// the func update a line' get a line in BO and send replace the old one, else throw exception
        /// </summary>
        /// <param name="line"></param>
        public void update_Line(Line line)
        {
            try
            {
                DO.Line lineDO = LineBoDoAdapter(line);// make the BO line to a DO line
                dl.Update_Line(lineDO);// send to update
            }
            catch (DO.Item_not_found_Exception ex)// the line canot update
            {
                throw new BO.Exceptions.Item_not_found_Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineStations"></param>
        /// <param name="id"></param>
        /// <returns></returns>
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
        public IEnumerable<BO.LineStation> Get_All_LineStations_For_Line(BO.Line line)
        {
            return from item in Get_All_LineStations()
                   where (item.LineId == line.Id)
                   select (new BO.LineStation { LineId = item.LineId, Station = item.Station, LineStationIndex = item.LineStationIndex, PrevStation = item.PrevStation, NextStation = item.NextStation });
        }

        DO.LineStation LineStationBoDoAdapter(BO.LineStation lineStationBO)
        {
            DO.LineStation lineStationDO = new DO.LineStation();

            int lineStationIndex = lineStationBO.LineStationIndex;

            lineStationBO.CopyPropertiesTo(lineStationDO);

            return lineStationDO;
        }


        public IEnumerable<LineStation> Get_All_LineStations()
        {
            IEnumerable<DO.LineStation> l = dl.Get_All_LineStations().ToList();
            IEnumerable<BO.LineStation> ls = l.Select(item => new BO.LineStation { LineId = item.LineId, Station = item.Station, LineStationIndex = item.LineStationIndex, PrevStation = item.PrevStation, NextStation = item.NextStation }).ToList();
            foreach (var item in ls)// go over all the line stations
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
                    else// is a last station
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
                DO.LineStation lineStationDO = new DO.LineStation { Is_Active = true, LineId = lineStation.LineId, LineStationIndex = lineStation.LineStationIndex, NextStation = lineStation.NextStation, PrevStation = lineStation.PrevStation, Station = lineStation.Station };
                dl.Add_LineStation(lineStationDO);
            }
            catch (DO.Add_Existing_Item_Exception ex)
            {
                throw new BO.Exceptions.Add_Existing_Item_Exception(ex.Message);
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
                AddAdjacentStationFromStation(s);
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
        #region Line timing
        public IEnumerable<BO.LineTiming> GetLineTiminig(BO.Station s)
        {
            IEnumerable<BO.Line> lineOfStation = FindAllLinesOfThisStation(s);
            return from l in lineOfStation
                   select new BO.LineTiming { LineCode = l.Code, LastStation = l.LastStation };
        }
        #endregion
        #region thread
        public IEnumerable<BO.LineTiming> GetLineTimingForStation(BO.Station stationBO, TimeSpan currentTime)
        {
            //list of lines that pass in the station
            List<BO.Line> listLines = FindAllLinesOfThisStation(stationBO).ToList();

            List<BO.LineTiming> times = new List<BO.LineTiming>();//list of the lines that the function will return
            TimeSpan hour = new TimeSpan(5, 0, 0);//help to find the times that in the range of one hour from currentTime                          
            for (int i = 0; i < listLines.Count(); i++)//for all the lines that pass in the station
            {
                //calculate the times
                TimeSpan tmp;//the current time
                int currentLineid = listLines[i].Id;// line id of the current line

                List<DO.LineTrip> lineSchedual = dl.Get_All_LineTrip().Where(l => l.Id == currentLineid).Select(l => l).ToList();// times of the current Line
                TimeSpan timeTilStation = travelTime(stationBO.Code, listLines[i]);// the time from start till the station
                                                                                   //  List<int> timesOfCurrentLine = new List<int>();

                for (int j = 0; j < lineSchedual.Count; j++)//for all the times in line sSchedual
                {
                    //check if currentTime-LeavingTime-travelTime more than zero and in the range of hour
                    if (lineSchedual[j].StartAt + timeTilStation <= currentTime + hour
                        && lineSchedual[j].StartAt + timeTilStation >= currentTime)
                    //check if the bus already passed the statioin  
                    {
                        if (currentTime - lineSchedual[j].StartAt >= TimeSpan.Zero)//if the line already get out from the station
                        {
                            tmp = timeTilStation - (currentTime - lineSchedual[j].StartAt);// time from the road till the station
                        }
                        else//if the line didnt get out from the station
                            tmp = timeTilStation + (lineSchedual[j].StartAt - currentTime);// time till statr drive till the station


                        times.Add(new BO.LineTiming
                        {
                            LineId = currentLineid,
                            LineCode = listLines[i].Code,
                            LastStation = listLines[i].LastStation,
                            ExpectedTimeTillArrive = tmp,
                            TripStart = lineSchedual[j].StartAt
                        });

                    }
                }
            }
            times = times.OrderBy(lt => lt.LineId).ToList();//order the list by the number of the lines in ascending order
            return times;
        }

        private TimeSpan travelTime(int stationCode, BO.Line line)
        {//func that return the time from first station in line to specific station
            TimeSpan sumTime = TimeSpan.Zero;
            List<BO.LineStation> lsBO = Get_All_LineStations_For_Line(line).ToList().OrderBy(l => l.LineStationIndex).ToList();
            foreach (var s in lsBO)
            {
                if (s.Station == stationCode)
                    sumTime += s.Time;//Time from next station
                else
                {
                    break;
                }
            }
            return sumTime;
        }


        #endregion
        public void Delete_LineStation(LineStation lineStaion)
        {
            DO.LineStation lsDO = new DO.LineStation() { Is_Active = true, LineId = lineStaion.LineId, LineStationIndex = lineStaion.LineStationIndex, NextStation = lineStaion.NextStation, PrevStation = lineStaion.PrevStation, Station = lineStaion.Station };
            dl.Delete_LineStation(lsDO);
        }

        public void AddAdjacentStationFromStation(BO.Station s)
        {
            Random r = new Random();
            foreach (var item in Get_All_Stations())
            {
                BO.AdjacentStations adjacentBO = new BO.AdjacentStations() { Station1 = s.Code, Station2 = item.Code };
                double x = s.Lattitude - item.Lattitude;
                double y = s.Longitude - item.Longitude;
                x = Math.Pow(x, 2);
                y = Math.Pow(y, 2);

                BO.AdjacentStations a = new BO.AdjacentStations
                {
                    Station1 = s.Code,
                    Station2 = item.Code,
                    Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59)),
                    Distance = Math.Sqrt(x + y)
                };
                BO.AdjacentStations b = new BO.AdjacentStations
                {
                    Station1 = item.Code,
                    Station2 = s.Code,
                    Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59)),
                    Distance = Math.Sqrt(x + y)
                };
                AddAdjacentStation(a);
                AddAdjacentStation(b);
            }
        }
    }


};
