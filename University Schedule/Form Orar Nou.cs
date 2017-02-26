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
using System.Timers;
using System.Runtime.InteropServices;

/// <summary>
/// Daca nu se introduce data in insert_data tot coloreaza patratul selectat
/// </summary>

namespace University_Schedule
{
    public partial class Form_Orar_Nou : Form
    {
        public static int[] valid_groups = {101,102,103,104,105,131,132,133,134,135,141,142,143,144,151,152,153,154,201,
        211,212,221,222,231,232,233,234,235,241,242,243,244,251,252,253,254,301,311,321,331,332,333,334,341,
            342,343,344,351,352,353,354,451,452,453,454,405,406,407,408,501,503,505,506,507,508};
        public static List<int> groups = new List<int>();

        public Course curs; 

        private Bitmap bmp;
        private Bitmap copy;
        
        
        private Point pointStart    ;
        private Point endPoint      ;
        private Point RectStartPoint;

        private Rectangle Rect = new Rectangle();
        private Rectangle selected_rectagle;

        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
       

        private bool isDrawing         ;
        private bool isStarted  = false;
        private bool deselected = false;
        private bool isRight    = false;
        
    
        private string group_number;


        public static Tuple<int, int> data;
        public static string day;

        private Brush color_select = Brushes.Aquamarine;
        private Color color_select_forProf = Color.Aquamarine;

        private static List<Course> cursuri = new List<Course>();
        private List<string> profesori = new List<string>();
        int repetition = 0, i = 0;


        public Form_Orar_Nou()
        {
            InitializeComponent();
            //de test
            for (int i = 0; i < valid_groups.Length; i++)
                groups.Add(valid_groups[i]);

            for (int i=0; i<= groups.Count-1;++i)
                comboBox1.Items.Add(groups[i]);

            comboBox1.Text = "101";


            if (File.Exists("group_101.png"))
            {
                cursuri = SaveXML.Load_Data(cursuri, "grupa_101.xml");
                Bitmap img;
                using (var bmpTemp = new Bitmap("group_101.png"))
                {
                    img = new Bitmap(bmpTemp);
                }
                bmp = img;
                pictureBox1.Image = img;
                File.Delete("group_101.png");
            }
            else
            {
                bmp = Drawing.DrawRectangleOnImage(pictureBox1.Size.Width, pictureBox1.Size.Height);
                pictureBox1.Image = bmp;
            }

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = new Point(0, 0);


        }

      

     

        public static List<Course> GetList()
        {
            return cursuri;
        }

        public void GetDataCourse()
        {

            int start_hour;
            int end_hour;
            start_hour = selected_rectagle.X / 94                 ;
            end_hour = start_hour + (selected_rectagle.Width / 94);

            start_hour = start_hour + 8;
            end_hour = end_hour + 9;


            data = new Tuple<int, int>(start_hour, end_hour);


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

        public void Delete_Course()
        {
            Course temp = new Course();

            temp.access_zi = day;
            temp.access_ora = data.Item1 + " - " + data.Item2;


            for (int i = 0; i < cursuri.Count; ++i)
                if (cursuri[i].access_zi == day && cursuri[i].access_ora == data.Item1 + " - " + data.Item2)
                    break;

            if (i < cursuri.Count)
            {
                cursuri.RemoveAt(i);

            }

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                curs = new Course();
                var form = new Insert_Course(curs);
                
                try
                {
                    if (form.ShowDialog() == DialogResult.OK)
                        using (Graphics graph = Graphics.FromImage(bmp))
                        {
                            graph.FillRectangle(color_select, selected_rectagle);
                        }
                    else
                    {
                        bmp = copy;
                        pictureBox1.Image = copy;
                        isStarted = false;
                        isRight = true;
                        isDrawing = false;
                        deselected = false;
                    }

                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


                if (form.DialogResult == DialogResult.OK)
                {
                    bmp = Drawing.InsertDataInImage(bmp, color_select, selected_rectagle, curs);
                }
                pictureBox1.Image = bmp;

                deselected = false;
                isStarted = false;
                GetDataCourse();


              
            }
            else
                return;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
   
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                  if (isStarted && deselected)
                  {
                          bmp = copy;
                          pictureBox1.Image = copy;
                          isStarted = false;
                          isRight = true;
                          isDrawing = false;
                          deselected = false;
                    }
                
                return;
            }
            RectStartPoint = e.Location;
            
            if (isStarted && deselected)
            {
                bmp = copy;
                pictureBox1.Image = bmp;
                Rect = new Rectangle();
            }
            copy = bmp.Clone() as Bitmap;
            isStarted = true;
            isRight = false;
            isDrawing = true;
            deselected = false;
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
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0 && isDrawing && !isRight)
                {
                    selected_rectagle = Rect;
                    e.Graphics.FillRectangle(selectionBrush, Rect);
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            pointStart = Drawing.SearchForMatch(Rect.X, Rect.Y);
            endPoint = Drawing.SearchForMatch(Rect.Size.Width + 94, Rect.Size.Height + 32);
            selected_rectagle = new Rectangle(new Point(pointStart.X+1,pointStart.Y+1), new Size(endPoint.X-2,endPoint.Y-1));
         
            using (Graphics graph = Graphics.FromImage(bmp))
            {
               graph.FillRectangle(selectionBrush, selected_rectagle);
            }
            pictureBox1.Image = bmp;
          
            isDrawing = false;
            deselected = true;

            GetDataCourse();
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
                color_select_forProf = colorDlg.Color;
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

        private void button5_Click(object sender, EventArgs e)
        {
            bmp.Save("group_" + group_number + ".png");
            SaveXML.Save_Data(cursuri, "grupa_" + group_number + ".xml");

            Form form = new Form1();
            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Rectangle r = new Rectangle(selected_rectagle.X, selected_rectagle.Y, selected_rectagle.Width, selected_rectagle.Height);
                    Bitmap copyy = new Bitmap(Resources.copy);
                    Bitmap croppedImage = copyy.Clone(r, copyy.PixelFormat);

                    g.DrawImage(croppedImage, selected_rectagle);
                    pictureBox1.Image = bmp;
                    GetDataCourse();
                    Delete_Course();




                }
                isStarted = false;
                isRight = true;
                isDrawing = false;
                deselected = false;
            }
       }

        private void Form_Orar_Nou_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            cursuri = SaveXML.Delete_List(cursuri);
            SaveXML.Save_Data(cursuri, "grupa_" + group_number + ".xml");


            bmp = Drawing.DrawRectangleOnImage(pictureBox1.Size.Width, pictureBox1.Size.Height);
            bmp.Save("group_" + group_number + ".png");

            pictureBox1.Image = bmp;

            isStarted = false;
            isRight = true;
            isDrawing = false;
            deselected = false;

        }

    }

}
