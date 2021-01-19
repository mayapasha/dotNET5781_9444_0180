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
        //private PO.Line currentDisplayLine;
        public static ObservableCollection<PO.Line> lines { get; set; }
        public LineInfoWindow()
        {
            lines = new ObservableCollection<PO.Line>();
            InitializeComponent();
            try
            {
                /*  IEnumerable<BO.Line> lBO = from item in MainWindow.bl.Get_All_Lines().ToList()
                                             select new BO.Line {Id=item.Id, List_Of_LineStation = item.List_Of_LineStation };
                  IEnumerable<PO.LineStation> lsPO = from item in lBO.ToList()
                                                     from item1 in item.List_Of_LineStation
                                                     select new PO.LineStation { };*/
                IEnumerable<PO.LineStation> lineStationsPO = MainWindow.bl.Get_All_LineStations().ToList().Select(item1 => new PO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance });
                IEnumerable<PO.Line> l = from item in MainWindow.bl.Get_All_Lines().ToList()
                                         select new PO.Line{ Area=item.Area, Code=item.Code, FirstStation=item.FirstStation, Id=item.Id, LastStation=item.LastStation };

                foreach (var item in l)
                {
                    IEnumerable<PO.LineStation> ls = lineStationsPO.ToList().Where(item1 => item1.LineId == item.Id).Select(item1=>new PO.LineStation { LineId = item1.LineId, Time = item1.Time, Station = item1.Station, PrevStation = item1.PrevStation, NextStation = item1.NextStation, LineStationIndex = item1.LineStationIndex, Distance = item1.Distance });
                                                     
                    
                    foreach (var item1 in ls)
                    {
                        item.List_Of_Line_Stations.Add(item1);
                    }
                    lines.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cbLines.ItemsSource = lines;
            cbLines.DisplayMemberPath = "Code";
           
           // l_Area.DataContext = (BO.Enums.Areas) PO.Line.Area;
           
            cbLines.SelectedIndex = 0;
            lbLineStations.DataContext = lines[0].List_Of_Line_Stations;
            UpGrid.DataContext = lines[0];
            //MainGrid.DataContext = lines[0].
        }
        private void ShowLine(int index)
        {
           // PO.Line l = lines.ToList().Find(item => item.Id == index);
          //  UpGrid.DataContext = currentDisplayLine;
          int I= lines.ToList().FindIndex(item => item.Id == index);
            lbLineStations.DataContext = lines[I].List_Of_Line_Stations;
            UpGrid.DataContext = lines[I];


        }
        private void cbLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowLine((cbLines.SelectedValue as PO.Line).Id);

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


