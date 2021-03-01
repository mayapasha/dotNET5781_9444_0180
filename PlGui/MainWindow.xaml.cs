using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBL bl = BLFactory.GetBL();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void b_line_Click(object sender, RoutedEventArgs e)
        {
            LineInfoWindow lineInfoWindow = new LineInfoWindow();
            lineInfoWindow.Show();
        }

        private void b_station_Click(object sender, RoutedEventArgs e)
        {
            StationInfoWindow stationInfoWindow = new StationInfoWindow();
            stationInfoWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LineTripInfoWindow lineTripInfoWindow = new LineTripInfoWindow();
            lineTripInfoWindow.Show();
        }
        private void BSimulator_Click(object sender, RoutedEventArgs e)
        {
            StationToSimulatorWindow stationToSimulatorWindow = new StationToSimulatorWindow();
            stationToSimulatorWindow.Show();
        }
    }
}
