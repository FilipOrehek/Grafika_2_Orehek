using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GALPR_Blank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            VRAM vram = new VRAM(500, 500);
            Point2D P0 = new Point2D(150, 150);
            Point2D P1 = new Point2D(480, 200);
            Point2D P2 = new Point2D(150, 250);
            Point2D P3 = new Point2D(400, 320);

            Calculations calculations = new Calculations();
            Point2D p1R = calculations.CalculateR(P0, P1, P0);
            Point2D p1L = calculations.CalculateL(P0, P1, P0);
            Point2D p2R = calculations.CalculateR(P1, P2, P0);
            Point2D p2L = calculations.CalculateL(P1, P2, P0);
            Point2D p3R = calculations.CalculateR(P2, P3, P1);
            Point2D p3L = calculations.CalculateL(P2, P3, P1);
            Point2D p4R = calculations.CalculateR(P3, P3, P2);
            Point2D p4L = calculations.CalculateL(P3, P3, P2);

            BezierCurve curve1 = new BezierCurve(P0, p1R, p2L, P1);
            BezierCurve curve2 = new BezierCurve(P1, p2R, p3L, P2);
            BezierCurve curve3 = new BezierCurve(P2, p3R, p4L, P3);
            curve1.DrawToVRAM(vram, Color.Red);
            curve2.DrawToVRAM(vram, Color.Blue);
            curve3.DrawToVRAM(vram, Color.Green);

            pictureBoxCustom1.Image = vram.GetBitmap();
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
