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
    /// Interaction logic for wndXifrar.xaml
    /// </summary>
    public partial class wndXifrar : Window
    {
        MyImage img;
        String direccioFinal = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\img\\imgFinal.png";
        int i = 0;
        int posHori = 0;
        int posVert = 0;

        public wndXifrar()
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

        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String direcFinal = "C:\\ProjectsCFGS\\2nCurs\\ServeisProces\\Estenografia\\Estenografia\\img\\imgFinal" + i + ".png";
                String textXifrat = tbMissatge.Text;
                String[] binariText = new string[textXifrat.Length];
                char[] nombreBinari = new char[8];
                for (int i = 0; i < textXifrat.Length; i++)
                {
                    binariText[i] = Convert.ToString(textXifrat[i], 2).PadLeft(8, '0');
                }

                for (int y = 0; y < binariText.Length; y++)
                {
                    var bytes = img.getPixel(posHori, posVert);
                    for (int i = 0; i < binariText[y].Length; i++)
                    {
                        nombreBinari[i] = binariText[y][i];
                    }
                    int B = ConvertirAEntero(nombreBinari);
                    string G = Convert.ToString(bytes[1], 2).PadLeft(8, '0');
                    string R = Convert.ToString(bytes[2], 2).PadLeft(8, '0');
                    string A = Convert.ToString(bytes[3], 2).PadLeft(8, '0');
                    byte[] prova = new byte[] {
                Convert.ToByte(B),
                bytes[1],
                bytes[2],
                bytes[3]};
                    img.setPixel(posHori, posVert, prova);
                    if (posHori == img.Width)
                    {
                        posHori = 0;
                        posVert++;
                    }
                    posHori++;
                    img.writeToDisk(direcFinal);
                }
                imgMod.Source = new BitmapImage(new Uri(direcFinal));
            }
            catch (Exception ex)
            {
                tbMissatge.Text = "ERROR A L'HORA DE CODIFICAR, MATEIX FITXER";
            }           
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
                MessageBox.Show("Error a l'hora de guardar l'imatge: " + ex.Message);
            }

        }
    }
}
