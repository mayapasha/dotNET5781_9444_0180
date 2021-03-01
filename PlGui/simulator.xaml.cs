using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for simulator.xaml
    /// </summary>
    public partial class simulator : Window
    {

        Stopwatch stopwatch;//stopwatch that runs behind
        BackgroundWorker timerworker;
        TimeSpan tsStartTime;//save the time when the stopwatch started working
        bool isTimerRun;
        public static ObservableCollection<BO.LineTiming> LineTimings { get; set; }
        public PO.Station station { get; set; }


        public simulator(PO.Station s)
        {
            InitializeComponent();
            station = s;
            tbIdStation.Content = station.Code;
            tbNameStation.Content = station.Name;
            Closing += Window_Closing;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


            stopwatch = new Stopwatch();//a new stopwatch that runs behind, since the window was open.
            tsStartTime = DateTime.Now.TimeOfDay;//save the time (date and timeSpan) when the stopwatch started working
            stopwatch.Restart();
            isTimerRun = true;

            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;

            timerworker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(231);
                Thread.Sleep(1000);//report progress each one second
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurrentTime = tsStartTime + stopwatch.Elapsed;//the curr time is the start time+ the time passed since then
            string timmerText = tsCurrentTime.ToString().Substring(0, 8);//take only hour, min, sec. 00:00:00 , 8 characters.
            clock.Text = timmerText;//show the current time. (as TimeSpan).
            BO.Station stationBO = new BO.Station() { Code = station.Code, Lattitude = station.Lattitude, Longitude = station.Longitude, Name = station.Name };
            // lbLineTiming.ItemsSource = MainWindow.bl.GetLineTimingForStation(stationBO, tsCurrentTime).ToList();
            IEnumerable<string> listOfStaing = from item in MainWindow.bl.GetLineTimingForStation(stationBO, tsCurrentTime).ToList()
                                               select item.ToString();
            lbLineTiming.ItemsSource = listOfStaing;
            if (lbLineTiming.Items.Count == 0)
                textOfLB.Text = "no lines avalible for now";
            else
                textOfLB.Text = "have a nice drive:)";
            //  NoBusesSoon.Visibility = Visibility.Visible;


        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            stopwatch.Stop();
            isTimerRun = false;
        }


    }


}
