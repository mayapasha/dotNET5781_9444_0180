using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DS
{
    public static class DataSource
    {
        public static Random r = new Random();
        public static List<Station> ListStations;//50
        public static List<Line> ListLines;//10
        public static List<LineStation> ListLineStations;//10
        public static List<Bus> ListBuses;//20
        public static List<User> ListUsers;
        
        static DataSource()
        {
            //DO.RunNumbers.Run_Number_Line_Station = 0;

            InitAllLists();
            DO.RunNumbers.Run_Number_Line_Station = 0;
        }
        static void InitAllLists()
        {
            #region listStations
            ListStations = new List<Station>();

            for (int i = 1; i < 51; i++)
            {
                Station s = new Station();
                s.Code = i;
                s.Lattitude = r.Next() * (2) + 31;
                s.Longitude = r.Next() * (1) + 34;
                ListStations.Add(s);
            }
            ListStations[0].Name = "Masof riding";
            ListStations[1].Name = "Aven gvirol/ shderot rokah";
            ListStations[2].Name = "Shay agnon/ shderot levi eshkol";
            ListStations[3].Name = "Sde dov/shderot levi eshkol";
            ListStations[4].Name = "Eren school/ Yehuda borla";
            ListStations[5].Name = "Yehuda borla/ Harav hauzner";
            ListStations[6].Name = "Ainstain/shderot levi eshkol ";
            ListStations[7].Name = "Haoniversita/ haim lavnon";
            ListStations[8].Name = "haim lavnon/ kalchkin";
            ListStations[9].Name = "shderot kakal/ ofakim";
            ListStations[10].Name = "afka/shderot kakal";
            ListStations[11].Name = "machlef kakal/shderot kakal";
            ListStations[12].Name = "shderot kakal/bnei efraim";
            ListStations[13].Name = "shlonski/bnei efraim";
            ListStations[14].Name = "shlonski/kehilat venezia";
            ListStations[15].Name = "pinchas rozen/ shlonski";
            ListStations[16].Name = "pinchas rozen/ kehilat yasi";
            ListStations[17].Name = "mivza kadesh/ raul velemberg";
            ListStations[18].Name = "tachanat rakevet bnei brak/ mivza kadesh";
            ListStations[19].Name = "derech ben guryon/derech sheshet hayamim";
            ListStations[20].Name = "ulamei modi'in";
            ListStations[21].Name = "derech ben guryon/ derech zabutinski";
            ListStations[22].Name = "derech ben guryon/ magadim";
            ListStations[23].Name = "derech ben guryon/ shalishut";
            ListStations[24].Name = "haroe/ derech ben guryon";
            ListStations[25].Name = "haroe/yerushalaim";
            ListStations[26].Name = "haroe/ hibat ziyon";
            ListStations[27].Name = "haroe/barkay";
            ListStations[28].Name = "haroe/ shderot yerushalaim";
            ListStations[29].Name = "haroe/ truman";
            ListStations[30].Name = "haroe/ pinchas rotenberg";
            ListStations[31].Name = "haroe/ derech izhak rabin";
            ListStations[32].Name = "raziel/hamea veachat";
            ListStations[33].Name = "hapark haleumi/ san martino";
            ListStations[34].Name = "biet sefer netaim/ yahadut filadelfia";
            ListStations[35].Name = "azriel/givati";
            ListStations[36].Name = "kikar harav pardes/ shalem";
            ListStations[37].Name = "iztadion vinter/ echad ha'am";
            ListStations[38].Name = "iztadion vinter";
            ListStations[39].Name = "amidar ramat gan";
            ListStations[40].Name = "hatfuzot/ komemiut";
            ListStations[41].Name = "ezel/ hamachteret";
            ListStations[42].Name = "ezel/ ben eliezer";
            ListStations[43].Name = "ben eliezer/ ezel";
            ListStations[44].Name = "pinchas rotenberg/ ben eliezer";
            ListStations[45].Name = "hayarden/pinchas";
            ListStations[46].Name = "hayarden/ezel";
            ListStations[47].Name = "negba/hayarden";
            ListStations[48].Name = "negba/haroe";
            ListStations[49].Name = "arlozerov/ bialik";
            #endregion

            #region listLine
            
            #endregion
        }

    }
    
}
