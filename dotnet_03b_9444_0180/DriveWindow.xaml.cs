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
namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for DriveWindow.xaml
    /// </summary>
    ///      
    
    public partial class DriveWindow : Window
    {
        public static Thread tdrive;
        public int index { get; set; }

        
        public DriveWindow()
        {
             tdrive = new Thread(goToDrive);
            // tdrive.Start();
            InitializeComponent();
            TBdriveText.KeyDown += new KeyEventHandler(tb_KeyDown);

        }


        public void goToDrive()
        {
            int drive_distance = int.Parse(TBdriveText.Text);
            if (MainWindow.Buses[index].ReadyForDrive(drive_distance))
            {
                Random r = new Random();
                int drive_speed = r.Next(20, 50);
                int drive_time = (drive_distance / drive_speed) * 6000;
                MainWindow.Buses[index].state = dotNet_01_9444_0180.Status.on_drive;
                Thread.Sleep(drive_time);
                MainWindow.Buses[index].state = dotNet_01_9444_0180.Status.ready;
                Close();
            }
            else
            {
                MessageBox.Show("the current bus is in status:" + MainWindow.Buses[index].state + ",there for it can not do the drive");
                Close();
                
            }
        }
       public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tdrive.Start();              
            }
           
        }

        private void bgotodrive_Click(object sender, RoutedEventArgs e)
        {
            tdrive.Start();
        }
    }



}
