using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace Generare_Puncte
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random();
        public int[] mx = { 180, -100, 210, 0 };
        public int[] my = { 220, 110, -150, 0 };
        public int[] sigmax = { 10, 15, 5, 600 };
        public int[] sigmay = { 10, 10, 20, 600 };

        public int nrPuncte = 1000;

        int[] xVect = new int[100000];
        int[] yVect = new int[100000];
        int[] squareVect = new int[100000];

        private void Form1_Load(object sender, EventArgs e)
        {
            Gauss(100, 10, 5);
            Console.WriteLine(Gauss(100, 10, 5));

        }

        public double Gauss(int zonaAleasa, int sigma, int xy)
        {
            double numarator = Math.Pow(zonaAleasa - xy, 2);
            double numitor = 2 * Math.Pow(sigma, 2);
            double fractie = numarator / numitor;

            return Math.Exp(-fractie);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            Graphics graphics = panel1.CreateGraphics();
            graphics.TranslateTransform((panel1.Width / 2), (panel1.Height / 2));
            graphics.DrawLine(pen, 0, 300, 0, -300);
            graphics.DrawLine(pen, -300, 0, 300, 0);


            // For generare puncte 
            for (int i = 0; i < nrPuncte; i++)
            {
                int square = rand.Next(0, 4);
                squareVect[i] = square;

                //Console.WriteLine("squareVect " + squareVect[i]);

                bool stopX = false;
                bool stopY = false;
                

                while (stopX == false)
                {
                    float prag = (float)rand.NextDouble();
                    int x = rand.Next(-300, 300);
                    double gauss = Gauss(mx[square], sigmax[square], x);

                    if (gauss >= prag)
                    {
                        xVect[i] = x;
                        stopX = true;

                        Console.WriteLine("x vect " + xVect[i]);
                        Console.WriteLine("zona " + square);
                    }
                }

                while (stopY == false)
                {
                    float prag = (float)rand.NextDouble();
                    int y = rand.Next(-300, 300);
                    double gauss = Gauss(my[square], sigmay[square], y);

                    if (gauss >= prag)
                    {
                        yVect[i] = y;
                        stopY = true;

                        Console.WriteLine("y vect " + yVect[i]);
                        Console.WriteLine("zona " + square);
                    }

                }

            }

            // Desenare puncte

            SolidBrush[] brush = { new SolidBrush(Color.Blue), new SolidBrush(Color.Green), new SolidBrush(Color.Orange), new SolidBrush(Color.Black) };

            for (int i = 0; i < nrPuncte; i++)
            {
                if (squareVect[i] == 0)
                {
                    graphics.FillEllipse(brush[0], xVect[i], yVect[i], 3, 3);
                }
                else if (squareVect[i] == 1)
                {
                    graphics.FillEllipse(brush[1], xVect[i], yVect[i], 3, 3);
                }
                else if (squareVect[i] == 2)
                {
                    graphics.FillEllipse(brush[2], xVect[i], yVect[i], 3, 3);

                }
                else
                {
                    graphics.FillEllipse(brush[3], xVect[i], yVect[i], 3, 3);
                }
            }


        }

    }
}
