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
    /// Interaction logic for MoreStationInfoWindow.xaml
    /// </summary>
    public partial class MoreStationInfoWindow : Window
    {
        public static ObservableCollection<PO.Line> OClines { get; set; }
        public PO.Station station { get; set; }
        public MoreStationInfoWindow(PO.Station s)
        {
            OClines = new ObservableCollection<PO.Line>();
            InitializeComponent();
            station = s;
            try
            {
                BO.Station stationsBO = PO.SwitchObjects.StationPoToBo(s);
                IEnumerable<BO.Line> LINESBO = MainWindow.bl.FindAllLinesOfThisStation(stationsBO);
                IEnumerable<PO.Line> Lpo = from item in LINESBO
                                           select new PO.Line { Area = item.Area, Code = item.Code, FirstStation = item.FirstStation, Id = item.Id, LastStation = item.LastStation };



                foreach (var item in Lpo)
                {
                    OClines.Add(item);
                }
                lb_lineOfStation.ItemsSource = OClines;
                l_station_code.Content = s.Code;
                l_station_name.Content = s.Name;
            }
            catch (BO.Exceptions.Item_not_found_Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}
