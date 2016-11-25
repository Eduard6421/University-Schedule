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
using System.IO;
using University_Schedule.Properties;

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

        public Course curs; 

        private Bitmap bmp;
        private Bitmap copy;


        Point[] point;
        private int indexPoint;

        
        private Point pointStart    ;
        private Point endPoint      ;
        private Point RectStartPoint;

        private Rectangle Rect = new Rectangle();
        private Rectangle selected_rectagle;

        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        private bool isDrawing         ;
        private bool draw_sel          ;
        private bool isStarted  = false;
        private bool deselected = false;
        private bool isClick    = false;
        private bool isRight    = false;
        private bool first_click = true;

        private string clickOrSelected = string.Empty;
    
        private string group_number;


        private static Tuple<int, int> data;
        private static string day;

        private Brush color_select = Brushes.Aquamarine;
        private static List<Course> cursuri = new List<Course>();


        public Form_Orar_Nou()
        {
            InitializeComponent();
            point = new Point[20]; 
            indexPoint = 0; 
            pictureBox1.Image = bmp;

            for(int i=0; i<= valid_groups.Length-1;++i)
                comboBox1.Items.Add(valid_groups[i]);

            comboBox1.Text = "101";

        }


        public static List<Course> GetList()
        {
            return cursuri;
        }

        public void GetDataCourse()
        {
            switch(selected_rectagle.X / 188 + 1)
            {
                case 1:
                    data = new Tuple<int, int>(8, 10);
                    break;
                case 2:
                    data = new Tuple<int, int>(10, 12);
                    break;
                case 3:
                    data = new Tuple<int, int>(12, 14);
                    break;
                case 4:
                    data = new Tuple<int, int>(14, 16);
                    break;
                case 5:
                    data = new Tuple<int, int>(16, 18);
                    break;
                case 6:
                    data = new Tuple<int, int>(18, 20);
                    break;
                default:
                    data = new Tuple<int, int>(0, 0);
                    break;
            }
            switch (selected_rectagle.Y / 128 + 1)
            {
                case 1:
                    day = "Luni";
                    break;
                case 2:
                    day = "Marti";
                    break;
                case 3:
                    day = "Miercuri";
                    break;
                case 4:
                    day = "Joi";
                    break;
                case 5:
                    day = "Vineri";
                    break;
              
                default:
                    day = "Error";
                    break;
            }
        }
        
        private void DrawAllRectanglesSelected()
        {
            for (int i = 0; i < indexPoint; i++)
            {
                bmp = Drawing.FillRectangleWithAColor(point[i], new Size(94, 32), bmp, color_select);
                
            }
            pictureBox1.Image = bmp;
        }

        private Point GetMin_inArray()
        {
            int min_x = point[0].X;
            int min_y = point[0].Y;
            for (int i = 1; i < indexPoint; i++)
            {
                min_x = Math.Min(min_x, point[i].X);
                min_y = Math.Min(min_y, point[i].Y);
            }
            return (new Point(min_x, min_y));
        }

        private Size GetMax_sizeArray()
        {
            return (new Size((indexPoint-2) * 94, (indexPoint-2) * 32));
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

            if (!isRight || indexPoint > 0)
            {
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
                    selected_rectagle = new Rectangle(GetMin_inArray(), GetMax_sizeArray());
                    using (Graphics graph = Graphics.FromImage(bmp))
                    {
                        graph.FillRectangle(color_select, selected_rectagle);
                    }
                    indexPoint = 0;
                    if (form.DialogResult == DialogResult.OK)
                    {
                        bmp = Drawing.InsertDataInImage(bmp, color_select, selected_rectagle, curs);
                    }
                    pictureBox1.Image = bmp;
                }
            }
            isRight = false;
            isStarted = false;
            indexPoint = 0;
            GetDataCourse();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            Point coordinates = me.Location;
            if (me.Button == MouseButtons.Right)
            {
                return;
            }
            if (clickOrSelected != "selected")
            {
                clickOrSelected = "click";
                point[indexPoint] = Drawing.SearchForMatch(coordinates.X, coordinates.Y);
                indexPoint++;
                isClick = true;
            }


        }
   
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Right && !first_click)
            {
                if (pictureBox1.Image != copy)
                   cursuri = SaveXML.Delete_Last_Entry(cursuri);

                bmp = copy;
                pictureBox1.Image = copy;


                isRight = true;
                if (indexPoint == 0)
                {
                    selected_rectagle = new Rectangle();

                }
                else
                {
                    indexPoint--;

                }




                return;
            }

            isRight = false;

           /* if (isStarted == true && deselected)
            {
                bmp = copy;
                pictureBox1.Image = copy;
            }*/

            isStarted = true;
            copy = bmp.Clone() as Bitmap;

            RectStartPoint = e.Location;
            isDrawing = true;
            selected_rectagle = new Rectangle();

            first_click = false;

            Invalidate();
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

            if (Rect.Size.Width >= 2 || Rect.Height >= 2)
            {
                clickOrSelected = "selected";
                isClick = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0 && isDrawing && !isClick)
                {
                    selected_rectagle = Rect;
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                return;//sunt contrabandist
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
                    graph.FillRectangle(selectionBrush, point[indexPoint - 1].X, point[indexPoint - 1].Y, 94, 32);
                }
                pictureBox1.Image = bmp;
            }
            clickOrSelected = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SaveXML.Save_Data(cursuri, "grupa_" + group_number +".xml");

            bmp.Save("group_" + group_number + ".png");
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

            cursuri = SaveXML.Load_Data(cursuri, "grupa_" + group_number + ".xml");

            if (File.Exists("group_" + group_number + ".png"))
            {

                Bitmap img;
                using (var bmpTemp = new Bitmap("group_" + group_number + ".png"))
                {
                    img = new Bitmap(bmpTemp);
                }
                bmp = img;
                pictureBox1.Image = img;
                File.Delete("group_" + group_number + ".png");
            }
            else
            {
                bmp = Drawing.DrawRectangleOnImage(pictureBox1.Size.Width, pictureBox1.Size.Height);
                pictureBox1.Image = bmp;
            }


            bmp.Save("group_" + group_number + ".png");


        }

    }

}
