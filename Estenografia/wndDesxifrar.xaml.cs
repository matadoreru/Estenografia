using Microsoft.Win32;
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
    /// Interaction logic for wndDesxifrar.xaml
    /// </summary>
    public partial class wndDesxifrar : Window
    {
        MyImage img;
        int posHori = 0;
        int posVert = 0;

        public wndDesxifrar()
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
            StringBuilder sB = new StringBuilder();
            try
            {
                String[] binariText = new string[8];

                for (int y = 0; y < img.Height; y++)
                {
                    var bytes = img.getPixel(posHori, posVert);
                    string B = Convert.ToString(bytes[0], 2).PadLeft(8, '0');
                    int valorLletra = ConvertirAEntero(B.ToCharArray());
                    sB.Append((char)valorLletra);
                    if (posHori == img.Width)
                    {
                        posVert++;
                        posHori = 0;
                    }
                    posHori++;
                }
                tbMissatge.Text = sB.ToString();
            }
            catch (Exception ex)
            {
                tbMissatge.Text = "Error a l'hora de desxifrar l'imatge";
            }
            
            posHori = 0;
            posVert = 0;
            sB.Clear();
        }

        private int ConvertirAEntero(char[] bitsLletre)
        {
            int resultat = 0;
            int potencia = 1;
            for (int i = bitsLletre.Length - 1; i >= 0; i--)
            {
                resultat += (bitsLletre[i] - '0') * potencia;
                potencia = potencia * 2;
            }
            return resultat;
        }
    }
}
