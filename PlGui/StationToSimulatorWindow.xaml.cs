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
using System.Windows.Shapes;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationToSimulatorWindow.xaml
    /// </summary>
    public partial class StationToSimulatorWindow : Window
    {
        public StationToSimulatorWindow()
        {
            InitializeComponent();
            cbStations.ItemsSource = MainWindow.bl.Get_All_Stations().Select(item => new PO.Station { Code = item.Code, Lattitude = item.Lattitude, Longitude = item.Longitude, Name = item.Name }).ToList();

        }

        private void cbStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PO.Station st = cbStations.SelectedItem as PO.Station;
            if (st != null)
            {
                simulator simulator = new simulator(st);
                simulator.Show();
            }
            else MessageBox.Show("ERROR");


        }
    }
}
