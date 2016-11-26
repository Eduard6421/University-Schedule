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

namespace University_Schedule
{
    public partial class Form_Sali_Libere : Form
    {



        private int[] numar_sala = { 1,3,5,8,9,10,12,17,28,118,120,121,122,201,202,204,213,214,215,216,
        217,218,219,220,221,302,303,305,310,317};

        private ClassRoom[,,] sali = new ClassRoom[5, 12,30];
        private List<Course> cursuri;





        public Form_Sali_Libere()
        {
            InitializeComponent();
            Get_Schedule();




        }



        private void  Get_Schedule()
        {

            int zi, ora, sala;
            int i,j;

            for (i = 0; i <= Form_Orar_Nou.valid_groups.Length - 1; ++i)
            {
                cursuri = SaveXML.Load_Data(cursuri, "grupa" + Form_Orar_Nou.valid_groups[i] + ".xml");

                if (cursuri.Count > 0)
                {
                    for (j = 0; j < cursuri.Count - 1; ++j)
                    {
                        zi = Convert.ToInt32(cursuri[i].access_zi);
                        ora = Convert.ToInt32(cursuri[i].access_ora);
                        sala = Convert.ToInt32(cursuri[i].access_sala);

                        sali[zi, ora, sala].is_empty = true;

                    }
                }



            }


        }





        private void button1_Click(object sender, EventArgs e)
        {

    

        }
    }
}
