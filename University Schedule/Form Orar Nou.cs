﻿using System;
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
/// Singurul bug este atunci cand selectezi din nou nu dispare vechea selectare .
/// Rezolvare ( se salveaza imaginea veche atunci cand apesi pe mouse astfel incat daca se selecteaza
/// din nou sa se interschimbe) .
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
        
        private void DrawAllRectanglesSelected()
        {
            for (int i = 0; i < indexPoint; i++)
            {
                bmp = Drawing.FillRectangleWithAColor(point[i], new Size(94, 32), bmp, Brushes.Red);
                
            }
            pictureBox1.Image = bmp;
        }
   
        /// <summary>
        /// Aici la semnul => va trebui sa salvam datele pe care le baga atunci cand apasa 
        /// pe buton pentru algoritm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            curs = new Course();
            var form = new Insert_Course(curs);
            form.ShowDialog();
            selected_rectagle = new Rectangle(pointStart, new Size(endPoint));
        
            ///=>           
            if (!draw_sel)
            {
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    graph.FillRectangle(Brushes.Red, selected_rectagle);
                }
                if (form.DialogResult == DialogResult.OK)
                {
                    bmp = Drawing.DrawString(curs.Materia, pictureBox1.Image, selected_rectagle);
                }
                pictureBox1.Image = bmp;
            }
            else
            {
                DrawAllRectanglesSelected();
                indexPoint = 0;
                if (form.DialogResult == DialogResult.OK)
                {
                    bmp = Drawing.DrawString(curs.Materia, pictureBox1.Image, new RectangleF(point[0], new SizeF(indexPoint * 94, indexPoint * 32)));
                }
                pictureBox1.Image = bmp;
            }
            
            indexPoint = 0;
        }
    
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

         private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            Point coordinates = me.Location;
            if (clickOrSelected != "selected")
            {
                clickOrSelected = "click";
                point[indexPoint] = Drawing.SearchForMatch(coordinates.X, coordinates.Y);
                indexPoint++;
            }
          
        }

        private Point RectStartPoint;
        private Bitmap copy;
        private Point pointStart;
        private Point endPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        private bool isDrawing;
        private string clickOrSelected = string.Empty;
        private bool draw_sel;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                bmp = copy;
                return;
            }
            if (clickOrSelected == "selected")
            {
                isDrawing = false;
                pointStart = Drawing.SearchForMatch(Rect.X, Rect.Y);
                endPoint = Drawing.SearchForMatch(Rect.Size.Width + 94, Rect.Size.Height + 32);

                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    graph.FillRectangle(selectionBrush, new Rectangle(pointStart, new Size(endPoint)));
                }
                pictureBox1.Image = bmp;
                draw_sel = false;
            }
            else if (clickOrSelected == "click")
            {
                draw_sel = true;
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    graph.FillRectangle(selectionBrush, point[indexPoint-1].X,point[indexPoint-1].Y, 94,32);
                }
                pictureBox1.Image = bmp;
            }
            
            clickOrSelected = string.Empty;
        }
       

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            Point tempEndPoint = e.Location;
            Rect.Location = new Point(
                Math.Min(RectStartPoint.X, tempEndPoint.X),
                Math.Min(RectStartPoint.Y, tempEndPoint.Y));
            Rect.Size = new Size(
                Math.Abs(RectStartPoint.X - tempEndPoint.X),
                Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
            pictureBox1.Invalidate();
            if(Rect.Size.Width>=2 || Rect.Height >=2)
                clickOrSelected = "selected"; 
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            RectStartPoint = e.Location;
            isDrawing = true;
            selected_rectagle = new Rectangle();
            copy = bmp.Clone() as Bitmap;
            
            Invalidate();
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0 && isDrawing)
                {
                    selected_rectagle = Rect;
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
          
        }
    }
}
