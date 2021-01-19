using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    {
        public AddStationWindow()
        {
            InitializeComponent();
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void b_save_Click(object sender, RoutedEventArgs e)
        {
            PO.Station station = new PO.Station();
            station.Code = int.Parse(tb_code.Text);
            station.Name = tb_name.Text;
            try
            {
                station.Lattitude = double.Parse(tb_lattitude.Text);
                station.Longitude = double.Parse(tb_longitude.Text);
                MainWindow.bl.AddStation(PO.SwitchObjects.StationPoToBo(station));
               
            }
            catch (BO.Exceptions.Add_Existing_Item_Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show("please enter to longitude and lattitude numbers");
            }

            StationInfoWindow.stations.Add(station);
            Close();
        }
    }
}
