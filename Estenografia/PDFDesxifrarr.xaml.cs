using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PDFDesxifrarr.xaml
    /// </summary>
    public partial class PDFDesxifrarr : Window
    {
        MyImage img;

        int posHori;
        int posVert;

        public PDFDesxifrarr()
        {
            InitializeComponent();
        }

        private void btnCarregar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Fitxers imatge|*.jpg;*.png";
            String resourceImg = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\img";

            ofd.InitialDirectory = resourceImg;
            bool? hanClickatAcceptar = ofd.ShowDialog();

            if (hanClickatAcceptar == true)
            {
                try
                {
                    //Carregar la imatge al pictureBox.
                    imgOriginal.Source = new BitmapImage(new Uri(ofd.FileName));
                    img = new MyImage(new BitmapImage(new Uri(ofd.FileName)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al carregar la imatge: " + ex.Message);
                }
            }
        }

        private void btnDesxifrar_Click(object sender, RoutedEventArgs e)
        {
            FileStream sRPDF = new FileStream("C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\PDF\\PDFFinal.pdf", FileMode.Create);
            try
            {
                posHori = 0;
                posVert = 0;
                var bytes = img.getPixel(posHori, posVert);

                while (bytes[3] == 255) 
                {
                    sRPDF.WriteByte(bytes[0]);
                    bytes = img.getPixel(posHori, posVert);
                    if (posHori == img.Width)
                    {
                        posHori = 0;
                        posVert++;
                    }
                    posHori++;
                    bytes = img.getPixel(posHori, posVert);
                }
                sRPDF.Close();
                tbMissatge.Text = "Transformacio correcta";
            }
            catch (Exception ex)
            {
                tbMissatge.Text = "Error a l'hora de desxifrar l'imatge: " + ex.Message;
            }
            sRPDF.Close();
        }
    }
}
