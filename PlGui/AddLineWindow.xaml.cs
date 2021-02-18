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
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        public ObservableCollection<PO.Station> stations  { get; set; }
        ObservableCollection<string> areaStrings { get; set; }
        public AddLineWindow()
        {
            try
            {
                stations = new ObservableCollection<PO.Station>();
                IEnumerable<PO.Station> s = from item in MainWindow.bl.Get_All_Stations()
                                            select new PO.Station { Code = item.Code, Name = item.Name, Longitude = item.Longitude, Lattitude = item.Lattitude } /*PO.SwitchObjects.StationBoToPo(item)*/;

                foreach (var item in s)
                {
                    stations.Add(item);
                }
               areaStrings = new ObservableCollection<string> {BO.Enums.Areas.Center.ToString(), BO.Enums.Areas.General.ToString(), BO.Enums.Areas.Jerusalem.ToString(), BO.Enums.Areas.North.ToString(), BO.Enums.Areas.South.ToString()};
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            InitializeComponent();
            cb_first_station.ItemsSource = stations;
            cb_last_station.ItemsSource = stations;
            cb_line_area.ItemsSource = areaStrings;
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void b_save_line_Click(object sender, RoutedEventArgs e)
        {
           if(cb_first_station.SelectedItem as PO.Station==cb_last_station.SelectedItem as PO.Station)
            {
                MessageBox.Show("the first and the last stations are the same");
                return;
            }
            PO.Line b = new PO.Line();
            b.Code = int.Parse(t_line_code.Text);
            if(cb_line_area.SelectedItem.ToString() == "Center")
            b.Area = BO.Enums.Areas.Center;
            if (cb_line_area.SelectedItem.ToString() == "General")
                b.Area = BO.Enums.Areas.General;
            if (cb_line_area.SelectedItem.ToString() == "Jerusalem")
                b.Area = BO.Enums.Areas.Jerusalem;
            if (cb_line_area.SelectedItem.ToString() == "North")
                b.Area = BO.Enums.Areas.North;
            if (cb_line_area.SelectedItem.ToString() == "South")
                b.Area = BO.Enums.Areas.South;
            PO.Station s = cb_first_station.SelectedItem as PO.Station;
            b.FirstStation = s.Code;
            s = cb_last_station.SelectedItem as PO.Station;
            b.LastStation = s.Code;
            try
            {
                BO.Line LBO = new BO.Line { Area = b.Area, Code = b.Code, FirstStation = b.FirstStation, LastStation = b.LastStation }; 
                MainWindow.bl.Add_Line(LBO);
               
            }
            catch (BO.Exceptions.Add_Existing_Item_Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
