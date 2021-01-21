using System;
using System.Collections.Generic;
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
    /// Interaction logic for DisTimeWindow.xaml
    /// </summary>
    public partial class DisTimeWindow : Window
    {
        public PO.LineStation ls { get; set; }
        public DisTimeWindow(PO.LineStation l)
        {
            InitializeComponent();
            ls = l;
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ls.NextStation == -1)
            {
                MessageBox.Show("you cant update this data");
                Close();
            }
            try
            {
                ls.Distance = double.Parse(tb_dis.Text);
                ls.Time = TimeSpan.Parse( tb_time.Text);
                MainWindow.bl.update_LineStation(new BO.LineStation { Distance = ls.Distance, LineId = ls.LineId, LineStationIndex = ls.LineStationIndex, Name = ls.Name, NextStation = ls.NextStation, PrevStation = ls.PrevStation, Station = ls.Station, Time = ls.Time });
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);//("please enter distance and time in correct format");
            }

        }
    }
}
