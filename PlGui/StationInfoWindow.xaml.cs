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
    /// Interaction logic for StationInfoWindow.xaml
    /// </summary>
    public partial class StationInfoWindow : Window
    {
        public static ObservableCollection<PO.Station> stations { get; set; }
        public StationInfoWindow()
        {
            
            stations = new ObservableCollection<PO.Station>();
            InitializeComponent();
            try
            {
                stations = new ObservableCollection<PO.Station>();
                IEnumerable<PO.Station> s = from item in MainWindow.bl.Get_All_Stations()
                                            select new PO.Station { Code = item.Code, Name = item.Name, Longitude = item.Longitude, Lattitude = item.Lattitude } /*PO.SwitchObjects.StationBoToPo(item)*/;
 
                foreach (var item in s)
                {
                    stations.Add(item);
                }
                lb_stations.DataContext = stations;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void b_add_station_Click(object sender, RoutedEventArgs e)
        {
            AddStationWindow addStationWindow = new AddStationWindow();
            addStationWindow.Show();
            // update_OC_Stations();
          
            //lb_stations.DataContext = stations;
            

        }

        private void lb_stations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Station  stationPO= lb_stations.SelectedItem as PO.Station;
            MoreStationInfoWindow moreStationInfoWindow = new MoreStationInfoWindow(stationPO);
            moreStationInfoWindow.Show();
        }
    }
}
