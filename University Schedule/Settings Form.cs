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
    public partial class Settings_Form : Form
    {
        public Settings_Form()
        {
            InitializeComponent();
        }

        //trebuie sa le fac permanente
        private void button1_Click(object sender, EventArgs e)
        {
            Form_Orar_Nou.groups.Add(Int32.Parse(textBox1.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new Form1();
            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form_Orar.profils.Add(textBox2.Text);
        }
    }
}
