using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        // углы
        double alpha, beta;
        //количество кругов в фигуре
        int num;
        //начальные координаты
        double X0, Y0;
        // фигура
       List<XYZ> points = new List<XYZ>();
        //масштаб
        double kx = 1, ky = 1;
        //цвет фигуры
        Pen pen = new Pen(Color.Black);
        public Form1()
        {
            InitializeComponent();

            //задаем начальные координаты
            X0 = pictureBox1.Width / 2;
            Y0 = pictureBox1.Height / 2;
            //задаем углы
            alpha = 0;
            beta = 0;

            cinder();
            draw(points);
            textBox1.Text = "1";
            textBox2.Text = "1";

        }
        //проекция
        private Point projection(XYZ p)
        {
            double x = (Math.Cos(alpha) * p.X + Math.Sin(alpha) * p.Y) * kx + X0;
            double y = (-Math.Sin(alpha)* Math.Cos(beta) * p.X + Math.Cos(alpha)* Math.Cos(beta) * p.Y +p.Z*Math.Sin(beta)) * ky + Y0;
            return new Point((int)(x), (int)(y));
        }
        //рисуем проекцию
        public void draw(List<XYZ> p)
        {
            // проверка наличия фигуры
            if (points.Count <= 0)
                return;
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bitmap);
            for (int i = 0; i < p.Count - 1; i++)
            {
                g.DrawLine(pen, projection(p[i]), projection(p[i + 1]));
                pictureBox1.Image = bitmap;
            }
            //сетка фигуры
            for (int i = 0; i< points.Count-num; i++)
            {
                g.DrawLine(pen, projection(p[i]), projection(p[i + num]));
            }
            pictureBox1.Image = bitmap;

        }
        //фигура
        public void cinder()
        {
            if (points != null)
                points.Clear();
            double R = 150;
            double x=0, y=0, z=0;
            
            for (double i = 0.00; i <= 180; i += 10)
            {
                num = 0;
                for (double j = 0.00; j <= 360; j += 10)
                {
                    double a = (double)(i * Math.PI / 180),
                        b = (double)(j * Math.PI / 180);
                    
                    x = R * Math.Sin(a) * Math.Cos(b);
                    y = R * Math.Sin(a) * Math.Sin(b);
                    
                    if (R * Math.Cos(a) > 0)
                    {
                        z = R * Math.Cos(a) + R;
                    }
                    else z = R * Math.Cos(a);

                    num++;
                    points.Add(new XYZ(x, y, z));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kx = Convert.ToDouble(textBox1.Text);
            ky = Convert.ToDouble(textBox2.Text);
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            draw(points);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                X0 +=3;
                draw(points);
            }
            if (e.KeyCode == Keys.A)
            {
                X0-=3;
                draw(points);
            }
            if (e.KeyCode == Keys.S)
            {
                Y0+=3;
                draw(points);
            }
            if (e.KeyCode == Keys.W)
            {
                Y0-=3;
                draw(points);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            alpha = (double)(trackBar1.Value) / 100;
            draw(points);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            beta = (double)(trackBar2.Value) / 100;
            draw(points);
        }

    }
}
