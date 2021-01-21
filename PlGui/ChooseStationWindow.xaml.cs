using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for ChooseStationWindow.xaml
    /// </summary>
    public partial class ChooseStationWindow : Window
    {
        public PO.Line line;
        public PO.LineStation lineStation;
        public static ObservableCollection<PO.Station> stations { get; set; }
        public ChooseStationWindow(PO.Line l, PO.LineStation S)
        {
            stations = new ObservableCollection<PO.Station>();
            try
            {
                stations = new ObservableCollection<PO.Station>();
                IEnumerable<PO.Station> s = from item in MainWindow.bl.Get_All_Stations()
                                            select new PO.Station { Code = item.Code, Name = item.Name, Longitude = item.Longitude, Lattitude = item.Lattitude };
                foreach (var item in s)
                {
                    stations.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeComponent();
            lb_stations.ItemsSource = stations;
            line = l;
            lineStation = S;
        }

        private void lb_stations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Station b = lb_stations.SelectedItem as PO.Station;
            try
            {
                BO.Station stationBO = new BO.Station { Code = b.Code, Lattitude = b.Lattitude, Longitude = b.Longitude, Name = b.Name };
                BO.Line lineBO = new BO.Line { Area = line.Area, Code = line.Code, FirstStation = line.FirstStation, Id = line.Id, LastStation = line.LastStation };
                IEnumerable<PO.LineStation> ls = MainWindow.bl.Get_All_LineStations().ToList().Where(item1 => item1.LineId == line.Id).Select(item1 => new PO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance, Name = item1.Name });
                IEnumerable<BO.LineStation> lsBO = ls.ToList().Select(item1 => new BO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance, Name = item1.Name });
                lineBO.List_Of_LineStation = lsBO;
                BO.LineStation lineStationBO = new BO.LineStation { Distance = lineStation.Distance, LineId = lineStation.LineId, LineStationIndex = lineStation.LineStationIndex, Name = lineStation.Name, NextStation = lineStation.NextStation, PrevStation = lineStation.PrevStation, Station = lineStation.Station, Time = lineStation.Time };
                MainWindow.bl.AddStationToLine(stationBO,lineBO, lineStationBO);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
