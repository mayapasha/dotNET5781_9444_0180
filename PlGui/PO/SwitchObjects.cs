using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public static class SwitchObjects
    {
        public static BO.Line LinePoToBo(PO.Line l)
        {
            BO.Line lineBO = new BO.Line();
            lineBO.Code = l.Code;
            lineBO.Area = l.Area;
            lineBO.FirstStation = l.FirstStation;
            lineBO.Id = l.Id;
            lineBO.LastStation = l.LastStation;
            lineBO.List_Of_LineStation = (IEnumerable<BO.LineStation>)l.List_Of_Line_Stations;
            return lineBO;
        }
       public static BO.LineStation LineStationPoToBo(PO.LineStation l)
        {
            BO.LineStation lineStationBo = new BO.LineStation();
            lineStationBo.LineId = l.LineId;
            lineStationBo.LineStationIndex = l.LineStationIndex;
            lineStationBo.NextStation = l.NextStation;
            lineStationBo.PrevStation = l.PrevStation;
            lineStationBo.Station = l.Station;
            return lineStationBo;
        }
       public static BO.Station StationPoToBo(PO.Station s)
        {
            BO.Station stationBO = new BO.Station();
            stationBO.Code = s.Code;
            stationBO.Lattitude = s.Lattitude;
            stationBO.Longitude = s.Longitude;
            stationBO.Name = s.Name;
            return stationBO;
        }
    }
}
