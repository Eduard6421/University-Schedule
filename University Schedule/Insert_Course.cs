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
    public partial class Insert_Course : Form
    {

        Form_Orar_Nou form;
        public Course curs;
        public Insert_Course(Form_Orar_Nou f)
        {
            InitializeComponent();
            form = f;
            curs = new Course();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curs.Materia = textBox1.Text;
            curs.Ora = textBox2.Text;
            curs.Profesor = textBox4.Text;
            curs.Sala = textBox3.Text;
            curs.Semigrupa = textBox5.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

       
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
