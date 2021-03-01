using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Xml.Linq;

namespace DL
{
    public class DLXML : IDL
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string adjacentStationPath = @"AdjacentStationXML.xml"; //XMLSerializer

        string lineStationPath = @"LineStationXML.xml"; //XMLSerializer
        string lineTripPath = @"LineTripXML.xml"; //XMLSerializer
        string linePath = @"LineXML.xml"; //XMLSerializer
        string stationPath = @"StationXML.xml"; //XElement
        #endregion



        #region line


        /// <summary>
        /// the func get an id of a line and try to find it in the xml files, elas it thorw an exception
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Line Get_Line(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);// get all the lines from the file

            DO.Line lineDO = ListLines.Find(l => l.Id == id && l.Is_Active == true);//get the line
            if (lineDO != null)// check if the line exist
                return lineDO; //no need to Clone()
            else//the line with this id is not exist
                throw new DO.Item_not_found_Exception("this line " + id + " has not found");
        }
        /// <summary>
        /// returns alll the lines in the xml files
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Line> Get_All_Lines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);//get all the lines from file 

            return from line in ListLines// link  of getting all the lines that is still exist
                   where line.Is_Active == true
                   select line; //no need to Clone()
        }
        /// <summary>
        /// the func add the line we gat if it is not exist, elas it thorws an excption
        /// </summary>
        /// <param name="line"></param>
        public void Add_Line(Line line)
        {
            List<Line> Listlines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            if (Listlines.FirstOrDefault(l => l.Id == line.Id && l.Is_Active == true) != null)//find if the line exist alreaddy ->throw exception
                throw new DO.Add_Existing_Item_Exception("this line " + line.Id + " is already exist");
            Listlines.Add(line); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(Listlines, linePath);// sava the changing in the fine xml
        }
        /// <summary>
        /// the func is get a line and deletet it or throw an exception
        /// </summary>
        /// <param name="line"></param>
        public void Delete_Line(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            DO.Line lineDO = ListLines.Find(l => l.Id == line.Id && l.Is_Active == true);//the line is exist

            if (lineDO != null)
            {
                lineDO.Is_Active = false;
                // ListLines.Remove(lineDO);
            }
            else// the line is not exist
                throw new DO.Item_not_found_Exception("this line is not found");

            XMLTools.SaveListToXMLSerializer(ListLines, linePath);//save the changing in the file
        }
        /// <summary>
        /// the func get an updated line' find the old one and remove it and save the new line->else thorw an exception
        /// </summary>
        /// <param name="line"></param>
        public void Update_Line(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            DO.Line lineDO = ListLines.Find(l => l.Id == line.Id && l.Is_Active == true);// get the old line
            if (lineDO != null && lineDO.Is_Active == true)//if it exist
            {
                ListLines.Remove(lineDO);//remove the old one
                line.Is_Active = true;// make the updated one exist
                ListLines.Add(line);// adding the updated one to the list
            }
            else//the line is not exist
            {
                throw new Item_not_found_Exception("the line " + line.Id + " was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListLines, linePath);//save the changes into the file
        }

        #endregion
        #region station
        /// <summary>
        /// get code of station and return this station and throw exception if its not exist
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Station Get_Station(int code)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);// load all the stations from the file

            Station p = (from sta in stationRootElem.Elements()
                         where int.Parse(sta.Element("Code").Value) == code// if its the right station
                         select new Station()// create new station with the data of the station that we want to return
                         {
                             Code = Int32.Parse(sta.Element("Code").Value),
                             Name = sta.Element("Name").Value,
                             Is_Active = bool.Parse(sta.Element("Is_Active").Value),
                             Longitude = Double.Parse(sta.Element("Longitude").Value),
                             Lattitude = Double.Parse(sta.Element("Lattitude").Value),
                         }
                        ).FirstOrDefault();

            if (p == null)// if the station not exist
                throw new DO.Item_not_found_Exception("this station is not exist");

            return p;
        }
        /// <summary>
        /// return all the stations in the file
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Station> Get_All_Stations()
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);//load the file

            return (from sta in stationRootElem.Elements()
                    select new Station()//get the station from the root element
                    {
                        Code = Int32.Parse(sta.Element("Code").Value),
                        Name = sta.Element("Name").Value,
                        Is_Active = bool.Parse(sta.Element("Is_Active").Value),
                        Longitude = Double.Parse(sta.Element("Longitude").Value),
                        Lattitude = Double.Parse(sta.Element("Lattitude").Value),
                    }
                   );
        }
        /// <summary>
        /// get station and add it if it wasnt exist before, else throw exception
        /// </summary>
        /// <param name="station"></param>
        public void Add_Station(Station station)
        {
            station.Is_Active = true;
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);//load the file

            XElement sta1 = (from s in stationRootElem.Elements()//check if the station exist
                             where int.Parse(s.Element("Code").Value) == station.Code && bool.Parse(s.Element("Is_Active").Value) == true
                             select s).FirstOrDefault();

            if (sta1 != null)// if its exist
                throw new DO.Add_Existing_Item_Exception("this station is already exist");
            //create xelement from the station
            XElement stationElem = new XElement("Station", new XElement("Code", station.Code),
                                  new XElement("Name", station.Name),
                                  new XElement("Longitude", station.Longitude),
                                  new XElement("lattitude", station.Lattitude),
                                  new XElement("Is_Active", station.Is_Active));


            stationRootElem.Add(stationElem);// add the station

            XMLTools.SaveListToXMLElement(stationRootElem, stationPath);//save the changes
        }
        /// <summary>
        /// get a station and delete it if its exist, else throw exception
        /// </summary>
        /// <param name="station"></param>
        public void Delete_Station(Station station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);//load the file

            XElement sta = (from s in stationRootElem.Elements()//search the station that we want to delete
                            where int.Parse(s.Element("Code").Value) == station.Code && bool.Parse(s.Element("Is_Active").Value) == true
                            select s).FirstOrDefault();

            if (sta != null)//if the station is exist
            {
                sta.Element("Is_Active").Value = false.ToString();
                //   sta.Remove(); //<==>   Remove per from personsRootElem
                XMLTools.SaveListToXMLElement(stationRootElem, stationPath);//save the changes
            }
            else//if its not exist
                throw new DO.Item_not_found_Exception("this station is not found");
        }
        /// <summary>
        /// get updated station and delete the old station and add the new one, if it wasnt exist before 
        /// throw exception
        /// </summary>
        /// <param name="station"></param>
        public void Update_Station(Station station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);//load the file

            XElement sta = (from s in stationRootElem.Elements()//try to find if the station exist
                            where int.Parse(s.Element("Code").Value) == station.Code
                            select s).FirstOrDefault();

            if (sta != null)//if its exist
            {
                sta.Element("Code").Value = station.Code.ToString();
                sta.Element("Name").Value = station.Name;
                sta.Element("Longitude").Value = station.Longitude.ToString();
                sta.Element("Lattitude").Value = station.Lattitude.ToString();
                sta.Element("Is_Active").Value = station.Is_Active.ToString();


                XMLTools.SaveListToXMLElement(stationRootElem, stationPath);// save the changes
            }
            else//if its not exist
                throw new DO.Item_not_found_Exception("this station is not found");
        }
        #endregion
        #region line station
        /*
        public LineStation Get_LineStation(int linestationIndex)
        {
            List<LineStation> LineStationLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            DO.LineStation lineStationDO = LineStationLines.Find(l => l.Id == id);
            if (lineStationDO != null)
                return lineStationDO; //no need to Clone()
            else
                throw new DO.Item_not_found_Exception("this line " + id + " has not found");
        }
        */
        /// <summary>
        /// the func return all the line station from the xml file
        /// </summary>
        /// <returns></returns>

        public IEnumerable<LineStation> Get_All_LineStations()
        {

            List<LineStation> ListStationsLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);// load all the info from the file

            return from lineStation in ListStationsLines// get all the line station that exist
                   where lineStation.Is_Active == true
                   select lineStation; //no need to Clone()
        }
        /// <summary>
        /// the func get a line station and add it if it not exist, else throw an exception
        /// </summary>
        /// <param name="lineStation"></param>
        public void Add_LineStation(LineStation lineStation)
        {
            List<LineStation> ListStationsLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);//load the  info from the file

            if (ListStationsLines.FirstOrDefault(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station && l.Is_Active == true) != null)// if the line station is exist
                throw new DO.Add_Existing_Item_Exception("this line station is already exist");

            ListStationsLines.Add(lineStation); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListStationsLines, lineStationPath);// save the changes
        }

        public void Delete_LineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            DO.LineStation lineSDO = ListLineStations.Find(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station && l.Is_Active == true);

            if (lineSDO != null)
            {
                //ListLineStations.Remove(lineSDO);
                ListLineStations.Find(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station && l.Is_Active == true).Is_Active = false;
            }
            else
                throw new DO.Item_not_found_Exception("this line is not found");

            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
        }

        /// <summary>
        /// the func get an updated line s and if it exist it replace it with the old one, else thorw exception
        /// </summary>
        /// <param name="lineStation"></param>
        public void Update_LineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);// load the info from the file

            DO.LineStation lineStationDO = ListLineStations.Find(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station && l.Is_Active == true);// check if it exist
            if (lineStationDO != null && lineStationDO.Is_Active == true)
            {
                ListLineStations.Remove(lineStationDO);// remove the old one
                lineStation.Is_Active = true;
                ListLineStations.Add(lineStation);// adding the update one
            }
            else//if it not exist
            {
                throw new Item_not_found_Exception("the line station was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);// save the changes
        }

        #endregion
        #region adjacent station


        /// <summary>
        /// the func get the code of two stations and get the adj station of them, else throw exception
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public AdjacentStations GetAdjacentStations(int x, int y)
        {

            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);//load the file

            DO.AdjacentStations adjacentStationsDO = ListAdjacentStations.Find(item => item.Station1 == x && item.Station2 == y && item.Is_Active == true);//check if its exist
            if (adjacentStationsDO != null)//if its exist
                return adjacentStationsDO; //no need to Clone()
            else//if its not exist
                throw new DO.Item_not_found_Exception("this adjacent Stations has not found");
        }
        /// <summary>
        /// get adjacent station and delete it if its exist, else throw exception
        /// </summary>
        /// <param name="x"></param>
        public void DeleteAdjacentStations(AdjacentStations x)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);//load the file

            DO.AdjacentStations adjacentStationsDO = ListAdjacentStations.Find(item => item.Station1 == x.Station1 && item.Station2 == x.Station2 && item.Is_Active == true);//check if its exist

            if (adjacentStationsDO != null)//if its exist
            {
                ListAdjacentStations.Remove(adjacentStationsDO);// remove
                adjacentStationsDO.Is_Active = false;//change the flag to false
                ListAdjacentStations.Add(adjacentStationsDO);//add the removed adjacent station
            }
            else//if its not exist
                throw new DO.Item_not_found_Exception("this Adjacent Station is not found");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationPath);//save the changes
        }
        /// <summary>
        /// get adjacent station and add it ifit wasnt exist before, else throw exception
        /// </summary>
        /// <param name="x"></param>
        public void AddAdjacentStation(AdjacentStations x)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);
            //if its exist
            if (ListAdjacentStations.FirstOrDefault(adj => adj.Station1 == x.Station1 && adj.Station2 == x.Station2 && adj.Is_Active == true) != null)
                throw new DO.Add_Existing_Item_Exception("this Adjacen tStation is already exist");
            x.Is_Active = true;
            ListAdjacentStations.Add(x); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationPath);//save the changes
        }
        /// <summary>
        /// get updated adjacent station and find an ole adjacent staion of it, if exit we replace it with the new one, else we throw exception
        /// </summary>
        /// <param name="x"></param>
        public void UpdateAdjecentStation(AdjacentStations x)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);//load the file

            DO.AdjacentStations adjacentStationsDO = ListAdjacentStations.Find(item => item.Station1 == x.Station1 && item.Station2 == x.Station2 && item.Is_Active == true);// find the adj station
            if (adjacentStationsDO != null && adjacentStationsDO.Is_Active == true)// if it already exist
            {
                ListAdjacentStations.Remove(adjacentStationsDO);// remove the old one
                adjacentStationsDO.Is_Active = true;
                ListAdjacentStations.Add(x);//adding the updated one
            }
            else//it is not exist
            {
                throw new Item_not_found_Exception("the Adjacent Stations was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationPath);//save the changes
        }
        /// <summary>
        /// the func return all the adj station 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);// load the file


            return from adjacentStation in ListAdjacentStations
                   where adjacentStation.Is_Active == true
                   select adjacentStation; //no need to Clone()   
        }

        #endregion
        #region line trip
        /// <summary>
        /// the func get a line id and an index and find the linetrip from it, else throw exception
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public LineTrip GetLineTrip(int lineId, int i)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);//load the file

            DO.LineTrip lineTripDO = ListLineTrips.Find(l => l.LineId == lineId && l.Id == i && l.Is_Active == true);//find the line trip
            if (lineTripDO != null)//is exist
                return lineTripDO; //no need to Clone()
            else// is not exist
                throw new DO.Item_not_found_Exception("this line trip has not found");
        }
        /// <summary>
        /// the func get a line trup and delete it if exist, else throw exception
        /// </summary>
        /// <param name="lineTrip"></param>
        public void DeleteLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);// load the file

            DO.LineTrip lineTripDO = ListLineTrips.Find(l => l.LineId == lineTrip.LineId && l.Is_Active == true);// find the line trup

            if (lineTripDO != null)// is exist
            {
                ListLineTrips.Find(l => l.LineId == lineTrip.LineId && l.Is_Active == true).Is_Active = false;//delete the line trip
            }
            else// it is not exist
                throw new DO.Item_not_found_Exception("this line is not found");

            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripPath);//save the changes
        }
        /// <summary>
        /// the func get an updated line trip and replace it with an old one if exist, else thorw exception
        /// </summary>
        /// <param name="lineTrip"></param>
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);//load the file

            DO.LineTrip lineTripDO = ListLineTrips.Find(l => l.LineId == lineTrip.LineId && l.Id == lineTrip.Id && l.Is_Active == true);// find the old one
            if (lineTripDO != null && lineTripDO.Is_Active == true)//if it exist
            {
                ListLineTrips.Remove(lineTripDO);//remove the old one
                lineTrip.Is_Active = true;
                ListLineTrips.Add(lineTrip);//add the new one
            }
            else// is not exist
            {
                throw new Item_not_found_Exception("the line " + lineTrip.Id + " was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripPath);//save the changes
        }
        /// <summary>
        /// the func get a line trip and adding it to the xml file if exist, else thorw exception
        /// </summary>
        /// <param name="lineTrip"></param>
        public void AddLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListlineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);//load the file

            if (ListlineTrips.FirstOrDefault(l => l.LineId == lineTrip.LineId && l.Id == lineTrip.Id && l.Is_Active == true) != null)// if the line trip exist
                throw new DO.Add_Existing_Item_Exception("this line trip is already exist");
            //the line trip not exist
            lineTrip.Is_Active = true;
            ListlineTrips.Add(lineTrip); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListlineTrips, lineTripPath);//save the changes
        }
        /// <summary>
        /// the fnuc return all the line trips
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LineTrip> Get_All_LineTrip()
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);//load the file

            return from lineTrip in ListLineTrips//get all exists line trip
                   where lineTrip.Is_Active == true
                   select lineTrip; //no need to Clone()
        }

        #endregion
    }
}