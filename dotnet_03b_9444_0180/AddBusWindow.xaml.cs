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
using dotNet_01_9444_0180;

namespace dotnet_03b_9444_0180
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        public AddBusWindow()
        {
            InitializeComponent();
        }
        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string license = tblicense.Text;                    
           if(license.Length==7)
            {

            }
           else
            {

            }
            DateTime? date = tbdate.SelectedDate;
           

           
            
            Buss b2 = new Buss(license, date.HasValue ? date.Value : new DateTime());
            MainWindow.Buses.Add(b2);
            Close();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

 

        private void tbdate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
