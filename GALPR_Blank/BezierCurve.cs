using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GALPR_Blank
{
    public class BezierCurve
    {
        public Point2D P0 { get; set; }
        public Point2D P1 { get; set; }
        public Point2D P2 { get; set; }
        public Point2D P3 { get; set; }

        public BezierCurve(Point2D startingPoint, Point2D startingR, Point2D endingL, Point2D endingPoint)
        {
            P0 = startingPoint;
            P1 = startingR;
            P2 = endingL;
            P3 = endingPoint;
        }

        public void DrawToVRAM(VRAM vram, Color color)
        {
            int step = 1000;
            double add = 1 / (double)(step - 1);
            double t = 0;

            Point2D lastPoint = P0;
            for (int i = 0; i < step; i++)
            {
                Point2D temp = GetPointByT(t);
                vram.SetPixel((int)(temp.X + 0.5), (int)(temp.Y + 0.5), color);
                t += add;
                lastPoint = temp;
            }
        }

        public Point2D GetPointByT(double t)
        {
            double mt = 1 - t;
            double mt2 = mt * mt;
            double tt = t * t;

            double wForP0 = mt * mt2;
            double wForP1 = 3 * mt2 * t;
            double wForP2 = 3 * mt * tt;
            double wForP3 = t * tt;

            Point2D export = new Point2D(0, 0);
            export.X = wForP0 * P0.X + wForP1 * P1.X + wForP2 * P2.X + wForP3 * P3.X;
            export.Y = wForP0 * P0.Y + wForP1 * P1.Y + wForP2 * P2.Y + wForP3 * P3.Y;

            return export;
        }
    }
}
