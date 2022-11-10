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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Estenografia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();         
        }

        private void btnXifrar_Click(object sender, RoutedEventArgs e)
        {
            wndXifrar wndXifrar = new wndXifrar();
            wndXifrar.ShowDialog();
        }

        private void btnDesxifrar_Click(object sender, RoutedEventArgs e)
        {
            wndDesxifrar wndDesxifrar = new wndDesxifrar();
            wndDesxifrar.ShowDialog();
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            PDFMenu wndPDF = new PDFMenu();
            wndPDF.ShowDialog();
        }
    }
}
