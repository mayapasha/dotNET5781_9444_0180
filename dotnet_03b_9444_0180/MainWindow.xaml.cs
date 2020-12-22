using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet_01_9444_0180;
using System.ComponentModel;
using System.Windows.Threading;
//Lihi barlev 324129444
// Maya Pasha 322290180
namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool MyProperty { get; set ; }
        // public static Thread trefeul;
        public static BackgroundWorker worker;// thread to refeul
        public static BackgroundWorker workerTreatment;// thread to treatment
        public static BackgroundWorker workerDrive;// threat to drive
        Buss my_bus;
        public static Buss help_bus;

        public static ObservableCollection<Buss> Buses { get; set; }
        DispatcherTimer countTimer;
        TimeSpan countDown;
        //BussList b = new BussList();
        Random r = new Random();
        int licensehelp = 0;
        string license = "";
        public MainWindow()
        {
             //r = new Random();
            help_bus = new Buss();
            countDown = new TimeSpan();
            countTimer = new DispatcherTimer();
            Buses = new ObservableCollection<Buss>();
            DateTime startdate = new DateTime();
            Random r = new Random();

            for (int i = 0; i < 10; i++)// loop that make 10 random buses
            {
                my_bus = new Buss();
                startdate = new DateTime(r.Next(2000, 2020), r.Next(1, 12), r.Next(1, 31));
                licensehelp = 0;
                license = "";
                if (startdate.Year < 2018)
                {
                    licensehelp = r.Next(1000000, 9999999);
                    int div = 1000000;
                    for (int j = 0; j < 9; j++)
                    {
                        if (j == 2 || j == 6)
                        {
                            license += "-";
                        }
                        else
                        {
                            license += licensehelp / div;
                            licensehelp %= div;
                            div /= 10;
                        }
                    }

                }
                else
                {
                    licensehelp = r.Next(10000000, 99999999);
                    int div = 10000000;
                    for (int j = 0; j < 10; j++)
                    {
                        if (j == 3 || j == 6)
                        {
                            license += "-";
                        }
                        else
                        {
                            license += licensehelp / div;
                            licensehelp %= div;
                            div /= 10;
                        }
                    }
                }

                my_bus = new Buss(license, startdate);
                my_bus.ThisMileage = r.Next(0, 20000);
                my_bus.FuelMileage = r.Next(0, 1200);
                my_bus.Mileage = my_bus.ThisMileage + my_bus.FuelMileage;
                int extra_year = r.Next(0, 2);
                my_bus.DateOfTreatment = new DateTime(startdate.Year + extra_year, startdate.Month, startdate.Day);
                my_bus.On_work = true;
                my_bus.WatchTime = "00:00:00";
                Buses.Add(my_bus);
            }


            InitializeComponent();
            LbBuses.DataContext = Buses;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            Buses.CollectionChanged += CollectionChanged;

            workerTreatment = new BackgroundWorker();
            workerTreatment.DoWork += WorkerTreatment_DoWork;
            workerTreatment.ProgressChanged += WorkerTreatment_ProgressChanged;
            workerTreatment.RunWorkerCompleted += WorkerTreatment_RunWorkerCompleted;
            workerTreatment.WorkerReportsProgress = true;

            workerDrive = new BackgroundWorker();
            workerDrive.DoWork += WorkerDrive_DoWork;
            workerDrive.ProgressChanged += WorkerDrive_ProgressChanged;
            workerDrive.RunWorkerCompleted += WorkerDrive_RunWorkerCompleted;
            workerDrive.WorkerReportsProgress = true;
            

        }

        private void WorkerDrive_DoWork(object sender, DoWorkEventArgs e)
        {
           
            Buss bus = (e.Argument) as Buss;
            int ind = Buses.IndexOf(bus);
            int drive_distance = int.Parse(bus.disDriving.ToString());
            
            int drive_speed = r.Next(20, 50);
            bus.Drive_Time= (drive_distance / drive_speed) * 6;
            countDown = TimeSpan.FromSeconds(bus.Drive_Time + 1);
            countTimer.Start();
            
            if (bus.ReadyForDrive(drive_distance))// check if the bus can do the drive
            {
                bus.On_work = false;
                for (int i = 1; i < bus.Drive_Time + 2; i++)// every second we call the report progress func
                {
                    System.Threading.Thread.Sleep(1000);
                    workerDrive.ReportProgress(i, bus);
                }
                bus.On_work =true;
                e.Result = ind;
            }
            else// if the bus can not do the drive
            {
                MessageBox.Show("the bus can not do the driving");
                e.Result = null;
            }
        }

        private void WorkerDrive_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Buss bus = (e.UserState) as Buss;
            int ind = Buses.IndexOf(bus);
            bus.state = dotNet_01_9444_0180.Status.on_drive;// change the status of the bus
            countDown = countDown.Add(TimeSpan.FromSeconds(-1));// update the clock one second less
            bus.WatchTime = countDown.ToString();// put the time in the clock
            
        }

        private void WorkerDrive_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)// if the the bus did the drive 
            {
                int ind = (int)e.Result;
                Buses[ind].state = dotNet_01_9444_0180.Status.ready;// change the status
                Buses[ind].disDriving = "";
                Buses[ind].Drive_Time = 0;// reset the clock
            }
        }

        private void WorkerTreatment_DoWork(object sender, DoWorkEventArgs e)
        {
            Buss bus = (e.Argument) as Buss;
            int ind = Buses.IndexOf(bus);
            countDown = TimeSpan.FromSeconds(145);
            countTimer.Start();

            for (int i = 1; i < 146; i++)// every second we call the report progress func
            {
                System.Threading.Thread.Sleep(1000);
                workerTreatment.ReportProgress(i, bus);
            }
            e.Result = ind;
        }
        private void WorkerTreatment_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Buss bus = (e.UserState) as Buss;// c
            int ind = Buses.IndexOf(bus);
            bus.state = dotNet_01_9444_0180.Status.on_a_treatment;// change the status
            countDown = countDown.Add(TimeSpan.FromSeconds(-1));// update the clock
            bus.WatchTime = countDown.ToString();// put the right time in the clock
        }
        private void WorkerTreatment_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int ind = (int)e.Result;
            Buses[ind].state = dotNet_01_9444_0180.Status.ready;// change the status
            Buses[ind].treatment();// change the mileage acording to the treatment
        }
        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Buss bus = (e.Argument)as Buss;
            e.Result = bus;
            int ind = Buses.IndexOf(bus);
            countDown = TimeSpan.FromSeconds(13);


            countTimer.Start();

            bus.On_work = false;
            for (int i = 1; i < 14; i++)// every second we call the report progress func
            {
                
                System.Threading.Thread.Sleep(1000);

                worker.ReportProgress(i, bus);
            }
            e.Result = ind;
            bus.On_work = true;
        }
        public void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Buss bus = (e.UserState)as Buss;
            int ind = Buses.IndexOf(bus);

            bus.state = dotNet_01_9444_0180.Status.on_refeul;// change the status

            countDown = countDown.Add(TimeSpan.FromSeconds(-1));// update the clock
            //textblock1.Text = countDown.ToString();
            bus.WatchTime = countDown.ToString();// put the right time in the clock
        }
        public void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int ind = (int)e.Result;
            Buses[ind].state = dotNet_01_9444_0180.Status.ready;// change the status
            Buses[ind].Refuel();// change the mileage acording to the refeul

        }
        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //open new window to add new bus
            AddBusWindow addBusWindow = new AddBusWindow();
            addBusWindow.Show();
        }

        private void drive_click(object sender, RoutedEventArgs e)
        {
            // open new window to send bus for a drive
            var buss = (Buss)((Button)sender).DataContext;
            // buss.state = Status.on_drive;
            if (buss.state == dotNet_01_9444_0180.Status.ready)// if the bus ready for drive
            {
                DriveWindow driveWindow = new DriveWindow(buss);
                driveWindow.Show();
            }
        }

        private void refeul_click(object sender, RoutedEventArgs e)
        {
            // send the bus for refeul
            if (worker.IsBusy != true)
            {
                if(((sender as Button).DataContext as Buss).state==dotNet_01_9444_0180.Status.ready)
                {
                    worker.RunWorkerAsync((sender as Button).DataContext as Buss);
                }
                
            }
        }

       


        private void LbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // open a window that show informatition about the bus
            int ind = LbBuses.SelectedIndex;
            BusInformationWindow busInformationWindow = new BusInformationWindow(ind);
            busInformationWindow.index = LbBuses.SelectedIndex;
            busInformationWindow.Show();
        }

    }
}
