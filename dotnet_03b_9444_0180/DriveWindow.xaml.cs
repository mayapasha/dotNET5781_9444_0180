using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
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
using System.Windows.Threading;

namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for DriveWindow.xaml
    /// </summary>
    ///      

    public partial class DriveWindow : Window
    {
        public  Buss buss;
       
       

        public DriveWindow(Buss b)
        {
            this.buss = b;
            InitializeComponent();
            TBdriveText.KeyDown += new KeyEventHandler(tb_KeyDown);
        }
  
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void tb_KeyDown(object sender, KeyEventArgs e)
        {
            // send the bus for a drive by enter key
            if (e.Key == Key.Enter)
            {
                if (MainWindow.workerDrive.IsBusy != true)

                {
                    buss.disDriving = TBdriveText.Text;
                    MainWindow.workerDrive.RunWorkerAsync(buss);
                   
                }

                
            }

        }

        private void bgotodrive_Click(object sender, RoutedEventArgs e)
        {
            //send the bus for a drive by clicking on button
                if (MainWindow.workerDrive.IsBusy != true)

                {
                    buss.disDriving = TBdriveText.Text;
                    MainWindow.workerDrive.RunWorkerAsync(buss);
                }
        }

    }



}
