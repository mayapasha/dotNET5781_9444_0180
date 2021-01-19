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
    /// Interaction logic for LineTripInfoWindow.xaml
    /// </summary>
    public partial class LineTripInfoWindow : Window
    {
        //private Line currentDisplayLineTrip;
        public static ObservableCollection<PO.LineTrip> lineTrips { get; set; }
        public IEnumerable<PO.Line> l { get; set; }
        public LineTripInfoWindow()
        {
            InitializeComponent();
            IEnumerable<PO.LineTrip> ltpo = MainWindow.bl.Get_All_LineTrips().ToList().Select(item => new PO.LineTrip { Id = item.Id, LineId = item.LineId, StartAt = item.StartAt }).ToList();
            l = from item in MainWindow.bl.Get_All_Lines().ToList()
                                     select new PO.Line { Area = item.Area, Code = item.Code, FirstStation = item.FirstStation, Id = item.Id, LastStation = item.LastStation };

           cb_lines.ItemsSource = l;
           cb_lines.DisplayMemberPath = " Code ";
           cb_lines.SelectedIndex = 0;
        }

        private void cb_lines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {        
                ShowLine((cb_lines.SelectedValue as PO.Line).Id);    
        }

        private void ShowLine(int code)
        {
            //currentDisplayLineTrip= l.ToList().Where(item=>item.Code==code).Select(item=>item);
            IEnumerable<PO.LineTrip> lineTrip_oF_Line = MainWindow.bl.Find_All_LineTrip_Of_Line(code).ToList().Select(item => new PO.LineTrip { Id = item.Id, LineId = item.LineId, StartAt = item.StartAt });
            lb_lineTrip.DataContext = lineTrip_oF_Line;
        }
    }
}
