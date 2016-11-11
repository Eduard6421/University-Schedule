using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            point[indexPoint] = Drawing.SearchForMatch(coordinates.X, coordinates.Y);

            //Dreptunghiul unde se va scrie informatia
            rec[indexPoint] = new RectangleF(point[indexPoint], new SizeF(60, 22));

            indexPoint++;
        }

        private void DrawAllRectanglesSelected()
        {
            // se deseneaza toate dreptunghiurile selectate pentru inserarea datelor.
            for (int i = 0; i < indexPoint; i++)
            {
                bmp = Drawing.FillRectangleWithAColor(point[i], new Size(60, 22), bmp, Brushes.Red);
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
            DrawAllRectanglesSelected();
            curs = new Course();
            var form = new Insert_Course(curs);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                 bmp = Drawing.DrawString(curs.Materia,pictureBox1.Image,rec[indexPoint-1]);
            }
            pictureBox1.Image = bmp;
            indexPoint = 0;
        }


        //o sa ne trebuiasca pentru optimizare
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
