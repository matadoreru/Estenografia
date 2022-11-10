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
    /// Interaction logic for pdfXifrar.xaml
    /// </summary>
    public partial class pdfXifrar : Window
    {
        MyImage img;
        String direccioFinal = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\img\\imgFinal.png";

        FileStream sRPDF = null;
        int i = 0;
        int posHori = 0;
        int posVert = 0;
        int H;
        public pdfXifrar()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String direcFinal = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\img\\imgFinal" + i + ".png";
                sRPDF.Seek(0, SeekOrigin.Begin);
                byte[] bytes = null;
                byte[] byteModificat = null;

                for (int y = 0; y < sRPDF.Length; y++)
                {
                    bytes = img.getPixel(posHori, posVert);
                    int B = sRPDF.ReadByte();

                    byteModificat = new byte[] {
                    Convert.ToByte(B),
                    bytes[1],
                    bytes[2],
                    Convert.ToByte(255)};
                    
                    
                    img.setPixel(posHori, posVert, byteModificat);
                    if (posHori == img.Width)
                    {
                        posHori = 0;
                        posVert++;
                    }
                    posHori++;
                    img.writeToDisk(direcFinal);
                }
                bytes = img.getPixel(posHori, posVert);
                byteModificat = new byte[] {
                    Convert.ToByte(0),
                    bytes[1],
                    bytes[2],
                    Convert.ToByte(122)};
                img.setPixel(posHori, posVert, byteModificat);
                img.writeToDisk(direcFinal);
                imgMod.Source = new BitmapImage(new Uri(direcFinal));
            }
            catch (Exception ex)
            {
                tbLogPDF.Text = "ERROR A L'HORA DE CODIFICAR: " + ex.Message;
            }
            sRPDF.Close();
        }

        private void btnCarregarPDF_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF|*.pdf";
            String resourceImg = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\PDF";

            ofd.InitialDirectory = resourceImg;
            bool? hanClickatAcceptar = ofd.ShowDialog();

            if (hanClickatAcceptar == true)
            {
                try
                {
                    //Carregar la imatge al pictureBox.
                    sRPDF = new FileStream(ofd.FileName, FileMode.Open);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al carregar el PDF: " + ex.Message);
                }
            }
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            String direcFinal = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\img\\imgFinal";

            try
            {
                save.InitialDirectory = direcFinal;
                save.Filter = "Imatge |*.jpg";

                if (save.ShowDialog() == true)
                {
                    img.writeToDisk(save.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error a l'hora de guardar l'imatge amb el PDF: " + ex.Message);
            }
        }
    }
}
