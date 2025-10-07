using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class ImageProcessing
    {
        public static void Saturation(VRAM vram, double factor)
        {
            for (int y = 0; y < vram.Height; y++)
            {
                for (int x = 0; x < vram.Width; x++)
                {
                    Color temp = vram.GetPixel(x, y);
                    HSL hsl = new HSL(temp);

                    hsl.Saturation *= (float)factor;

                    if (hsl.Saturation > 1) hsl.Saturation = 1;
                    if (hsl.Saturation < 0) hsl.Saturation = 0;

                    Color result = hsl.ToRGBColor();
                    vram.SetPixel(x, y, result);
                }
            }
        }

        public static void HueRotation(VRAM vram, int factor)
        {
            for (int y = 0; y < vram.Height; y++)
            {

                for (int x = 0; x < vram.Width; x++)
                {
                    Color temp = vram.GetPixel(x, y);
                    HSL hsl = new HSL(temp);

                    hsl.Hue += factor;

                    while (hsl.Hue < 0) hsl.Hue += 360;
                    while (hsl.Hue > 359) hsl.Hue -= 360;

                    Color result = hsl.ToRGBColor();
                    vram.SetPixel(x, y, result);
                }
            }

        }

    }
}
