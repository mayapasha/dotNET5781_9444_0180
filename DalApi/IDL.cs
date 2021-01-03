using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDL
    {
        #region user
        DO.User Get_User(string Username);
        IEnumerable<DO.User> Get_All_Users();
        void Add_User(DO.User user);
        void Delete_User(DO.User user);
        #endregion

        #region bus
        DO.Bus Get_Bus(int license);
        IEnumerable<DO.Bus> Get_All_Buses();
        void Add_Bus(DO.Bus bus);
        void Delete_Bus(DO.Bus bus);
        void Update_Bus(DO.Bus bus);
        #endregion

        #region line
        DO.Line Get_Line(int id);
        IEnumerable<DO.Line> Get_All_Lines();
        void Add_Line(DO.Line line);
        void Delete_Line(DO.Line line);
        void Update_Line(DO.Line line);
        #endregion

        #region station
        DO.Station Get_Station(int id);
        IEnumerable<DO.Station> Get_All_Stations();
        void Add_Station(DO.Station station);
        void Delete_Station(DO.Station station);
        void Update_Station(DO.Station station);
        #endregion

        #region line station
        DO.LineStation Get_LineStation(int linestationIndex);
        IEnumerable<DO.LineStation> Get_All_LineStations();
        void Add_LineStation(DO.LineStation lineStation);
        void Delete_LineStation(DO.LineStation lineStation);
        void Update_LineStation(DO.LineStation lineStation);
        #endregion
    }
}
