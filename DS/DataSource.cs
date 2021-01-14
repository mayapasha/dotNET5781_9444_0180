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
        public static List<LineStation> ListLineStations;//100
        public static List<Bus> ListBuses;//20
        public static List<User> ListUsers;
        public static List<AdjacentStations> ListAdjacentStations;
        public static List<LineTrip> listLineTrip;
        

        static DataSource()
        {
            //DO.RunNumbers.Run_Number_Line_Station = 0;
            
            InitAllLists();
            
            DO.RunNumbers.Run_Number_Line_Station = 0;
        }
        public static void InitAllLists()
        {


            #region listStations
            ListStations = new List<Station>();

            for (int i = 1; i < 51; i++)
            {
                Station s = new Station
                {
                    Code = i,
                    Lattitude = r.NextDouble() * (2.3) + 31,
                    Longitude = r.NextDouble() * (1.2) + 34
                };

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
            ListLines = new List<Line>
            {
                new Line
                {
                    Id = 0,
                    Code = 33,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 2,
                    LastStation = 5
                },
                new Line 
                { 
                    Id = 1,
                    Code = 61, 
                    Area = Enums.Areas.Center, 
                    Is_Active = true, 
                    FirstStation = 1, 
                    LastStation = 7 
                },
                new Line 
                { 
                    Id = 2, 
                    Code = 67, 
                    Area = Enums.Areas.Center, 
                    Is_Active = true, 
                    FirstStation = 2, 
                    LastStation = 6 
                },
                new Line
                {
                    Id = 3,
                    Code = 85,
                    Area = Enums.Areas.Jerusalem,
                    Is_Active = true,
                    FirstStation = 6,
                    LastStation =4
                },
                new Line
                {
                    Id = 4,
                    Code = 131,
                    Area = Enums.Areas.North,
                    Is_Active = true,
                    FirstStation = 1,
                    LastStation = 3
                },
                new Line
                {
                    Id = 5,
                    Code = 999,
                    Area = Enums.Areas.South,
                    Is_Active = true,
                    FirstStation = 8,
                    LastStation = 4
                },
                new Line
                {
                    Id = 6,
                    Code = 44,
                    Area = Enums.Areas.Jerusalem,
                    Is_Active = true,
                    FirstStation = 5,
                    LastStation = 9
                },
                new Line
                {
                    Id = 7,
                    Code = 430,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 2,
                    LastStation = 8
                },
                new Line
                {
                    Id = 8,
                    Code = 2,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 9,
                    LastStation = 1
                },
                new Line
                {
                    Id = 9,
                    Code = 255,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 3,
                    LastStation = 8
                },

            };
            
            #endregion

            #region listAdjacentStations
            ListAdjacentStations = new List<AdjacentStations>();
            for (int i = 0; i < ListStations.Count; i++)
            {
                for (int j = 0; j < ListStations.Count; j++)
                {
                    DO.AdjacentStations a = new AdjacentStations();
                    if (i != j)
                    {
                        a.Station1 = ListStations[j].Code;
                        a.Station2 = ListStations[i].Code;
                        double x = ListStations[j].Lattitude - ListStations[i].Lattitude;
                        double y = ListStations[j].Longitude - ListStations[i].Longitude;
                        x = Math.Pow(x, 2);
                        y = Math.Pow(y, 2);
                        a.Distance = Math.Sqrt(x + y);
                        a.Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59));
                    }
                    ListAdjacentStations.Add(a);
                }
            }
            #endregion

            #region  listLineStation

            /*
            int index_linestation = 0;
            int k = 1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    ListLineStations[index_linestation].LineId = ListLines[i].Code;
                    if (j == 0)
                    {
                        ListLineStations[index_linestation].Station = ListLines[i].FirstStation;
                    }
                    if (j == 9)
                    {
                        ListLineStations[index_linestation].Station = ListLines[i].LastStation;
                    }
                    else
                    {
                        if (ListStations[i * k + j].Code == ListLines[i].FirstStation || ListStations[i * k + j].Code == ListLines[i].LastStation)
                        {

                            ListLineStations[index_linestation].Station = ListStations[i * k + j + 1].Code;
                        }
                        ListLineStations[index_linestation].Station = ListStations[i * k + j ].Code;
                    }
                    ListLineStations[index_linestation].LineStationIndex = j;
                    index_linestation++;
                }
                if(i==2)
                {
                    k++;
                }
                if(i==5)
                {
                    k++;
                }
                if(i==7)
                {
                    k++;
                }
            }
              */
            #endregion

            #region list line trip
            listLineTrip = new List<LineTrip>
            {
                new LineTrip
                {
                    LineId=ListLines[0].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,15,0),
                    StartAt=new TimeSpan(7,0,0),
                    FinishAt=new TimeSpan(12,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[0].Id,
                    Id=2,
                    Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    FinishAt=new TimeSpan(16,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[0].Id,
                    Id=3,
                    Frequency=new TimeSpan(0,20,0),
                    StartAt=new TimeSpan(16,0,0),
                    FinishAt=new TimeSpan(23,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[1].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    FinishAt=new TimeSpan(16,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[2].Id,
                    Id=1,
                    Frequency=new TimeSpan(2,0,0),
                    StartAt=new TimeSpan(8,0,0),
                    FinishAt=new TimeSpan(20,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[3].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,10,0),
                    StartAt=new TimeSpan(7,0,0),
                    FinishAt=new TimeSpan(18,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[3].Id,
                    Id=2,
                    Frequency=new TimeSpan(0,20,0),
                    StartAt=new TimeSpan(18,0,0),
                    FinishAt=new TimeSpan(0,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[4].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,0,0),
                    StartAt=new TimeSpan(12,0,0),
                    FinishAt=new TimeSpan(0,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[5].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    FinishAt=new TimeSpan(19,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[6].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,5,0),
                    StartAt=new TimeSpan(5,0,0),
                    FinishAt=new TimeSpan(13,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[7].Id,
                    Id=1,
                    Frequency=new TimeSpan(1,0,0),
                    StartAt=new TimeSpan(4,0,0),
                    FinishAt=new TimeSpan(7,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[7].Id,
                    Id=2,
                    Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(7,0,0),
                    FinishAt=new TimeSpan(16,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[7].Id,
                    Id=3,
                    Frequency=new TimeSpan(0,15,0),
                    StartAt=new TimeSpan(16,0,0),
                    FinishAt=new TimeSpan(17,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[8].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    FinishAt=new TimeSpan(16,0,0)
                },
                new LineTrip
                {
                    LineId=ListLines[9].Id,
                    Id=1,
                    Frequency=new TimeSpan(0,20,0),
                    StartAt=new TimeSpan(23,0,0),
                    FinishAt=new TimeSpan(4,0,0)
                }
            };
            #endregion
        }

    }
    
}
