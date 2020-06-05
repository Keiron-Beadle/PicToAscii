using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicToAscii
{
    public class AsciiArtCreator
    {
        private string imageLocation;
        //private string scale = " .,-+*#&$";
        private string scale = " .,-*+:o8&#";

        public AsciiArtCreator(string imageLocation)
        {
            this.imageLocation = imageLocation;
        }

        public void Go(string saveLocation)
        {
            Bitmap img = Resized(new Bitmap(imageLocation));

            using (StreamWriter sWrite = new StreamWriter(saveLocation))
            {
                for (int y = 0; y < img.Height; y++)
                {
                    for (int x = 0; x < img.Width; x++)
                    {
                        Color c = img.GetPixel(x, y);
                        c = GetGrayscale(c);
                        int index = (int)Math.Floor(((double)c.R / 255 * (scale.Length - 1)));
                        sWrite.Write(scale[index]);
                    }
                    sWrite.WriteLine();
                }
            }
        }

        private Bitmap Resized(Bitmap bitmap)
        {
            float scaleFactor = 0f;
            int width = 0, height = 0;
            int requiredHeight = 420;
            do
            {
                scaleFactor += 0.1f;
                height = (int)(bitmap.Height / scaleFactor);
            } while (height > requiredHeight);
            width = (int)(bitmap.Width / scaleFactor);
            Rectangle rect = new Rectangle(0, 0, width, height);
            Bitmap returnImg = new Bitmap(width, height);

            returnImg.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(returnImg))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(bitmap, rect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);       
                }
            }
            //returnImg.Save("Help.jpg");
            return returnImg;
        }

        private Color GetGrayscale(Color c)
        {
            return Color.FromArgb((c.R + c.B + c.G) / 3,
                                         (c.R + c.B + c.G) / 3,
                                         (c.R + c.B + c.G) / 3);
        }
    }
}
