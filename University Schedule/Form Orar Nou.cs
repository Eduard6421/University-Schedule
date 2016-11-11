using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Schedule
{
    public partial class Form_Orar_Nou : Form
    {
        public Form_Orar_Nou()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            Point coordinates = me.Location;
            MessageBox.Show(coordinates.X.ToString() + "   " + coordinates.Y.ToString());
            pictureBox1.Image = Drawing.DrawRectangleOnImage(pictureBox1.Size.Width,pictureBox1.Size.Height);
            Point p = Drawing.SearchForMatch(coordinates.X, coordinates.Y);
            pictureBox1.Image = Drawing.FillRectangleWithAColor(p, new Size(120, 90), pictureBox1.Image as Bitmap, Brushes.Red);
        }
    }
}
