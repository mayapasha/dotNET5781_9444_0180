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
    /// Interaction logic for DeleteLineWindow.xaml
    /// </summary>
    public partial class DeleteLineWindow : Window
    {
        public DeleteLineWindow()
        {
            InitializeComponent();
        }
        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // func that let enter only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void b_delete_line_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               BO.Line l= MainWindow.bl.Get_Line(int.Parse(t_delete_line.Text));
                MainWindow.bl.Delete_Line(l);
            }
            catch(BO.Exceptions.Add_Existing_Item_Exception)
            {
                MessageBox.Show("the line that you want to delete is not found");
            }
            Close();
        }
    }
}
