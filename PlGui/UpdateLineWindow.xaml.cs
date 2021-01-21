using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        public ObservableCollection<string> areaStrings { get; set; }
        PO.Line line;
        public UpdateLineWindow(PO.Line l)
        {
            areaStrings = new ObservableCollection<string> { BO.Enums.Areas.Center.ToString(), BO.Enums.Areas.General.ToString(), BO.Enums.Areas.Jerusalem.ToString(), BO.Enums.Areas.North.ToString(), BO.Enums.Areas.South.ToString() };
            InitializeComponent();

            line = l;
            mainGrid.DataContext = line;
            cb_area.ItemsSource = areaStrings;
            lb_line.ItemsSource = line.List_Of_Line_Stations;
            cb_area.SelectedItem = line.Area.ToString();
        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (cb_area.SelectedItem.ToString() == "Center")
                line.Area = BO.Enums.Areas.Center;
            if (cb_area.SelectedItem.ToString() == "General")
                line.Area = BO.Enums.Areas.General;
            if (cb_area.SelectedItem.ToString() == "Jerusalem")
                line.Area = BO.Enums.Areas.Jerusalem;
            if (cb_area.SelectedItem.ToString() == "North")
                line.Area = BO.Enums.Areas.North;
            if (cb_area.SelectedItem.ToString() == "South")
                line.Area = BO.Enums.Areas.South;
            BO.Line l = new BO.Line { Area = line.Area, Code = line.Code, FirstStation = line.FirstStation, Id = line.Id, LastStation = line.LastStation };
            IEnumerable<PO.LineStation> ls = MainWindow.bl.Get_All_LineStations().ToList().Where(item1 => item1.LineId == line.Id).Select(item1 => new PO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance, Name = item1.Name });
            IEnumerable<BO.LineStation> lsBO = ls.ToList().Select(item1 => new BO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance, Name = item1.Name });
            l.List_Of_LineStation = lsBO.ToList();
            MainWindow.bl.update_Line(l);
            Close();
        } 
        private void b_add_station_Click(object sender, RoutedEventArgs e)
        {
            PO.LineStation lineStation = (sender as Button).DataContext as PO.LineStation;
            ChooseStationWindow chooseStationWindow = new ChooseStationWindow(line, lineStation);
            chooseStationWindow.Show();
        }
        private void b_dis_time_Click(object sender, RoutedEventArgs e)
        {
            PO.LineStation lsPO = (sender as Button).DataContext as PO.LineStation;
            DisTimeWindow disTimeWindow = new DisTimeWindow(lsPO);
            disTimeWindow.Show();
        }
    }
}
