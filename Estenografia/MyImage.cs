using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Estenografia
{
    public class MyImage
    {
        byte[] bytesImatge;
        int width;
        int height;
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public MyImage(int w, int h)
        {
            width = w;
            height = h;
            bytesImatge = new byte[width * height * 4];
        }

        public MyImage(BitmapImage imatgeOriginal)
        {
            int bytesPerPixel = imatgeOriginal.Format.BitsPerPixel / 8;
            if (bytesPerPixel != 4)
                throw new Exception("No sabem treballar amb imatges !=4 bpp");
            width = imatgeOriginal.PixelWidth;
            height = imatgeOriginal.PixelHeight;
            bytesImatge = new byte[width * height * bytesPerPixel];
            int stride = width * bytesPerPixel;
            imatgeOriginal.CopyPixels(bytesImatge, stride, 0);
        }

        internal byte[] getPixel(int x, int y)
        {
            int nPixel = y * this.width + x;
            int nByte = nPixel * 4;

            return new byte[] {
                bytesImatge[nByte],
                bytesImatge[nByte+1],
                bytesImatge[nByte+2],
                bytesImatge[nByte+3]};
        }

        internal void setPixel(int x, int y, byte[] bytes)
        {
            int nPixel = y * this.width + x;
            int nByte = nPixel * 4;
            bytesImatge[nByte] = bytes[0];
            bytesImatge[nByte+1] = bytes[1];
            bytesImatge[nByte+2] = bytes[2];
            bytesImatge[nByte+3] = bytes[3];
        }

        public WriteableBitmap GetWriteableBitmap()
        {
            int bytesPerPixel = 4;
            WriteableBitmap wb = new WriteableBitmap(width, height,
                96, 96, PixelFormats.Bgr32, null);
            int stride = width * bytesPerPixel;
            wb.WritePixels(new Int32Rect(0, 0, width, height), this.bytesImatge,
                stride, 0);
            return wb;
        }


        internal void writeToDisk(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                BmpBitmapEncoder codificador = new BmpBitmapEncoder();
                codificador.Frames.Add(BitmapFrame.Create(GetWriteableBitmap()));
                codificador.Save(stream);
            }
        }
    }
}
