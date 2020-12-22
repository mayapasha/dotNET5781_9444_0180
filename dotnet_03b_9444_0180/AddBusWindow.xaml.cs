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
using System.Text.RegularExpressions;
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
            // check if the number that enter is a license number
            string license = tblicense.Text;
            if (license.Length == 7)
            {
                license = license.Insert(2, "-");
                license = license.Insert(6, "-");
            }
            else
            if (license.Length == 8)
            {
                license = license.Insert(3, "-");
                license = license.Insert(6, "-");
            }
            else
            {
                MessageBox.Show(" the license info is not currect- please  enter a currect licnse id");
                return;
            }
            DateTime? date = tbdate.SelectedDate;
            Buss b2 = new Buss(license, date.HasValue ? date.Value : new DateTime());
            MainWindow.Buses.Add(b2);
            Close();
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {// the func let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
