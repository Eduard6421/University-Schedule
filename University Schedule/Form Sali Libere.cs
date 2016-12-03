﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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


        private void Find_FreeHours()
         {





         }
        public void Find_Class(string day)
        {

            int i, j;
            bool is_valid = false;

            for (i = 0; i < numar_sala.Length; ++i)
            {
                j = ora_inceput;
                is_valid = true;
                while (j <= ora_final && is_valid)
                {
                    if (sali[zi,i, j].is_empty == false)
                        is_valid = false;

                    ++j;

                }

                if (is_valid == true)
                {
                    ListViewItem temp = new ListViewItem(numar_sala[i].ToString());
                    temp.SubItems.Add(day);
                    temp.SubItems.Add(ora_inceput + " " + ora_final);
                    listView1.Items.Add(temp);

                }





                }

        }
        public void Is_Class_Free(string day,int sala,int ora_inceput, int ora_final)
        {
            bool is_valid = true;
            int i = ora_inceput;
            while(i <= ora_final && is_valid)
            {

                if (sali[zi, Array.IndexOf(numar_sala, sala), i].is_empty == false)
                is_valid = false;
                ++i;

            }

            if (is_valid)
            {
                ListViewItem temp = new ListViewItem(sala.ToString());
                temp.SubItems.Add(day);
                temp.SubItems.Add(ora_inceput + " - " + ora_final);
                listView1.Items.Add(temp);

            }
            else
            {
                ListViewItem temp = new ListViewItem(sala.ToString());
                temp.SubItems.Add("No Classroom Available");

            }





        }



        public void  Convert_Data(string day, string hour, string classroom)
        {
            switch (day)
            {
                case "Luni": zi = 0; break;
                case "Marti": zi = 1; break;
                case "Miercuri": zi = 2; break;
                case "Joi": zi = 3; break;
                case "Vineri": zi = 4; break;
                default: zi = -1; break;
            }

            sala = Int32.Parse(classroom);

            string[] parts = Regex.Split(hour,"- ");

            ora_inceput = Int32.Parse(parts[0]);
            ora_final = Int32.Parse(parts[1]);

            

        }


        public void Convert_Data1(string day, string hour_start, string hour_end, string classroom)
        {


            switch (day)
            {
                case "Luni": zi = 0; break;
                case "Marti": zi = 1; break;
                case "Miercuri": zi = 2; break;
                case "Joi": zi = 3; break;
                case "Vineri": zi = 4; break;
                default: zi = -1; break;
            }


            try
            {
                sala = int.Parse(classroom);

            }

            catch (System.FormatException)
            {
                Debug.WriteLine("Invalid Class");
                try
                {

                    ora_inceput = int.Parse(hour_start);
                    ora_final = int.Parse(hour_end);
                    Find_Class(day);
                    return;
                }

                catch
                {

                    Debug.WriteLine("Everything Wong");
                    return;

                }
                

            }

            try
            {

                ora_inceput = int.Parse(hour_start);
                ora_final = int.Parse(hour_end);
                Is_Class_Free(day,sala,ora_inceput,ora_final);
            }

            catch 
            {

                Debug.WriteLine("Invalid Hours");
                Find_FreeHours();
                return;


            }

            


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
                        if (temp >= 0)
                        {
                            for (k = ora_inceput; k < ora_final; ++k)
                            {
                                sali[zi, temp, k].is_empty = false;

                            }
                        }
                        else
                            Debug.WriteLine("Inexistent Class");

                    }
                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {


            foreach ( ListViewItem item  in listView1.Items)
            listView1.Items.Remove(item);

            Convert_Data1(textBox2.Text, maskedTextBox1.Text,maskedTextBox2.Text, maskedTextBox3.Text); // zi / inceput / final / sala


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
