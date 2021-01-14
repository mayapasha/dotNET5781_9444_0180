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
        PO.Line l = new PO.Line();
        private PO.Line currentDisplayLine;
        public static ObservableCollection<PO.Line> lines { get; set; }
        public LineInfoWindow()
        {
            lines = new ObservableCollection<PO.Line>();
            InitializeComponent();
            try
            {
                IEnumerable<PO.Line> l = from item in MainWindow.bl.Get_All_Lines()
                                         select PO.SwitchObjects.LineBoToPo(item);
                lines = (ObservableCollection<PO.Line>)l;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            cbLines.ItemsSource = lines;
            cbLines.DisplayMemberPath = " PO.Line.Code ";
            cbLines.SelectedIndex = 0;
        }
        private void ShowLine(int index)
        {
            currentDisplayLine = lines[index];
            UpGrid.DataContext = currentDisplayLine;
            lbLineStations.DataContext = currentDisplayLine.List_Of_Line_Stations;
        }
        private void cbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowLine((cbLines.SelectedValue as PO.Line).Code);

        }

        private void lbLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void b_update_line_Click(object sender, RoutedEventArgs e)
        {
            PO.Line line = (PO.Line)cbLines.SelectedItem;
            UpdateLineWindow updateLineWindow = new UpdateLineWindow(line);       
            updateLineWindow.ShowDialog();
        }
    }
}


