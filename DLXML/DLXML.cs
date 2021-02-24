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

        public Line Get_Line(int id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            DO.Line lineDO = ListLines.Find(l => l.Id == id);
            if (lineDO != null)
                return lineDO; //no need to Clone()
            else
                throw new DO.Item_not_found_Exception("this line " + id + " has not found");
        }

        public IEnumerable<Line> Get_All_Lines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            return from line in ListLines
                   select line; //no need to Clone()
        }

        public void Add_Line(Line line)
        {
            List<Line> Listlines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            if (Listlines.FirstOrDefault(l => l.Id == line.Id) != null)
                throw new DO.Add_Existing_Item_Exception("this line " + line.Id + " is already exist");

            Listlines.Add(line); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(Listlines, linePath);
        }

        public void Delete_Line(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            DO.Line lineDO = ListLines.Find(l => l.Id == line.Id);

            if (lineDO != null)
            {
                ListLines.Remove(lineDO);
            }
            else
                throw new DO.Item_not_found_Exception("this line is not found");

            XMLTools.SaveListToXMLSerializer(ListLines, linePath);
        }

        public void Update_Line(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            DO.Line lineDO = ListLines.Find(l => l.Id == line.Id);
            if (lineDO != null && lineDO.Is_Active == true)
            {
                ListLines.Remove(lineDO);
                line.Is_Active = true;
                ListLines.Add(line);
            }
            else
            {
                throw new Item_not_found_Exception("the line " + line.Id + " was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListLines, linePath);
        }
        #endregion
        #region station
        public Station Get_Station(int code)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);

            Station p = (from sta in stationRootElem.Elements()
                         where int.Parse(sta.Element("Code").Value) == code
                         select new Station()
                         {
                             Code = Int32.Parse(sta.Element("Code").Value),
                             Name = sta.Element("Name").Value,
                             Is_Active = bool.Parse(sta.Element("Is_Active").Value),
                             Longitude = Double.Parse(sta.Element("Longitude").Value),
                             Lattitude = Double.Parse(sta.Element("Lattitude").Value),
                         }
                        ).FirstOrDefault();

            if (p == null)
                throw new DO.Item_not_found_Exception("this station is not exist");

            return p;
        }

        public IEnumerable<Station> Get_All_Stations()
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);

            return (from sta in stationRootElem.Elements()
                    select new Station()
                    {
                        Code = Int32.Parse(sta.Element("Code").Value),
                        Name = sta.Element("Name").Value,
                        Is_Active = bool.Parse(sta.Element("Is_Active").Value),
                        Longitude = Double.Parse(sta.Element("Longitude").Value),
                        Lattitude = Double.Parse(sta.Element("Lattitude").Value),
                    }
                   );
        }

        public void Add_Station(Station station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);

            XElement sta1 = (from s in stationRootElem.Elements()
                             where int.Parse(s.Element("Code").Value) == station.Code
                             select s).FirstOrDefault();

            if (sta1 != null)
                throw new DO.Add_Existing_Item_Exception("this station is already exist");

            XElement stationElem = new XElement("Station", new XElement("Code", station.Code),
                                  new XElement("Name", station.Name),
                                  new XElement("Longitude", station.Longitude),
                                  new XElement("lattitude", station.Lattitude),
                                  new XElement("Is_Active", station.Is_Active));


            stationRootElem.Add(stationElem);

            XMLTools.SaveListToXMLElement(stationRootElem, stationPath);
        }

        public void Delete_Station(Station station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);

            XElement sta = (from s in stationRootElem.Elements()
                            where int.Parse(s.Element("Code").Value) == station.Code
                            select s).FirstOrDefault();

            if (sta != null)
            {
                sta.Remove(); //<==>   Remove per from personsRootElem

                XMLTools.SaveListToXMLElement(stationRootElem, stationPath);
            }
            else
                throw new DO.Item_not_found_Exception("this station is not found");
        }

        public void Update_Station(Station station)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationPath);

            XElement sta = (from s in stationRootElem.Elements()
                            where int.Parse(s.Element("Code").Value) == station.Code
                            select s).FirstOrDefault();

            if (sta != null)
            {
                sta.Element("Code").Value = station.Code.ToString();
                sta.Element("Name").Value = station.Name;
                sta.Element("Longitude").Value = station.Longitude.ToString();
                sta.Element("Lattitude").Value = station.Lattitude.ToString();
                sta.Element("Is_Active").Value = station.Is_Active.ToString();


                XMLTools.SaveListToXMLElement(stationRootElem, stationPath);
            }
            else
                throw new DO.Item_not_found_Exception("this station is not found");
        }
        #endregion
        #region line station
        /*  public LineStation Get_LineStation(int linestationIndex)
          {
              List<LineStation> LineStationLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

              DO.LineStation lineStationDO = LineStationLines.Find(l => l.Id == id);
              if (lineStationDO != null)
                  return lineStationDO; //no need to Clone()
              else
                  throw new DO.Item_not_found_Exception("this line " + id + " has not found");
          }*/

        public IEnumerable<LineStation> Get_All_LineStations()
        {

            List<LineStation> ListStationsLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            return from lineStation in ListStationsLines
                   select lineStation; //no need to Clone()
        }

        public void Add_LineStation(LineStation lineStation)
        {
            List<LineStation> ListStationsLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            if (ListStationsLines.FirstOrDefault(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station) != null)
                throw new DO.Add_Existing_Item_Exception("this line station is already exist");

            ListStationsLines.Add(lineStation); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListStationsLines, lineStationPath);
        }

        /* public void Delete_LineStation(LineStation lineStation)
         {
             List<LineStation> ListLines = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

             DO.Line lineDO = ListLines.Find(l => l.Id == line.Id);

             if (lineDO != null)
             {
                 ListLines.Remove(lineDO);
             }
             else
                 throw new DO.Item_not_found_Exception("this line is not found");

             XMLTools.SaveListToXMLSerializer(ListLines, linePath);
         }
        */
        public void Update_LineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            DO.LineStation lineStationDO = ListLineStations.Find(l => l.LineId == lineStation.LineId && l.Station == lineStation.Station);
            if (lineStationDO != null && lineStationDO.Is_Active == true)
            {
                ListLineStations.Remove(lineStationDO);
                lineStation.Is_Active = true;
                ListLineStations.Add(lineStation);
            }
            else
            {
                throw new Item_not_found_Exception("the line station was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
        }
        #endregion
        #region adjacent station
        public AdjacentStations GetAdjacentStations(int x, int y)
        {//item.Station1==x&& item.Station2==y&& item.Is_Active==true

            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);

            DO.AdjacentStations adjacentStationsDO = ListAdjacentStations.Find(item => item.Station1 == x && item.Station2 == y && item.Is_Active == true);
            if (adjacentStationsDO != null)
                return adjacentStationsDO; //no need to Clone()
            else
                throw new DO.Item_not_found_Exception("this adjacent Stations has not found");
        }

        public void DeleteAdjacentStations(AdjacentStations x)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);

            DO.AdjacentStations adjacentStationsDO = ListAdjacentStations.Find(item => item.Station1 == x.Station1 && item.Station2 == x.Station2 && item.Is_Active == true);

            if (adjacentStationsDO != null)
            {
                ListAdjacentStations.Remove(adjacentStationsDO);
            }
            else
                throw new DO.Item_not_found_Exception("this Adjacent Station is not found");

            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationPath);
        }

        public void AddAdjacentStation(AdjacentStations x)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);

            if (ListAdjacentStations.FirstOrDefault(adj => adj.Station1 == x.Station1 && adj.Station2 == x.Station2 && adj.Is_Active == true) != null)
                throw new DO.Add_Existing_Item_Exception("this Adjacen tStation is already exist");

            ListAdjacentStations.Add(x); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationPath);
        }

        public void UpdateAdjecentStation(AdjacentStations x)
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);

            DO.AdjacentStations adjacentStationsDO = ListAdjacentStations.Find(item => item.Station1 == x.Station1 && item.Station2 == x.Station2 && item.Is_Active == true);
            if (adjacentStationsDO != null && adjacentStationsDO.Is_Active == true)
            {
                ListAdjacentStations.Remove(adjacentStationsDO);
                adjacentStationsDO.Is_Active = true;
                ListAdjacentStations.Add(x);
            }
            else
            {
                throw new Item_not_found_Exception("the Adjacent Stations was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListAdjacentStations, adjacentStationPath);
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            List<AdjacentStations> ListAdjacentStations = XMLTools.LoadListFromXMLSerializer<AdjacentStations>(adjacentStationPath);


            return from adjacentStation in ListAdjacentStations
                   select adjacentStation; //no need to Clone()   
        }

        #endregion
        #region line trip
        public LineTrip GetLineTrip(int lineId, int i)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            DO.LineTrip lineTripDO = ListLineTrips.Find(l => l.LineId == lineId && l.Id == i && l.Is_Active == true);
            if (lineTripDO != null)
                return lineTripDO; //no need to Clone()
            else
                throw new DO.Item_not_found_Exception("this line trip has not found");
        }

        public void DeleteLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            DO.LineTrip lineTripDO = ListLineTrips.Find(l => l.LineId == lineTrip.LineId);

            if (lineTripDO != null)
            {
                ListLineTrips.Remove(lineTripDO);
            }
            else
                throw new DO.Item_not_found_Exception("this line is not found");

            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripPath);
        }

        public void UpdateLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            DO.LineTrip lineTripDO = ListLineTrips.Find(l => l.LineId == lineTrip.LineId && l.Id == lineTrip.Id && l.Is_Active == true);
            if (lineTripDO != null && lineTripDO.Is_Active == true)
            {
                ListLineTrips.Remove(lineTripDO);
                lineTrip.Is_Active = true;
                ListLineTrips.Add(lineTrip);
            }
            else
            {
                throw new Item_not_found_Exception("the line " + lineTrip.Id + " was not found");
            }
            XMLTools.SaveListToXMLSerializer(ListLineTrips, lineTripPath);
        }

        public void AddLineTrip(LineTrip lineTrip)
        {
            List<LineTrip> ListlineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            if (ListlineTrips.FirstOrDefault(l => l.LineId == lineTrip.LineId && l.Id == lineTrip.Id && l.Is_Active == true) != null)
                throw new DO.Add_Existing_Item_Exception("this line trip is already exist");

            ListlineTrips.Add(lineTrip); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListlineTrips, lineTripPath);
        }

        public IEnumerable<LineTrip> Get_All_LineTrip()
        {
            List<LineTrip> ListLineTrips = XMLTools.LoadListFromXMLSerializer<LineTrip>(lineTripPath);

            return from lineTrip in ListLineTrips
                   select lineTrip; //no need to Clone()
        }

        #endregion



    }
}