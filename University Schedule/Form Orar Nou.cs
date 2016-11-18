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
/// Posibil inlocuirea array-ului Point si RectangleF cu 2 liste cu aceste date pentru 
///    manipularea mai buna in scrierea lor in bitmap.
/// </summary>

namespace University_Schedule
{
    public partial class Form_Orar_Nou : Form
    {
        public Course curs; //pentru stocarea datelor unei selectari ( trebui facut un vector sau introdus in baza de date).
        Bitmap bmp; //imaginea de manevra ... trebuie abdatat pictureboxul pentru a se vedea stuff-urile adaugate.
        RectangleF[] rec; //vector pentru stocarea dreptunghiurilor pentru scris ... pentru test este folosit cel dupa pozitia 0.
        Point[] point; // vector folosit pentru stocarea patratelor pentru colorat in diferite culori ... trebuie facuta o functie de generat culori random.
        int indexPoint; // indexul pentru cei 2 vectori
        Rectangle selected_rectagle; //pentru selectarea mai multor patratele

        public Form_Orar_Nou()
        {
            InitializeComponent();
            rec = new RectangleF[20]; //initializare
            point = new Point[20]; //initializare  ...test 20 se va mari mai tarziu
            indexPoint = 0; //initializare
            //se face imaginea initiala si se initializeaza pictureboxul
            bmp = Drawing.DrawRectangleOnImage(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = bmp;
        }
        /// <summary>
        /// Se stocheaza Point - urile pentru desenarea lor ulterioara... stocharea se face
        /// direct in dreptunghiurile corecte.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            Point coordinates = me.Location;
            MessageBox.Show(coordinates.X.ToString() + "   " + coordinates.Y.ToString());
           
            //se cauta pe baza coordonatelor unde sa dat click in care patrat se doreste colorarea
            //trebuie updatat cu patrate mai fine pentru cursurile cu ore impare precum si saptamani impare si pare.
            //point[indexPoint] = Drawing.SearchForMatch(coordinates.X, coordinates.Y);

            //Dreptunghiul unde se va scrie informatia
          // rec[indexPoint] = new RectangleF(point[indexPoint], new SizeF(94, 32));

         //   indexPoint++;
        }

        private void DrawAllRectanglesSelected()
        {
            // se deseneaza toate dreptunghiurile selectate pentru inserarea datelor.
            for (int i = 0; i < indexPoint; i++)
            {
                bmp = Drawing.FillRectangleWithAColor(point[i], new Size(94, 32), bmp, Brushes.Red);
            }
            //update ( nu e necesara decat pentru test ) 
            pictureBox1.Image = bmp;
        }
   

        /// <summary>
        /// In momentul in care apesi pe buton va scrie textul din Materie in imagine la inceputul ei.
        /// Trebuie lucrat si la incadrarea intr-un dreptunghi corespunzator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //trebuie modificat pentru toate datele precum si incadrarea lor in urmatoarele dreptunghiuri.
           // DrawAllRectanglesSelected();
            curs = new Course();
            var form = new Insert_Course(curs);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                ///Rectangle-ul in care scrie este facut din point[0] care e primul point verificat si ultimul 
                //// Trebuie facut in functia pentru SearchTypeCourse facut pentru fiecare in functie de curs
                bmp = Drawing.DrawString(curs.Materia,pictureBox1.Image, new RectangleF(point[0], new SizeF(indexPoint * 94, indexPoint * 32)));
            }
            pictureBox1.Image = bmp;
            indexPoint = 0;

        
        }


        //o sa ne trebuiasca pentru optimizare
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// Pentru selectarea unei parti din picture box tot ce e mai jos e necesar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Brush selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Rect.Contains(e.Location))
                {
                    Debug.WriteLine("Right click");
                }
            }
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
            // Determine the initial rectangle coordinates...
            RectStartPoint = e.Location;
            indexPoint = 0;
            point[indexPoint] = Drawing.SearchForMatch(RectStartPoint.X,RectStartPoint.Y);
            Invalidate();
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
            // Draw the rectangle...
            if (pictureBox1.Image != null)
            {
                if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                {
                    Point point1 = Drawing.SearchForMatch(Rect.Location.X, Rect.Location.Y);
                    Point point2 = Drawing.SearchForMatch(Rect.Size.Width + 94, Rect.Size.Height + 32);
                    Size size = new Size(point2);
                    selected_rectagle = new Rectangle(point1, size);
                     e.Graphics.FillRectangle(selectionBrush, selected_rectagle);

                    if (point1!=point[indexPoint])
                    {
                        indexPoint++;
                        point[indexPoint] = point1;
                    }

                }
            }
        }
    }
}
