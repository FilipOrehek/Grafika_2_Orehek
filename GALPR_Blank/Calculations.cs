using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GALPR_Blank
{
    public class Calculations
    {
        public Point2D CalculateL(Point2D p, Point2D pNext, Point2D pPrev)
        {
            // Calculate Li = Pi - (Pi+1 - Pi-1)/6
            return new Point2D(p.X - (pNext.X - pPrev.X) / 6, p.Y - (pNext.Y - pPrev.Y) / 6);
        }

        public Point2D CalculateR(Point2D p, Point2D pNext, Point2D pPrev)
        {
            // Calculate Ri = Pi + (Pi+1 - Pi-1)/6
            return new Point2D(p.X + (pNext.X + pPrev.X) / 6, p.Y + (pNext.Y + pPrev.Y) / 6);
        }
    }
}
