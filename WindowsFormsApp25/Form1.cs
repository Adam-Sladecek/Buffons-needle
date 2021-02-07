using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp25
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        
       
        Pen pen = new Pen(Color.Red, 5f);
        private void Form1_Load(object sender, EventArgs e)
        {
            sum_of_intersections = 0;
            number_of_throws = 0;
            space_between_lines = 100;
            number_of_lines = 10;
            needle_length = space_between_lines / 2;
            switch1 = 0;
            switch2 = 0;

            timer1.Enabled = true;
            timer1.Interval = 1;
           
            backgroundWorker2.WorkerSupportsCancellation = true;
            
          
           
            
        }
        int space_between_lines, number_of_lines, needle_length, sum_of_intersections, time_interval, switch1, switch2, number_of_throws;
        double ratio;
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           int i;
           
            
            
                Graphics g = e.Graphics;
                for (i = 0; i <= number_of_lines - 1; i++)
                {
                    g.DrawLine(Pens.Black, (i * space_between_lines), 0, (i * space_between_lines), ((number_of_lines - 1) * space_between_lines));

                }
            
            
         
        }
        
        
       
        Point b1, b2;
       
        private void Animation()
        {
            while (true)
            {
                if (backgroundWorker2.CancellationPending)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(time_interval);

                    int xsur, ysur, i;
                  
                    xsur = rnd.Next(space_between_lines * (number_of_lines - 1));
                    ysur = rnd.Next(space_between_lines * (number_of_lines - 1));
                    
                    (Point, Point) g;
                    g = line_segment(xsur, ysur);
                    b1 = g.Item1;
                    b2 = g.Item2;
                    pen.Color = Color.Red;
                    
                    if (is_intersecting(p1, p2))
                    {
                        sum_of_intersections += 1;
                        pen.Color = Color.Blue;
                    }

                    number_of_throws += 1;

                    
                    ratio = (2.0 * needle_length) / (space_between_lines * (Convert.ToDouble(sum_of_intersections) / Convert.ToDouble(number_of_throws)));
                    Graphics t = CreateGraphics();
                    t.DrawLine(pen, b1.X, b1.Y, b2.X, b2.Y);
              
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch1 = 1 - switch2;
            switch2 = 1;
            if (switch1 == 1)
            {
                button1.Text = "Stop";
                backgroundWorker2.RunWorkerAsync();
            }
            else
            {
                button1.Text = "Štart";
                switch2 = 0;
                backgroundWorker2.CancelAsync();
            }
           

        }

        Point middle = new Point();
        Point p1 = new Point();
        Point p2 = new Point();
        private void timer1_Tick(object sender, EventArgs e)
        {


            label1.Text = "Number of intersections : " + Convert.ToString(sum_of_intersections);
            label2.Text = "Number of throws : " + Convert.ToString(number_of_throws);
             label3.Text = "Aproxiamation : " + Convert.ToString(ratio);
            label8.Text = "Deviation : " + Math.Abs(Math.PI - ratio).ToString();
            time_interval = trackBar1.Value;
           

        }

      

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Animation();
            
        }


        double rand_angle;
       
       
        
        (Point,Point) line_segment (int xx,int yy)
        {
            
           
            middle.X = xx;
            middle.Y = yy;
            
            rand_angle = rnd.NextDouble()*(Math.PI);
            
            p1.X = middle.X + Convert.ToInt32(needle_length / 2* Math.Cos(rand_angle));
            p1.Y = middle.Y + Convert.ToInt32(needle_length / 2 * Math.Sin(rand_angle));
           
            p2.X = middle.X - Convert.ToInt32(needle_length / 2 * Math.Cos(rand_angle));
            p2.Y = middle.Y - Convert.ToInt32(needle_length / 2 * Math.Sin(rand_angle));
            return (p1,p2) ;  

        }
        bool is_intersecting (Point a,Point b)
        {
            int i;
            
            for (i=0;i<=(number_of_lines - 1)* space_between_lines; i+= space_between_lines)
            {
             
                if ((a.X<=i && b.X>=i) || (b.X <= i && a.X >= i))
                {
                    return true;
                }
                
                
            }
            return false;
        }
    }
}
