using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class VRAM
    {
        private int width;
        private int height;
        private Color[,] rawData;

        public int Width { get => width; }
        public int Height { get => height; }

        public VRAM(int width, int height)
        {
            this.width = width;
            this.height = height;

            rawData = new Color[height, width];

            ResetVRAM(Color.White);
        }

        public void ResetVRAM(Color color)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    rawData[y, x] = color;
                }
            }
        }

        public Color GetPixel(int x, int y)
        {
            return rawData[y, x];
        }

        public void SetPixel(int x, int y, Color color)
        {
            rawData[y, x] = color;
        }

        public Bitmap GetBitmap()
        {
            Bitmap colorBitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
            BitmapData bitmap_data = colorBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

            unsafe
            {
                byte* line = (byte*)bitmap_data.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                    {
                        line[2] = rawData[y, x].R;
                        line[1] = rawData[y, x].G;
                        line[0] = rawData[y, x].B;
                        line += 4;
                    }
            }
            colorBitmap.UnlockBits(bitmap_data);
            return colorBitmap;
        }

        public static VRAM CreateFromBitmap(Bitmap sourceBitmap)
        {
            VRAM export = new VRAM(sourceBitmap.Width, sourceBitmap.Height);
            Bitmap temp = sourceBitmap.Clone(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), PixelFormat.Format32bppRgb);
            BitmapData bitmap_data = temp.LockBits(new Rectangle(0, 0, temp.Width, temp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            unsafe
            {
                byte* line = (byte*)bitmap_data.Scan0.ToPointer();
                for (int y = 0; y < export.Height; y++)
                    for (int x = 0; x < export.Width; x++)
                    {
                        export.rawData[y, x] = Color.FromArgb(line[2], line[1], line[0]);
                        line += 4;
                    }
            }

            temp.UnlockBits(bitmap_data);
            temp.Dispose();

            return export;
        }

        public VRAM Copy()
        {
            VRAM export = new VRAM(width, height);
            Array.Copy(rawData, export.rawData, width * height);
            return export;
        }
    }
}
