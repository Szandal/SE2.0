using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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

namespace SE
{
    /// <summary>
    /// Logika interakcji dla klasy InputBox.xaml
    /// </summary>
    public partial class InputBox : MetroWindow
    {
        private string text;
        public InputBox(string message)
        {
            InitializeComponent();
            Message.Content = message;
            Visibility = Visibility.Visible;

        }


        public string GetText()
        {
            return text;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            text = InputText.Text;
            Close();
        }
    }
}
