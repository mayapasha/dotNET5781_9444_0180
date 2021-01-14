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
            InitializeComponent();
            try
            {
                IEnumerable<PO.Station> s = from item in MainWindow.bl.Get_All_Stations()
                                            select PO.SwitchObjects.StationBoToPo(item);
                stations = (ObservableCollection<PO.Station>)s;
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
        }
    }
}
