using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using dotNet_01_9444_0180;
using System.ComponentModel;

namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for BusInformationWindow.xaml
    /// </summary>
    public partial class BusInformationWindow : Window
    {
        // public static Thread t_treatment;
        //ublic Buss buss;
        

        public int index { get; set; }
        public BusInformationWindow(int selectedIndex)
        {
            
           
            index = selectedIndex;

            InitializeComponent();
            L_license.Text =MainWindow.Buses[selectedIndex].LicenseNum;
            L_mileage.Text = MainWindow.Buses[selectedIndex].Mileage.ToString();
            L_startDate.Text = MainWindow.Buses[selectedIndex].startdate.ToString();
            L_state.Text = MainWindow.Buses[selectedIndex].state.ToString();
            tb_busDriver.Text= MainWindow.Buses[selectedIndex].DriverName;
            tb_busCost.Text = MainWindow.Buses[selectedIndex].BusCost.ToString();
            tb_busCompenyType.Text = MainWindow.Buses[selectedIndex].BusCompenyType;
            tb_madeIn.Text = MainWindow.Buses[selectedIndex].MadeIn;
        
        }
      

        private void refeul_button_Click(object sender, RoutedEventArgs e)
        {
            // send the bus to refeul
            if (MainWindow.worker.IsBusy != true && MainWindow.Buses[index].state==dotNet_01_9444_0180.Status.ready)
            {
                MainWindow.worker.RunWorkerAsync(MainWindow.Buses[index]);
            }
        }

        private void treatment_button_Click(object sender, RoutedEventArgs e)
        {
            // send the bus to treatment
            if (MainWindow.workerTreatment.IsBusy != true && MainWindow.Buses[index].state == dotNet_01_9444_0180.Status.ready)
            {
                MainWindow.workerTreatment.RunWorkerAsync(MainWindow.Buses[index]);
            }
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            // save the data in the text boxes
            MainWindow.Buses[index].DriverName = tb_busDriver.Text;
            if(tb_busCost.Text!="")
            {
                MainWindow.Buses[index].BusCost = int.Parse(tb_busCost.Text);
            }

            MainWindow.Buses[index].BusCompenyType = tb_busCompenyType.Text;
            MainWindow.Buses[index].MadeIn = tb_madeIn.Text;

        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // the func let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
