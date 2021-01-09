using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        public AddLineWindow()
        {
            InitializeComponent();
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void b_save_line_Click(object sender, RoutedEventArgs e)
        {
           
            BO.Line b = new BO.Line();
            b.Code = int.Parse(t_line_code.Text);
            b.Area = (BO.Enums.Areas)cb_line_area.SelectedItem;
            //creat ienumerble of line stations from the list box
            MainWindow.bl.Add_Line(b);
            Close();
        }
    }
}
