using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class Line
    {
        public Line() { }
        //x = y = 0
        public static void DrawPoint(Point a, VRAM temp, Color color)
        {
            temp.SetPixel(a.X, a.Y, color);
        }
        //x = 0
        public static void DrawVerticalLine(Point a, Point b, VRAM temp, Color color)
        {
            int height = b.Y - a.Y;
            for (int y = 0; y < height; y++)
            {
                temp.SetPixel(a.X, y, color);
            }
        }
        //y = 0
        public static void DrawHorizontalLine(Point a, Point b, VRAM temp, Color color)
        {
            int width = b.X - a.X;
            for (int x = 0; x < width; x++)
            {
                temp.SetPixel(x, a.Y, color);
            }
        }
        //x = y
        public static void DrawDiagonalLine(Point a, Point b, VRAM temp, Color color)
        {
            int length = Math.Min((b.X - a.X), (b.Y - a.Y));
            for (int i = 0; i < length; i++)
            {
                temp.SetPixel(i, i, color);
            }
        }
        //x != y
        public static void DrawLine(int x1, int y1, int x2, int y2, VRAM temp, Color color)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                temp.SetPixel(x1, y1, color);

                if (x1 == x2 && y1 == y2)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }
        public static void DrawLineWithComparison(Point a, Point b, VRAM temp, Color color)
        {
            if (a.Equals(b))
            {
                DrawPoint(a, temp, color);
            }
            else if (a.X == a.Y)
            {
                DrawLine(a.X, a.Y, b.X, b.Y, temp, color);
            }
            else if (b.X - a.X == 0 || a.X - b.X == 0)
            {
                DrawVerticalLine(a, b, temp, color);
            }
            else if (b.Y - a.Y == 0 || a.Y - b.Y == 0)
            {
                DrawHorizontalLine(a, b, temp, color);
            }
            else if (a.X == a.Y && b.X == b.Y)
            {
                DrawDiagonalLine(a, b, temp, color);
            }
            else if (a.X < a.Y)
            {
                DrawLine(a.X, a.Y, b.X, b.Y, temp, color);
            }
            else
            {
                DrawLine(b.X, b.Y, a.X, a.Y, temp, color);
            }
        }
    }
}
