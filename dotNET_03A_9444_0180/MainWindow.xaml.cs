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
using dotNet_02_9444_0180;
namespace dotNET_03A_9444_0180
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusLines b = new BusLines();
        private BusLine currentDisplayBusLine;
        public MainWindow()
        {
            b.lines = new List<BusLine>();
            int counter = 0;
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                BusLine other = new BusLine();
                other.Stations = new List<BusLineStation>();
                other.BusLineNumber = i;
                other.Area = (Places)r.Next(0, 4);

                other.Stations = new List<BusLineStation>();
                for (int k = 0; k < 5; k++)
                {
                    BusLineStation s = new BusLineStation();
                    s.BusStationKey = r.Next(1, 99999);
                    other.Stations.Add(s);
                }
                b.lines.Add(other);
                counter = 0;
                for (int j = 0; j < i; j++)
                {
                    if (b.lines[i] == b.lines[j])
                    {
                        counter++;
                    }
                    if (counter == 1)
                    {
                        if (b.lines[j].FirstStation != b.lines[i].LastStation)
                        {
                            while (b.lines[i] == b.lines[j])
                            {
                                b.lines[i].BusLineNumber = r.Next(1, 100);
                            }
                            counter = 0;
                        }

                    }
                }


            }
            InitializeComponent();
            //cbBusLines = new ComboBox();
            cbBusLines.ItemsSource = b;
            cbBusLines.DisplayMemberPath = " BusLineNumer ";
            cbBusLines.SelectedIndex = 0;

        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = b[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusLineNumber);

        }
    }
}
