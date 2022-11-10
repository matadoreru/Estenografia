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

namespace Estenografia
{
    /// <summary>
    /// Interaction logic for PDFMenu.xaml
    /// </summary>
    public partial class PDFMenu : Window
    {
        public PDFMenu()
        {
            InitializeComponent();
        }

        private void btnDesxifrar_Click(object sender, RoutedEventArgs e)
        {
            PDFDesxifrarr wndDesxifrar = new PDFDesxifrarr();
            wndDesxifrar.ShowDialog();
        }

        private void btnXifrar_Click(object sender, RoutedEventArgs e)
        {
            pdfXifrar wndXifrar = new pdfXifrar();
            wndXifrar.ShowDialog();
        }
    }
}
