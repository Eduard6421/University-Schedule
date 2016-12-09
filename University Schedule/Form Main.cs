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
    public partial class Form1 : Form
    {
        

        /// <summary>
        /// HIIII
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            pictureBox1.BackColor = Color.FromArgb(00, 33, 66);
        }

        /// <summary>
        /// Pentru a trece comanda la urmatorul form si a nu trebui sa dai stop mereu
        /// cand vrei sa inchizi aplicatia din IDE.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new Form_Orar_Nou();
            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form_Sali_Libere();
            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Form test = new Test();
            this.Hide();
            //test.Closed += (s, args) => this.Close();
           // test.Show();
        }
        protected override void OnLoad(EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form test = new Form_Orar();
            this.Hide();
            test.Closed += (s, args) => this.Close();
            test.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form seting = new Settings_Form();
            this.Hide();
            seting.Closed += (s, args) => this.Close();
            seting.Show();
        }
    }
}
