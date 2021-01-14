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
    /// Interaction logic for UpdateLineWindow.xaml
    /// </summary>
    public partial class UpdateLineWindow : Window
    {
        PO.Line line;
        public UpdateLineWindow(PO.Line l)
        {
            InitializeComponent();

            line = l;
        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            line.Area = (BO.Enums.Areas)cb_area.SelectedItem;
            line.Code = int.Parse(tb_code.Text);
            MainWindow.bl.update_Line(PO.SwitchObjects.LinePoToBo(line));
            Close();
        } 
        private void b_add_station_Click(object sender, RoutedEventArgs e)
        {
            PO.LineStation s = (sender as Button).DataContext as PO.LineStation;
            ChooseStationWindow chooseStationWindow = new ChooseStationWindow(line,s);
            chooseStationWindow.Show();
        }
        private void b_dis_time_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
