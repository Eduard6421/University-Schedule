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
/// Daca nu se introduce data in insert_data tot coloreaza patratul selectat
/// </summary>

namespace University_Schedule
{
    public partial class Form_Orar_Nou : Form
    {
       int[] valid_groups = {101,102,103,104,105,131,132,133,134,135,141,142,143,144,151,152,153,154,201,
        211,212,221,222,231,232,233,234,235,241,242,243,244,251,252,253,254,301,311,321,331,332,333,334,341,
            342,343,344,351,352,353,354,451,452,453,454,405,406,407,408,501,503,505,506,507,508};

        private Brush color_select = Brushes.Aquamarine;
        private static List<Course> cursuri = new List<Course>();
        string group_number;



        public static List<Course> GetList()
        {
            return cursuri;
        }

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

            for(int i=0; i<= valid_groups.Length-1;++i)
                comboBox1.Items.Add(valid_groups[i]);


        }
        
        private void DrawAllRectanglesSelected()
        {
            for (int i = 0; i < indexPoint; i++)
            {
                bmp = Drawing.FillRectangleWithAColor(point[i], new Size(94, 32), bmp, color_select);
                
            }
            pictureBox1.Image = bmp;
        }

        private Point GetMin_possition()
        {
            int min_x = point[0].X;
            int min_y = point[0].Y;
            for (int i = 1; i < indexPoint; i++)
            {
                min_x = Math.Min(min_x, point[i].X);
                min_y = Math.Min(min_y, point[i].Y);
            }

            return (new Point(Math.Min(min_x, selected_rectagle.X), Math.Min(min_y, selected_rectagle.Y)));
        }

        private Size GetMax_size()
        {
            int max_x;
            int max_y;
            if (selected_rectagle.Width == (indexPoint) * 94)
            {
                max_x = selected_rectagle.Width;
                max_y = (indexPoint - 1) * 32 + selected_rectagle.Height;
            }
            else
            {
                max_x = selected_rectagle.Width + (indexPoint - 1) * 94;
                max_y = selected_rectagle.Height;
            }
            return (new Size(max_x, max_y));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            curs = new Course();
            var form = new Insert_Course(curs);
            form.ShowDialog();
            selected_rectagle = new Rectangle(pointStart, new Size(endPoint));

            if (!draw_sel)
            {
                if (indexPoint > 0)
                {
                    Point st = GetMin_possition();
                    Size sz = GetMax_size();
                    selected_rectagle = new Rectangle(st, sz);
                    using (Graphics graph = Graphics.FromImage(bmp))
                    {
                        graph.FillRectangle(color_select, selected_rectagle);
                    }
                    indexPoint = 0;
                }
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    graph.FillRectangle(color_select, selected_rectagle);
                }
                if (form.DialogResult == DialogResult.OK)
                {
                    // bmp = Drawing.DrawString(curs.access_materia, pictureBox1.Image, selected_rectagle);
                    bmp = Drawing.InsertDataInImage(bmp, color_select, selected_rectagle, curs);
                }
                pictureBox1.Image = bmp;
            }
            else
            {
                DrawAllRectanglesSelected();
                indexPoint = 0;
                if (form.DialogResult == DialogResult.OK)
                {
                    bmp = Drawing.DrawString(curs.access_materia, pictureBox1.Image, new RectangleF(point[0], new SizeF(indexPoint  +94, indexPoint  +32)));
                }
                pictureBox1.Image = bmp;
            }
            isStarted = false;
            indexPoint = 0;
            //bmp.Save("group_" + group_number + ".png");
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
                deselected = true;
            }
            else if (clickOrSelected == "click")
            {
                deselected = false;
                draw_sel = true;
                using (Graphics graph = Graphics.FromImage(bmp))
                {
                    graph.FillRectangle(selectionBrush, point[indexPoint-1].X,point[indexPoint-1].Y, 94,32);
                }
                pictureBox1.Image = bmp;
            }
            clickOrSelected = string.Empty;
        }
        bool isStarted = false;
        bool deselected = false;
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
            if (Rect.Size.Width >= 2 || Rect.Height >= 2)
            {
                clickOrSelected = "selected";
            }
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(isStarted == true && deselected)
            {
                bmp = copy;
                pictureBox1.Image = bmp;
            }
            isStarted = true;
            copy = bmp.Clone() as Bitmap;

            RectStartPoint = e.Location;
            isDrawing = true;
            selected_rectagle = new Rectangle();
            
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

        private void button2_Click(object sender, EventArgs e)
        {

            SaveXML.Save_Data(cursuri, "grupa_" + group_number +".xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                color_select = new SolidBrush(colorDlg.Color);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            group_number = comboBox1.Text;
        }
    }
}
