using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class HSL
    {
        /// <summary>
        /// Hue component.
        /// </summary>
        /// 
        /// <remarks>Hue is measured in the range of [0, 359].</remarks>
        /// 
        public int Hue;

        /// <summary>
        /// Saturation component.
        /// </summary>
        /// 
        /// <remarks>Saturation is measured in the range of [0, 1].</remarks>
        /// 
        public float Saturation;

        /// <summary>
        /// Luminance value.
        /// </summary>
        /// 
        /// <remarks>Luminance is measured in the range of [0, 1].</remarks>
        /// 
        public float Luminance;

        public HSL() { }

        public HSL(int hue, float saturation, float luminance)
        {
            this.Hue = hue;
            this.Saturation = saturation;
            this.Luminance = luminance;
        }

        public HSL(Color color) {

            float r = (color.R / 255.0f);
            float g = (color.G / 255.0f);
            float b = (color.B / 255.0f);

            float min = Math.Min(Math.Min(r, g), b);
            float max = Math.Max(Math.Max(r, g), b);
            float delta = max - min;

            // get luminance value
            Luminance = (max + min) / 2;

            if (delta == 0)
            {
                // gray color
                Hue = 0;
                Saturation = 0.0f;
            }
            else
            {
                // get saturation value
                Saturation = (Luminance <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

                // get hue value
                float hue;

                if (r == max)
                {
                    hue = ((g - b) / 6) / delta;
                }
                else if (g == max)
                {
                    hue = (1.0f / 3) + ((b - r) / 6) / delta;
                }
                else
                {
                    hue = (2.0f / 3) + ((r - g) / 6) / delta;
                }

                // correct hue if needed
                if (hue < 0)
                    hue += 1;
                if (hue > 1)
                    hue -= 1;

                Hue = (int)(hue * 360);
            }
        }

        public Color ToRGBColor() {

            if (Saturation == 0)
            {
                int gray = (int)Math.Floor(Luminance * 255);
                gray = Math.Min(255, Math.Max(0, gray));

                return Color.FromArgb(gray, gray, gray);
            }
            else
            {
                float v1, v2;
                float hue = (float)Hue / 360;

                v2 = (Luminance < 0.5) ?
                    (Luminance * (1 + Saturation)) :
                    ((Luminance + Saturation) - (Luminance * Saturation));

                v1 = 2 * Luminance - v2;

                int red = (int)Math.Floor(255 * Hue_2_RGB(v1, v2, hue + (1.0f / 3)));
                int green = (int)Math.Floor(255 * Hue_2_RGB(v1, v2, hue));
                int blue = (int)Math.Floor(255 * Hue_2_RGB(v1, v2, hue - (1.0f / 3)));

                red = Math.Min(255, Math.Max(0, red));
                green = Math.Min(255, Math.Max(0, green));
                blue = Math.Min(255, Math.Max(0, blue));

                return Color.FromArgb(red, green, blue);
            }
        }

        private static float Hue_2_RGB(float v1, float v2, float vH)
        {
            if (vH < 0)
                vH += 1;
            if (vH > 1)
                vH -= 1;
            if ((6 * vH) < 1)
                return (v1 + (v2 - v1) * 6 * vH);
            if ((2 * vH) < 1)
                return v2;
            if ((3 * vH) < 2)
                return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);
            return v1;
        }
    }
}
