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

namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Thread trefeul;
        Buss my_bus;

        public static ObservableCollection<Buss> Buses { get; set; }

        BussList b = new BussList();


        int licensehelp = 0;
        string license = "";
        public MainWindow()
        {
            
            Buses = new ObservableCollection<Buss>();
            DateTime startdate = new DateTime();
            Random r = new Random();

            for (int i = 0; i < 10; i++)
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

                my_bus=new Buss(license, startdate);
                my_bus.ThisMileage = r.Next(0, 20000);
                my_bus.FuelMileage = r.Next(0, 1200);
                my_bus.Mileage = my_bus.ThisMileage + my_bus.FuelMileage;
                my_bus.DateOfTreatment = new DateTime(startdate.Year + r.Next(0, 2), startdate.Month, startdate.Day);
                Buses.Add(my_bus);
            }
           

            InitializeComponent();
            Buses.CollectionChanged += CollectionChanged;
            LbBuses.DataContext = Buses;
            trefeul = new Thread(refeulingTheBus);

        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newbuss = e.NewItems[0];
            LbBuses.Items.Add(newbuss);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow addBusWindow = new AddBusWindow();
            addBusWindow.ShowDialog();
        }

        private void drive_click(object sender, RoutedEventArgs e)
        {
            DriveWindow driveWindow = new DriveWindow();
            driveWindow.index = LbBuses.SelectedIndex;
            driveWindow.ShowDialog();
        }

        private void refeul_click(object sender, RoutedEventArgs e)
        {
            trefeul.Start();
        }

        public void refeulingTheBus()
        {
            int indexCurrent = LbBuses.SelectedIndex;
            Buses[indexCurrent].state = dotNet_01_9444_0180.Status.on_refeul;
            Thread.Sleep(12000);
            Buses[indexCurrent].state = dotNet_01_9444_0180.Status.ready;
        }
  

        private void LbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BusInformationWindow busInformationWindow = new BusInformationWindow();
            busInformationWindow.index = LbBuses.SelectedIndex;
            busInformationWindow.ShowDialog();
        }
    }
}
