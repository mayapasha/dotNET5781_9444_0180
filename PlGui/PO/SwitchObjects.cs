using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static PO.Line LineBoToPo(BO.Line l)
        {
            PO.Line linePO = new Line();
            linePO.Area = l.Area;
            linePO.Code = l.Code;
            linePO.Id = l.Id;
            linePO.LastStation = l.LastStation;
            IEnumerable<BO.LineStation> ls = l.List_Of_LineStation;
            IEnumerable<PO.LineStation> lsPO = from item in ls
                                               select LineStationBoToPo(item);
            linePO.List_Of_Line_Stations = (ObservableCollection<LineStation>)lsPO;
            linePO.FirstStation = l.FirstStation;
            return linePO;
        }
        public static PO.Station StationBoToPo(BO.Station s)
        {
            PO.Station stationPO = new Station();
            stationPO.Name = s.Name;
            stationPO.Code = s.Code;
            stationPO.Lattitude = s.Lattitude;
            stationPO.Longitude = s.Longitude;
            return stationPO;
        }
        public static PO.LineStation LineStationBoToPo(BO.LineStation ls)
        {
            PO.LineStation lineStationPO = new LineStation();
            lineStationPO.LineId = ls.LineId;
            lineStationPO.Station = ls.Station;
            lineStationPO.LineStationIndex = ls.LineStationIndex;
            lineStationPO.PrevStation = ls.PrevStation;
            lineStationPO.NextStation = ls.NextStation;
            lineStationPO.Distance = ls.Distance;
            lineStationPO.Time = ls.Time;
            return lineStationPO;
        }
    }
}
