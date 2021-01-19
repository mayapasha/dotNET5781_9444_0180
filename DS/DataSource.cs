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
                    Longitude = r.NextDouble() * (1.2) + 34,
                    Is_Active = true
                    
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
                    FirstStation = 0,
                    LastStation = 9
                },
                new Line
                {
                    Id = 2,
                    Code = 67,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 10,
                    LastStation = 19
                },
                new Line
                {
                    Id = 3,
                    Code = 85,
                    Area = Enums.Areas.Jerusalem,
                    Is_Active = true,
                    FirstStation = 20,
                    LastStation =29
                },
                new Line
                {
                    Id = 4,
                    Code = 131,
                    Area = Enums.Areas.North,
                    Is_Active = true,
                    FirstStation = 30,
                    LastStation = 39
                },
                new Line
                {
                    Id = 5,
                    Code = 999,
                    Area = Enums.Areas.South,
                    Is_Active = true,
                    FirstStation = 40,
                    LastStation = 49
                },
                new Line
                {
                    Id = 6,
                    Code = 44,
                    Area = Enums.Areas.Jerusalem,
                    Is_Active = true,
                    FirstStation = 5,
                    LastStation = 14
                },
                new Line
                {
                    Id = 7,
                    Code = 430,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 15,
                    LastStation = 24
                },
                new Line
                {
                    Id = 8,
                    Code = 2,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 25,
                    LastStation = 34
                },
                new Line
                {
                    Id = 9,
                    Code = 255,
                    Area = Enums.Areas.Center,
                    Is_Active = true,
                    FirstStation = 36,
                    LastStation = 45
                },

            };

            #endregion

            #region listAdjacentStations

            ListAdjacentStations = new List<AdjacentStations>();
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    if (j != i)
                    {

                        double x = ListStations[j].Lattitude - ListStations[i].Lattitude;
                        double y = ListStations[j].Longitude - ListStations[i].Longitude;
                        x = Math.Pow(x, 2);
                        y = Math.Pow(y, 2);

                        DO.AdjacentStations a = new AdjacentStations
                        {
                            Station1 = i,
                            Station2 = j,
                            Is_Active = true,
                            Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59)),
                            Distance = Math.Sqrt(x + y)
                        };
                        ListAdjacentStations.Add(a);
                    };
                       
                    
                }
            }

            /*
            for (int i = 0; i < ListStations.Count; i++)
            {
                for (int j = 0; j < ListStations.Count; j++)
                {
                    if (i == j)
                    {
                        ;
                    }
                    else
                    {
                        DO.AdjacentStations a = new AdjacentStations();
                        a.Station1 = ListStations[i].Code;
                        a.Station2 = ListStations[j].Code;
                        double x = ListStations[j].Lattitude - ListStations[i].Lattitude;
                        double y = ListStations[j].Longitude - ListStations[i].Longitude;
                        x = Math.Pow(x, 2);
                        y = Math.Pow(y, 2);
                        a.Distance = Math.Sqrt(x + y);
                        a.Time = new TimeSpan(0, r.Next(0, 15), r.Next(0, 59));
                        a.Is_Active = true;
                        ListAdjacentStations.Add(a);
                    }
                }
            }
            
            */
            #endregion

            #region  listLineStation
            ListLineStations = new List<LineStation>
            {
            new LineStation
            {
            LineId= 0,
            Station = 2,
            PrevStation= -1,
            NextStation= 4,
            LineStationIndex=1,
            Is_Active=true

            },
            new LineStation
            {
            LineId= 0,
            Station = 4,
            PrevStation= 2,
            NextStation= 10,
            LineStationIndex=1,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 10,
            PrevStation= 4,
            NextStation= 32,
            LineStationIndex=2,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 32,
            PrevStation= 10,
            NextStation= 40,
            LineStationIndex=3,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 40,
            PrevStation= 32,
            NextStation= 22,
            LineStationIndex=4,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 22,
            PrevStation= 40,
            NextStation= 41,
            LineStationIndex=5,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 41,
            PrevStation= 22,
            NextStation= 42,
            LineStationIndex=6,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 42,
            PrevStation= 41,
            NextStation= 43,
            LineStationIndex=7,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 43,
            PrevStation= 42,
            NextStation= 5,
            LineStationIndex=8,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 0,
            Station = 5,
            PrevStation= 43,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 1,
            Station = 0,
            PrevStation= -1,
            NextStation= 1,
            LineStationIndex=0,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 1,
            Station = 1,
            PrevStation= 0,
            NextStation= 2,
            LineStationIndex=1,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 1,
            Station = 2,
            PrevStation= 1,
            NextStation= 3,
            LineStationIndex=2,
            Is_Active=true
            },
               new LineStation
            {
            LineId= 1,
            Station = 3,
            PrevStation= 2,
            NextStation= 4,
            LineStationIndex=3,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 1,
            Station = 4,
            PrevStation= 3,
            NextStation= 5,
            LineStationIndex=4,
            Is_Active=true
            },
 new LineStation
            {
            LineId= 1,
            Station = 5,
            PrevStation= 4,
            NextStation= 6,
            LineStationIndex=5,
            Is_Active=true
            },
  new LineStation
            {
            LineId= 1,
            Station = 6,
            PrevStation= 5,
            NextStation= 7,
            LineStationIndex=6,
            Is_Active=true
            },
   new LineStation
            {
            LineId= 1,
            Station = 7,
            PrevStation= 6,
            NextStation=8,
            LineStationIndex=7,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 1,
            Station = 8,
            PrevStation= 7,
            NextStation= 9,
            LineStationIndex=8,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 1,
            Station = 9,
            PrevStation= 8,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 2,
            Station = 10,
            PrevStation= -1,
            NextStation= 11,
            LineStationIndex=0,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 2,
            Station = 11,
            PrevStation= 10,
            NextStation= 12,
            LineStationIndex=1,
            Is_Active=true
            },
               new LineStation
            {
            LineId= 2,
            Station = 12,
            PrevStation= 11,
            NextStation= 13,
            LineStationIndex=2,
            Is_Active=true
            },
                new LineStation
            {
            LineId= 2,
            Station = 13,
            PrevStation= 12,
            NextStation= 14,
            LineStationIndex=3,
            Is_Active=true
            },
                 new LineStation
            {
            LineId= 2,
            Station = 14,
            PrevStation= 13,
            NextStation= 15,
            LineStationIndex=4,
            Is_Active=true
            },
                  new LineStation
            {
            LineId= 2,
            Station = 15,
            PrevStation= 14,
            NextStation= 16,
            LineStationIndex=5,
            Is_Active=true
            },
                   new LineStation
            {
            LineId= 2,
            Station = 16,
            PrevStation= 15,
            NextStation= 17,
            LineStationIndex=6,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 2,
            Station = 17,
            PrevStation= 16,
            NextStation= 18,
            LineStationIndex=7,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 2,
            Station = 18,
            PrevStation= 17,
            NextStation= 19,
            LineStationIndex=8,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 2,
            Station = 19,
            PrevStation= 18,
            NextStation= 20,
            LineStationIndex=9,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 3,
            Station = 20,
            PrevStation= -1,
            NextStation= 21,
            LineStationIndex=0,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 3,
            Station = 21,
            PrevStation= 20,
            NextStation= 22,
            LineStationIndex=1,
            Is_Active=true
            },
               new LineStation
            {
            LineId= 3,
            Station = 22,
            PrevStation= 21,
            NextStation= 23,
            LineStationIndex=2,
            Is_Active=true
            },
                new LineStation
            {
            LineId= 3,
            Station = 23,
            PrevStation= 22,
            NextStation= 24,
            LineStationIndex=3,
            Is_Active=true
            },
                 new LineStation
            {
            LineId= 3,
            Station = 24,
            PrevStation= 23,
            NextStation= 25,
            LineStationIndex=4,
            Is_Active=true
            },
                  new LineStation
            {
            LineId= 3,
            Station = 25,
            PrevStation= 24,
            NextStation= 26,
            LineStationIndex=5,
            Is_Active=true
            },
                   new LineStation
            {
            LineId= 3,
            Station = 26,
            PrevStation= 25,
            NextStation= 27,
            LineStationIndex=6,
            Is_Active=true
            },
                    new LineStation
            {
            LineId= 3,
            Station = 27,
            PrevStation= 26,
            NextStation= 28,
            LineStationIndex=7,
            Is_Active=true
            },
                     new LineStation
            {
            LineId= 3,
            Station = 28,
            PrevStation= 27,
            NextStation= 29,
            LineStationIndex=8,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 3,
            Station = 29,
            PrevStation= 28,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 4,
            Station = 30,
            PrevStation= -1,
            NextStation= 31,
            LineStationIndex=0,
            Is_Active=true
            },
               new LineStation
            {
            LineId= 4,
            Station = 31,
            PrevStation= 30,
            NextStation= 32,
            LineStationIndex=1,
            Is_Active=true
            },
                new LineStation
            {
            LineId= 4,
            Station = 32,
            PrevStation= 31,
            NextStation= 33,
            LineStationIndex=2,
            Is_Active=true
            },
                 new LineStation
            {
            LineId= 4,
            Station = 33,
            PrevStation= 32,
            NextStation= 34,
            LineStationIndex=3,
            Is_Active=true
            },
                  new LineStation
            {
            LineId= 4,
            Station = 34,
            PrevStation= 33,
            NextStation= 35,
            LineStationIndex=4,
            Is_Active=true
            },
                   new LineStation
            {
            LineId= 4,
            Station =35,
            PrevStation= 34,
            NextStation= 36,
            LineStationIndex=5,
            Is_Active=true
            },
                    new LineStation
            {
            LineId= 4,
            Station = 36,
            PrevStation= 35,
            NextStation= 37,
            LineStationIndex=6,
            Is_Active=true
            },
                     new LineStation
            {
            LineId= 4,
            Station = 37,
            PrevStation= 36,
            NextStation= 38,
            LineStationIndex=7,
            Is_Active=true
            },
                      new LineStation
            {
            LineId= 4,
            Station = 38,
            PrevStation= 37,
            NextStation= 39,
            LineStationIndex=8,
            Is_Active=true
            },
                       new LineStation
            {
            LineId= 4,
            Station = 39,
            PrevStation= 38,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
                        new LineStation
            {
            LineId= 5,
            Station = 40,
            PrevStation= -1,
            NextStation= 41,
            LineStationIndex=0,
            Is_Active=true
            },
                         new LineStation
            {
            LineId= 5,
            Station =41,
            PrevStation= 40,
            NextStation= 42,
            LineStationIndex=1,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 5,
            Station =42,
            PrevStation= 41,
            NextStation= 43,
            LineStationIndex=2,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 5,
            Station = 43,
            PrevStation= 42,
            NextStation= 44,
            LineStationIndex=3,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 5,
            Station = 44,
            PrevStation= 43,
            NextStation= 45,
            LineStationIndex=4,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 5,
            Station = 45,
            PrevStation= 44,
            NextStation= 46,
            LineStationIndex=5,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 5,
            Station = 46,
            PrevStation= 45,
            NextStation= 47,
            LineStationIndex=6,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 5,
            Station = 47,
            PrevStation= 46,
            NextStation= 48,
            LineStationIndex=7,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 5,
            Station =48,
            PrevStation= 47,
            NextStation= 49,
            LineStationIndex=8,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 5,
            Station = 49,
            PrevStation= 48,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 6,
            Station = 5,
            PrevStation= -1,
            NextStation= 6,
            LineStationIndex=0,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 6,
            Station = 6,
            PrevStation= 5,
            NextStation= 7,
            LineStationIndex=1,
            Is_Active=true
            },
               new LineStation
            {
            LineId= 6,
            Station = 7,
            PrevStation= 6,
            NextStation= 8,
            LineStationIndex=2,
            Is_Active=true
            },
                new LineStation
            {
            LineId= 6,
            Station = 8,
            PrevStation= 7,
            NextStation= 9,
            LineStationIndex=3,
            Is_Active=true
            },
                 new LineStation
            {
            LineId= 6,
            Station = 9,
            PrevStation= 8,
            NextStation= 10,
            LineStationIndex=4,
            Is_Active=true
            },
                  new LineStation
            {
            LineId= 6,
            Station = 10,
            PrevStation= 9,
            NextStation= 11,
            LineStationIndex=5,
            Is_Active=true
            },
                   new LineStation
            {
            LineId= 6,
            Station = 11,
            PrevStation= 10,
            NextStation= 12,
            LineStationIndex=6,
            Is_Active=true
            },
                    new LineStation
            {
            LineId= 6,
            Station = 12,
            PrevStation= 11,
            NextStation= 13,
            LineStationIndex=7,
            Is_Active=true
            },
                     new LineStation
            {
            LineId= 6,
            Station = 13,
            PrevStation= 12,
            NextStation= 14,
            LineStationIndex=8,
            Is_Active=true
            },
                      new LineStation
            {
            LineId= 6,
            Station = 14,
            PrevStation= 13,
            NextStation= 15,
            LineStationIndex=9,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 15,
            PrevStation= -1,
            NextStation=16,
            LineStationIndex=0,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 16,
            PrevStation= 15,
            NextStation= 17,
            LineStationIndex=1,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 7,
            Station = 17,
            PrevStation= 16,
            NextStation= 18,
            LineStationIndex=2,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 18,
            PrevStation= 17,
            NextStation= 19,
            LineStationIndex=3,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 19,
            PrevStation= 18,
            NextStation= 20,
            LineStationIndex=4,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 20,
            PrevStation= 19,
            NextStation= 21,
            LineStationIndex=5,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 21,
            PrevStation= 20,
            NextStation= 22,
            LineStationIndex=6,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 22,
            PrevStation= 21,
            NextStation= 23,
            LineStationIndex=7,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 23,
            PrevStation= 22,
            NextStation= 24,
            LineStationIndex=8,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 7,
            Station = 24,
            PrevStation= 23,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 8,
            Station = 25,
            PrevStation= -1,
            NextStation= 26,
            LineStationIndex=0,
            Is_Active=true
            } ,
                new LineStation
            {
            LineId= 8,
            Station = 26,
            PrevStation= 25,
            NextStation= 27,
            LineStationIndex=1,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 8,
            Station = 27,
            PrevStation= 26,
            NextStation= 28,
            LineStationIndex=2,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 8,
            Station = 28,
            PrevStation= 27,
            NextStation= 29,
            LineStationIndex=3,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 8,
            Station = 29,
            PrevStation= 28,
            NextStation= 30,
            LineStationIndex=4,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 8,
            Station = 30,
            PrevStation= 29,
            NextStation= 31,
            LineStationIndex=5,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 8,
            Station = 31,
            PrevStation= 30,
            NextStation= 32,
            LineStationIndex=6,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 8,
            Station =32,
            PrevStation= 31,
            NextStation= 33,
            LineStationIndex=7,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 8,
            Station = 33,
            PrevStation= 32,
            NextStation= 34,
            LineStationIndex=8,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 8,
            Station = 34,
            PrevStation= 33,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 9,
            Station = 36,
            PrevStation= -1,
            NextStation= 37,
            LineStationIndex=0,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 9,
            Station = 37,
            PrevStation= 36,
            NextStation= 38,
            LineStationIndex=1,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 9,
            Station = 38,
            PrevStation= 37,
            NextStation= 39,
            LineStationIndex=2,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 9,
            Station = 39,
            PrevStation= 38,
            NextStation= 40,
            LineStationIndex=3,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 9,
            Station = 40,
            PrevStation= 39,
            NextStation= 41,
            LineStationIndex=4,
            Is_Active=true
            },
              new LineStation
            {
            LineId= 9,
            Station = 41,
            PrevStation= 40,
            NextStation= 42,
            LineStationIndex=5,
            Is_Active=true
            },
               new LineStation
            {
            LineId= 9,
            Station = 42,
            PrevStation= 41,
            NextStation= 43,
            LineStationIndex=6,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 9,
            Station = 43,
            PrevStation= 42,
            NextStation= 44,
            LineStationIndex=7,
            Is_Active=true
            },
            new LineStation
            {
            LineId= 9,
            Station = 44,
            PrevStation= 43,
            NextStation= 45,
            LineStationIndex=8,
            Is_Active=true
            },
             new LineStation
            {
            LineId= 9,
            Station = 45,
            PrevStation= 44,
            NextStation= -1,
            LineStationIndex=9,
            Is_Active=true
            }
            };
            /*int index_linestation = 0;
            int k = 1;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    ListLineStations[index_linestation].LineId = ListLines[i].Id;
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
                }*/
            #endregion

            #region list line trip
            listLineTrip = new List<LineTrip>
            {
                new LineTrip
                {
                    LineId=ListLines[0].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,15,0),
                    StartAt=new TimeSpan(7,0,0),
                    //FinishAt=new TimeSpan(12,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[0].Id,
                    Id=2,
                    //Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    //FinishAt=new TimeSpan(16,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[0].Id,
                    Id=3,
                    //Frequency=new TimeSpan(0,20,0),
                    StartAt=new TimeSpan(16,0,0),
                    //FinishAt=new TimeSpan(23,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[1].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                   // FinishAt=new TimeSpan(16,0,0)
                   Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[2].Id,
                    Id=1,
                    //Frequency=new TimeSpan(2,0,0),
                    StartAt=new TimeSpan(8,0,0),
                    //FinishAt=new TimeSpan(20,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[3].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,10,0),
                    StartAt=new TimeSpan(7,0,0),
                    //FinishAt=new TimeSpan(18,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[3].Id,
                    Id=2,
                    //Frequency=new TimeSpan(0,20,0),
                    StartAt=new TimeSpan(18,0,0),
                    //FinishAt=new TimeSpan(0,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[4].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,0,0),
                    StartAt=new TimeSpan(12,0,0),
                    //FinishAt=new TimeSpan(0,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[5].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    //FinishAt=new TimeSpan(19,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[6].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,5,0),
                    StartAt=new TimeSpan(5,0,0),
                    //FinishAt=new TimeSpan(13,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[7].Id,
                    Id=1,
                   // Frequency=new TimeSpan(1,0,0),
                    StartAt=new TimeSpan(4,0,0),
                   // FinishAt=new TimeSpan(7,0,0)
                   Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[7].Id,
                    Id=2,
                   // Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(7,0,0),
                   // FinishAt=new TimeSpan(16,0,0)
                   Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[7].Id,
                    Id=3,
                    //Frequency=new TimeSpan(0,15,0),
                    StartAt=new TimeSpan(16,0,0),
                   // FinishAt=new TimeSpan(17,0,0)
                   Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[8].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,30,0),
                    StartAt=new TimeSpan(12,0,0),
                    //FinishAt=new TimeSpan(16,0,0)
                    Is_Active=true
                },
                new LineTrip
                {
                    LineId=ListLines[9].Id,
                    Id=1,
                    //Frequency=new TimeSpan(0,20,0),
                    StartAt=new TimeSpan(23,0,0),
                    //FinishAt=new TimeSpan(4,0,0)
                    Is_Active=true
                }
            };
            #endregion
        }

    }
    
}
