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

namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for BusInformationWindow.xaml
    /// </summary>
    public partial class BusInformationWindow : Window
    {
        public static Thread t_treatment;
        public int index { get; set; }
        public BusInformationWindow()
        {
            t_treatment = new Thread(bus_treatment);
            InitializeComponent();
        }
        public void bus_treatment()
        {
            MainWindow.Buses[index].state = dotNet_01_9444_0180.Status.on_a_treatment;
            Thread.Sleep(144000);
            MainWindow.Buses[index].state = dotNet_01_9444_0180.Status.ready;
        }

        private void refeul_button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.trefeul.Start();
        }

        private void treatment_button_Click(object sender, RoutedEventArgs e)
        {
            t_treatment.Start();
        }
    }
}
