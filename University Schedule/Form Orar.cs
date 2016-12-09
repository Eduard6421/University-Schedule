using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using University_Schedule.Properties;

namespace University_Schedule
{
    public partial class Form_Orar : Form
    {
        public static List<string> profils = new List<string>();
        private Bitmap img;
       
        /// <summary>
        /// profils si cu groups trebuie initializate cu iteme in main form ... 
        /// eroare daca nu se intra mai intai in form-nou unde se initializeaza in constructor
        /// da crash pentru ca profils este vid
        /// </summary>
        public Form_Orar()
        {
            profils.Add("MATE");
            profils.Add("INFO");
            profils.Add("CTI");
           
            InitializeComponent();
            for (int i = 0; i <= profils.Count - 1; ++i)
                comboBox2.Items.Add(profils[i]);

            for (int i = 0; i <= Form_Orar_Nou.groups.Count - 1; ++i)
                comboBox1.Items.Add(Form_Orar_Nou.groups[i]);

            comboBox1.Text =Form_Orar_Nou.groups[0].ToString();
            comboBox2.Text = profils[0];

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string group_number = comboBox1.Text;
            if (File.Exists("group_" + group_number + ".png"))
            {
                using (var bmpTemp = new Bitmap("group_" + group_number + ".png"))
                {
                    img = new Bitmap(bmpTemp);
                }
                img = Drawing.CombineImages(img,group_number,comboBox2.Text);
                pictureBox1.Image = img;
            }
            else
            {
                img = Resources.schema_orar;
                pictureBox1.Image = img;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string filename = sfd.FileName;
                img.Save(filename);
                label1.Text = "Status: File has been saved."; 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form test = new Form1();
            this.Hide();
            test.Closed += (s, args) => this.Close();
            test.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string group_number = comboBox1.Text;
            if (File.Exists("group_" + group_number + ".png"))
            {
                using (var bmpTemp = new Bitmap("group_" + group_number + ".png"))
                {
                    img = new Bitmap(bmpTemp);
                }
                img = Drawing.CombineImages(img, group_number, comboBox2.Text);
                pictureBox1.Image = img;
            }
            else
            {
                img = Resources.schema_orar;
                pictureBox1.Image = img;
            }
        }
    }
}
