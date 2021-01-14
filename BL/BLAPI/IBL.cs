using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLAPI
{
    public interface IBL
    {
        void AddStationToLine(BO.Station s, BO.Line l, BO.LineStation ls);
        #region user
        BO.User Get_User(string username);
        IEnumerable<BO.User> Get_All_Users();
        void Add_User(BO.User user);
        void Delete_user(BO.User user);
        #endregion

        #region bus
        BO.Bus Get_Bus(int lisence);
        IEnumerable<BO.Bus> Get_All_Buses();
        void Add_Bus(BO.Bus bus);
        void Delete_Bus(BO.Bus bus);
        void update_Bus(BO.Bus bus);
        #endregion

        #region line
        BO.Line Get_Line(int id);
        IEnumerable<BO.Line> Get_All_Lines();
        void Add_Line(BO.Line line);
        void Delete_Line(BO.Line line);
        void update_Line(BO.Line line);
        IEnumerable<BO.Station> Line_Station_To_Station(IEnumerable<BO.LineStation> lineStations, int id);
        #endregion

        #region line station
        BO.LineStation Get_LineStation(int lineStationIndex);
        IEnumerable<BO.LineStation> Get_All_LineStations();
        void Add_LineStaton(BO.LineStation lineStation);
        void Delete_LineStation(BO.LineStation lineStaion);
        void update_LineStation(BO.LineStation lineStation);
        #endregion

        #region station
        IEnumerable<BO.Station> Get_All_Stations();
        BO.Station GetStation(int n);
        void AddStation(BO.Station s);
        void DeleteStation(BO.Station s);
        void UpdateStation(BO.Station s);
        #endregion
        #region AdjacentSatations
        BO.AdjacentStations Get_AdjacentStation(int x, int y);
        void AddAdjacentStation(BO.AdjacentStations s);
        void DeleteAdjacentStation(BO.AdjacentStations s);
        void UpdateAdjacentStation(BO.AdjacentStations s);
        #endregion
    }
}
