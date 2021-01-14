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
    /// Interaction logic for ChooseStationWindow.xaml
    /// </summary>
    public partial class ChooseStationWindow : Window
    {
        public PO.Line line;
        public PO.LineStation lineStation;

        public ChooseStationWindow(PO.Line l, PO.LineStation S)
        {
            InitializeComponent();
            lb_stations.ItemsSource = MainWindow.bl.Get_All_Stations().ToList();///////
            line = l;
            lineStation = S;

        }

        private void lb_stations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PO.Station b = sender as PO.Station;
            try
            {
                MainWindow.bl.AddStationToLine(PO.SwitchObjects.StationPoToBo(b),PO.SwitchObjects.LinePoToBo( line),PO.SwitchObjects.LineStationPoToBo( lineStation));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
