using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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


        
        public Course curs;
        public Insert_Course(Course cours)
        {
            InitializeComponent();
            curs = cours;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Course curs = new Course();


            curs.access_materia = textBox1.Text;
            curs.access_ora = textBox2.Text;
            curs.access_profesor = textBox4.Text;
            curs.access_sala = textBox3.Text;
            curs.acces_semigrupa = textBox5.Text;



            Form_Orar_Nou.GetList().Add(curs);


           // SaveXML.Save_Data(curs, "data.xml"); 


            this.DialogResult = DialogResult.OK;
            this.Close();
        }



    }
}
