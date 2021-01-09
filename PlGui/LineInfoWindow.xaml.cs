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
using BLAPI;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for LineInfoWindow.xaml
    /// </summary>
    public partial class LineInfoWindow : Window
    {
        BO.Line b = new BO.Line();
        private BO.Line currentDisplayBusLine;
        public static ObservableCollection<BO.Line> lines { get; set; }
        public LineInfoWindow()
        {
            lines = new ObservableCollection<BO.Line>();
            InitializeComponent();
            try
            {
                lines = (ObservableCollection<BO.Line>)MainWindow.bl.Get_All_Lines();
                
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR");
                throw;
            }
            cbBusLines.ItemsSource = lines;
            cbBusLines.DisplayMemberPath = " BO.Line.id ";
            cbBusLines.SelectedIndex = 0;
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = lines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.List_Of_LineStation;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BO.Line).Code);

        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {



        }

        private void b_add_line_Click(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow();
            addLineWindow.ShowDialog();
        }

        private void b_delete_line_Click(object sender, RoutedEventArgs e)
        {
            DeleteLineWindow deleteLineWindow = new DeleteLineWindow();
            deleteLineWindow.Show();
        }
    }
}


