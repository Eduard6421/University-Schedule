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

        private ClassRoom[,,] sali = new ClassRoom[5, 30, 24];
        private List<Course> cursuri = new List<Course>();
        int zi, ora_inceput,ora_final, sala;


        public Form_Sali_Libere()
        {
            InitializeComponent();
            Initialize_Matrix();
            Get_Schedule();

        }

        public void Initialize_Matrix()
        {

            int i, j, k;

            for (i = 0; i < 5; ++i)
                for (j = 0; j < 30; ++j)
                    for (k = 0; k < 24; ++k)
                        sali[i, j, k] = new ClassRoom();



        }

        public void  Convert_Data(string day, string hour, string classroom)
        {
            switch (day)
            {
                case "Luni": zi = 0    ;   break;
                case "Marti": zi = 1   ;   break;
                case "Miercuri": zi = 2;   break;
                case "Joi": zi = 3     ;   break;
                case "Vineri": zi = 4  ;   break;
                default: zi = -1;break;
            }

            sala = Int32.Parse(classroom);

            char c1, c2;

            c1 = hour[0];
            c2 = hour[1];

            if (c2 >= '0' && c2 <= '9')
            {
                ora_inceput = (int)Char.GetNumericValue(c1);
                ora_inceput = ora_inceput * 10;
                ora_inceput = ora_inceput + (int)Char.GetNumericValue(c2);
            }
            else
            { ora_inceput = (int)Char.GetNumericValue(c1);
            }

            c1 = hour[4];
            c2 = hour[5];


            if (c2 >= '0' && c2 <= '9' )
            {
                ora_final = (int)Char.GetNumericValue(c1);
                ora_final = ora_final * 10;
                ora_final = ora_final + (int)Char.GetNumericValue(c2);
            }
            else
                ora_final = (int)Char.GetNumericValue(c1);
            

        }

        private void  Get_Schedule()
        {
          
            int i,j,k;

            for (i = 0; i <= Form_Orar_Nou.valid_groups.Length - 1; ++i)
            {
                cursuri = SaveXML.Load_Data(cursuri, "grupa_" + Form_Orar_Nou.valid_groups[i] + ".xml");

                if (cursuri.Count > 0)
                {
                    for (j = 0; j < cursuri.Count; ++j)
                    {

                        Convert_Data(cursuri[j].access_zi, cursuri[j].access_ora, cursuri[j].access_sala);
                        int temp = Array.IndexOf(numar_sala, sala);
                        for (k=ora_inceput;k<ora_final;++k)
                                sali[zi, temp, k].is_empty = false;

                    }
                }

            }


        }



        private void button1_Click(object sender, EventArgs e)
        {
      
            Convert_Data(textBox2.Text, textBox3.Text, textBox1.Text);

            if (sali[zi, Array.IndexOf(numar_sala, sala), ora_inceput].is_empty == true)
                            Debug.WriteLine("am terminat");

      


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form test = new Form1();
            this.Hide();
            test.Closed += (s, args) => this.Close();
            test.Show();
        }
    }
}
