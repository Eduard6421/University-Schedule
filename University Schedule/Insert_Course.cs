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
            if (textBox4.Text == "")
            {
                MessageBox.Show("Nu a fost introdusa materia");
                this.DialogResult = DialogResult.Cancel;
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Nu a fost selectata nicio sala.");
                this.DialogResult = DialogResult.Cancel;
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Nu a fost introdus niciun profesor");
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                string[] cifre = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
                string[] litere = { "a", "b", "c", "d", "e", "f", "g", "h", "l", "m", "n", "o", "p", "q"
                                ,"r","s","t","u","w","x","y","z"};
                try
                {

                    if (Drawing.ContainsAny(textBox4.Text, cifre))
                        throw new Exception("Profesor nu poate contine cifre.");
                    if (Drawing.ContainsAny(textBox3.Text, litere))
                        throw new Exception("Sala nu poate contine litere");
                    if (Drawing.ContainsAny(textBox5.Text, litere))
                        throw new Exception("Semigrupa poate contine doar o cifra");

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return;
                }

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

    }
