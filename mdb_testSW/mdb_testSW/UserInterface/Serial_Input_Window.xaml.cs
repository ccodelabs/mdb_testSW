using MahApps.Metro.Controls;
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

namespace mdb_testSW.UserInterface
{
    /// <summary>
    /// Interaction logic for Serial_Input_Window.xaml
    /// </summary>
    public partial class Serial_Input_Window : MetroWindow
    {
        public string serialNumber = "Cancel";
        public Serial_Input_Window()
        {
            InitializeComponent();
            this.Topmost = true;
            Activate();
            serial_number_txtbox.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            serialNumber = serial_number_txtbox.Text;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            serialNumber = "Cancel";
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
