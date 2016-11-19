using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Mai trebuie lucrat putin la Searchul patratelor ca sa fie mai fina.
/// </summary>

namespace University_Schedule
{
    public partial class Form_Orar_Nou : Form
    {
        public Course curs; 
        Bitmap bmp; 
        Point[] point; 
        int indexPoint;
        Rectangle selected_rectagle; 

        public Form_Orar_Nou()
        {
            InitializeComponent();
            point = new Point[20]; 
            indexPoint = 0; 
            bmp = Drawing.DrawRectangleOnImage(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = bmp;
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            Point coordinates = me.Location;
            MessageBox.Show(coordinates.X.ToString() + "   " + coordinates.Y.ToString());
           
           
        }

        private void DrawAllRectanglesSelected()
        {
            for (int i = 0; i < indexPoint; i++)
            {
                bmp = Drawing.FillRectangleWithAColor(point[i], new Size(94, 32), bmp, Brushes.Red);
                
            }
            pictureBox1.Image = bmp;
        }
   

        private void button1_Click(object sender, EventArgs e)
        {
            curs = new Course();
            var form = new Insert_Course(curs);
            form.ShowDialog();
            selected_rectagle = new Rectangle(pointStart, new Size(endPoint));
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                graph.FillRectangle(Brushes.Red, selected_rectagle);
            }
            
            if (form.DialogResult == DialogResult.OK)
            {
                bmp = Drawing.DrawString(curs.Materia,pictureBox1.Image, new RectangleF(point[0], new SizeF(indexPoint * 94, indexPoint * 32)));
             
            }
            pictureBox1.Image = bmp;
            indexPoint = 0;

        
        }


    
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        
        private Point RectStartPoint;
        private Point pointStart;
        private Point endPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private bool isDrawing;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Rect.Contains(e.Location))
                {
                    Debug.WriteLine("Right click");
                }
            }
            isDrawing = false;
            endPoint = Drawing.SearchForMatch(Rect.Width + 94, Rect.Height + 32);

            using (Graphics graph = Graphics.FromImage(bmp))
            {
                graph.FillRectangle(selectionBrush, new Rectangle(pointStart, new Size(endPoint)));
            }
            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            Point tempEndPoint = e.Location;
            Rect.Location = new Point(
                Math.Min(RectStartPoint.X, tempEndPoint.X),
                Math.Min(RectStartPoint.Y, tempEndPoint.Y));
            Rect.Size = new Size(
                Math.Abs(RectStartPoint.X - tempEndPoint.X),
                Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            RectStartPoint = e.Location;
            isDrawing = true;
            indexPoint = 0;
            pointStart = Drawing.SearchForMatch(RectStartPoint.X, RectStartPoint.Y);
            point[indexPoint] = Drawing.SearchForMatch(RectStartPoint.X,RectStartPoint.Y);
            Invalidate();
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
            if (pictureBox1.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0 && isDrawing)
                {
                    Point point2 = Drawing.SearchForMatch(Rect.Size.Width + 94, Rect.Size.Height + 32);
                    Size size = new Size(point2);
                    selected_rectagle = new Rectangle(new Point(Rect.Location.X, Rect.Location.Y), new Size(Rect.Size.Width, Rect.Size.Height));
                     e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
          
        }
    }
}
