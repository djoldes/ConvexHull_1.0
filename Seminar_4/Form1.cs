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
            //ConvexHull(e);

            Pen penred = new Pen(Color.Red, 1);
            Random rnd = new Random();
            int n = rnd.Next(10, 50);
            Point[] p = new Point[n];
            p[0].X = rnd.Next(50, this.ClientSize.Width - 50);
            p[0].Y = rnd.Next(50, this.ClientSize.Height - 50);
            e.Graphics.DrawEllipse(penred, p[0].X - 2, p[0].Y - 2, 4, 4);
            int nord = 0, vest = 0, est = 0, sud = 0;
            for (int i = 1; i < n; i++)
            {
                p[i].X = rnd.Next(10, this.ClientSize.Width - 10);
                p[i].Y = rnd.Next(10, this.ClientSize.Height - 10);
                e.Graphics.DrawEllipse(penred, p[i].X - 2, p[i].Y - 2, 4, 4);
            }
            //sortare dupa X//
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (p[i].X < p[j].X)
                        (p[i], p[j]) = (p[j], p[i]);
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (p[vest].X > p[i].X)
                    vest = i;
                if (p[est].X < p[i].X)
                    est = i;
                if (p[nord].Y > p[i].Y)
                    nord = i;
                if (p[sud].Y < p[i].Y)
                    sud = i;
            }
            e.Graphics.DrawEllipse(penred, p[nord].X - 4, p[nord].Y - 4, 8, 8);
            e.Graphics.DrawEllipse(penred, p[sud].X - 4, p[sud].Y - 4, 8, 8);
            e.Graphics.DrawEllipse(penred, p[est].X - 4, p[est].Y - 4, 8, 8);
            e.Graphics.DrawEllipse(penred, p[vest].X - 4, p[vest].Y - 4, 8, 8);

            Pen penYellow = new Pen(Color.Yellow, 2);
            Pen penOrange = new Pen(Color.Orange, 2);

            int aux11 = vest, aux12 = 0;        /////////////soratrea de la V-E/////////////
            for (int i = 0; i < n; i++)
                if (p[i].X > p[aux11].X)
                {
                    aux12 = i;
                    break;
                }
            int aux21 = est, aux22 = 0;         /////////////soratrea de la E-V/////////////
            for (int i = 0; i < n; i++)
                if (p[i].X < p[aux21].X)
                {
                    aux22 = i;
                    break;
                }
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (p[i].X > p[aux11].X)    /////////////soratrea de la V-E/////////////
                    {
                        float m1, m2;
                        m1 = (float)(p[aux11].Y - p[aux12].Y) / (p[aux11].X - p[aux12].X);
                        m2 = (float)(p[aux11].Y - p[i].Y) / (p[aux11].X - p[i].X);
                        if (m1 < m2)
                            aux12 = i;
                        //e.Graphics.DrawLine(penred, p[aux1], p[i]);
                    }
                    if (p[i].X < p[aux21].X)    /////////////soratrea de la E-V/////////////
                    {
                        float m1, m2;
                        m1 = (float)(p[aux21].Y - p[aux22].Y) / (p[aux21].X - p[aux22].X);
                        m2 = (float)(p[aux21].Y - p[i].Y) / (p[aux21].X - p[i].X);
                        if (m1 < m2)
                            aux22 = i;
                        //e.Graphics.DrawLine(penred, p[aux1], p[i]);
                    }
                }
                e.Graphics.DrawLine(penYellow, p[aux11], p[aux12]);
                aux11 = aux12;
                for (int i = 1; i < n; i++)     /////////////soratrea de la V-E/////////////
                    if (p[i].X > p[aux11].X)
                    {
                        aux12 = i;
                        break;
                    }
                e.Graphics.DrawLine(penOrange, p[aux21], p[aux22]);
                aux21 = aux22;
                for (int i = 0; i < n; i++)     /////////////soratrea de la E-V/////////////
                    if (p[i].X < p[aux21].X)
                    {
                        aux22 = i;
                        break;
                    }
            }

            /*public void ConvexHull(PaintEventArgs e)
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
            }*/
        }
    }
}