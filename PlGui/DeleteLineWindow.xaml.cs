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
    /// Interaction logic for DeleteLineWindow.xaml
    /// </summary>
    public partial class DeleteLineWindow : Window
    {
        public ObservableCollection<PO.Line> lines { get; set; }
        public DeleteLineWindow(ObservableCollection<PO.Line> l)
        {

            InitializeComponent();
            lines = l;
            cb_lineDelete.ItemsSource = lines;

        }
       /* public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }*/
        private void b_delete_line_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PO.Line l = cb_lineDelete.SelectedItem as PO.Line;
                BO.Line lbo = new BO.Line { Area = l.Area, Code = l.Code, FirstStation = l.FirstStation, Id = l.Id, LastStation = l.LastStation };
                IEnumerable<BO.LineStation> ls = from item1 in l.List_Of_Line_Stations.ToList()
                                                 select new BO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance, Name = item1.Name };
                // IEnumerable<BO.>
                lbo.List_Of_LineStation = ls.ToList();
                //BO.Line l_delete= MainWindow.bl.Get_Line(lbo.Id);
                MainWindow.bl.Delete_Line(lbo);
                LineInfoWindow.lines.Remove(l);
                Close();
            }
            catch(BO.Exceptions.Add_Existing_Item_Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
}
