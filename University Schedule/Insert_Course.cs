using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace University_Schedule
{
    public partial class Insert_Course : Form
    {


        
        public static Course curs;
        public Insert_Course(Course cours)
        {
            InitializeComponent();
            curs = cours;
   
            textBox2.Text = Form_Orar_Nou.data.Item1 + " - " + Form_Orar_Nou.data.Item2;


        }

        private void button1_Click(object sender, EventArgs e)
        {

           

            curs.access_materia = textBox1.Text;
            curs.access_ora = textBox2.Text;
            curs.access_profesor = textBox4.Text;
            curs.access_sala = textBox3.Text;
            curs.access_semigrupa = textBox5.Text;
            curs.access_zi = Form_Orar_Nou.day;
            


            Form_Orar_Nou.GetList().Add(curs);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }
















    }
}
