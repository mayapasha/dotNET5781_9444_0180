using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
                 startdate = new DateTime(r.Next(2000, 2020), r.Next(1, 12), r.Next(1, 31));
                licensehelp = 0;
                license = "";
                if (startdate.Year < 2018)
                {
                    licensehelp = r.Next(1000000, 9999999);                
                    int div = 1000000;
                    for(int j=0;j<9;j++)
                    {
                        if(j==2 ||j==6)
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
                
                b.AddNewBuss(license,startdate);
                b.Busses[i].ThisMileage = r.Next(0, 20000);
                b.Busses[i].FuelMileage = r.Next(0, 1200);
                b.Busses[i].Mileage = b.Busses[i].ThisMileage + b.Busses[i].FuelMileage;
                b.Busses[i].DateOfTreatment = new DateTime (startdate.Year+r.Next(0,2),startdate.Month,startdate.Day); 
                
            }
            foreach (var item in b.Busses)
            {
                Buses.Add(item);
            }

            //bool flag = false;

            //for (int i = 0; i < 3; i++)
            //{
            //    flag = false;
            //    foreach (var item in b.Busses)
            //    {
            //        if(i==0)
            //        {

            //        }



            //    }
            //}
            InitializeComponent();
            for (int i = 0; i < 10; ++i)
            {
                //Button newButton = new Button();
                //newButton.Content = "";
                //newButton.Name = "btn";
                //newButton. = true;
               // newButton.Background =;
               // newButton.ForeColor = Color.Black;

              //  LbBuses.Items.Add(newButton);
               // newButton.Dock = DockStyle.Top;
                //newButton.BringToFront();


                ListBoxItem newItem = new ListBoxItem();              
                newItem.Content = Buses[i];
                //newItem. = new Button();
                LbBuses.Items.Add(newItem);
                //LbBuses.Items[i]
            }
            Buses.CollectionChanged += CollectionChanged;
            //LbBuses.DataContext = Buses;
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

        private void LbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
