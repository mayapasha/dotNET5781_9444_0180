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
        public AddLineWindow()
        {
            try
            {
                stations =(ObservableCollection<PO.Station>) MainWindow.bl.Get_All_Stations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
            InitializeComponent();
            cb_first_station.ItemsSource = stations;
            cb_last_station.ItemsSource = stations;
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
            b.Area = (BO.Enums.Areas)cb_line_area.SelectedItem;
            PO.Station s = cb_first_station.SelectedItem as PO.Station;
            b.FirstStation = s.Code;
            s = cb_last_station.SelectedItem as PO.Station;
            b.LastStation = s.Code;
            MainWindow.bl.Add_Line(PO.SwitchObjects.LinePoToBo(b));
            Close();
        }
    }
}
