using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Seminar_4
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ConvexHull(e);
        }

        public void ConvexHull(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red);
            Random rnd = new Random();

            int n = 50;
            PointF[] m = new PointF[n];

            float raza_m = 5;

            for (int i = 0; i < n; i++)
            {
                m[i].X = rnd.Next(50, this.ClientSize.Width - 50);
                m[i].Y = rnd.Next(50, this.ClientSize.Height - 50);
                g.DrawEllipse(pen, m[i].X - raza_m, m[i].Y - raza_m, raza_m * 2, raza_m * 2);
            }
            Point extrem = new Point();
            int xMin = (int)m[0].X;
            for (int i = 1; i < n; i++)
            {
                if (m[i].X < xMin)
                {
                    extrem.X = (int)m[i].X;
                    extrem.Y = (int)m[i].Y;
                }
            }

            
            
            
            int dis = 0;
            Point[] hullpoints = new Point[n];
            Point A = extrem;
            Point B = new Point();  
            Point C = new Point();
            int k = 0;

            for(int i = 0; i < n; i++)
            {
                if (A.X == m[i].X && A.Y == m[i].Y)
                {
                }
                else
                {
                    B.X = (int)m[i].X;
                    B.Y = (int)m[i].Y;
                    for (int j = 0; j < n; j++)
                    {
                        if(j == i)
                        {
                        }
                        else
                        {
                            C.X = (int)m[i].X;
                            C.Y = (int)m[i].Y;
                            dis = (C.X - A.X) * (B.Y - A.Y) - (C.Y - A.Y) * (B.X - A.X);
                            if(dis > 0)
                            {
                                B.X = C.X;
                                B.Y = C.Y;
                            }
                            else
                            {
                                hullpoints[k].X = C.X;
                                hullpoints[k].Y = C.Y;
                                k++;
                            }
                        }
                    }
                }
                A = B;
            }

            Pen pensula = new Pen(Color.Blue);
            int nrpuncte = hullpoints.Length;
            for(int i = 0; i < nrpuncte - 1; i++)
            {
                g.DrawLine(pensula, hullpoints[i].X, hullpoints[i].Y, hullpoints[i+1].X, hullpoints[i+1].Y);
            }
            g.DrawLine(pensula, hullpoints[nrpuncte-1].X, hullpoints[nrpuncte-1].Y, hullpoints[0].X, hullpoints[0].Y);
        }
    }
}