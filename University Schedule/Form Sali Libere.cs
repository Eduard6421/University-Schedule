using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace University_Schedule
{
    public partial class Form_Sali_Libere : Form
    {



        private int[] numar_sala = { 1,3,5,8,9,10,12,17,28,117,118,120,121,122,201,202,204,213,214,215,216,
        217,218,219,220,221,302,303,305,310,317}; // sunt 31

        private string[] zile = { "Luni", "Marti", "Miercuri", "Joi", "Vineri" };

        private ClassRoom[,,] sali = new ClassRoom[5, 31, 24];
        private List<Course> cursuri = new List<Course>();
        int zi, ora_inceput,ora_final, sala;


 


        public Form_Sali_Libere()
        {
            InitializeComponent();
            Initialize_Matrix();
            Initialize_ComboBox();
            Get_Schedule();

     

        }



       
        public void Initialize_Matrix()
        {

            int i, j, k;

            for (i = 0; i < 5; ++i)
                for (j = 0; j < 31; ++j)
                    for (k = 0; k < 24; ++k)
                        sali[i, j, k] = new ClassRoom();



        }

        public void Initialize_ComboBox()
        {

            comboBox1.Items.Add("Luni");
            comboBox1.Items.Add("Marti");
            comboBox1.Items.Add("Miercuri");
            comboBox1.Items.Add("Joi");
            comboBox1.Items.Add("Vineri");
            comboBox1.Items.Add("");
            comboBox1.Text = "";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;


        }


        private void Find_FreeHours(string day,int sala)
         {

            int i;
            int j;
            bool is_valid = false;

            for(i=0;i<24;++i)
            {
                try
                {
                    if (sali[zi, Array.IndexOf(numar_sala, sala), i].is_empty == true && i > 7 && i < 22)
                    {
                        j = i + 1;
                        ListViewItem temp = new ListViewItem(sala.ToString());
                        temp.SubItems.Add(day);
                        temp.SubItems.Add(i + " - " + j);
                        listView1.Items.Add(temp);

                        is_valid = true;
                    }
                }
                catch
                {
                    Debug.WriteLine("Penus");
                    return;
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
                temp.SubItems.Add("Ocupat");
                listView1.Items.Add(temp);

            }





        }

        public void All_Free_CLasses()
        {
            int i, j, k;

            for (i = 0; i < 5; ++i)
            {
                for(j=0;j<31;++j)
                {
                    for (k = 0; k < 24; ++k)
                        if (sali[i, j, k].is_empty == true)
                        {

                            int l = k + 1;
                            ListViewItem temp = new ListViewItem(numar_sala[j].ToString());
                            temp.SubItems.Add(zile[i]);
                            temp.SubItems.Add(k + "-" + l);
                            listView1.Items.Add(temp);


                        }

                }

            }


        }
        public void Find_Class_In_Hours_And_Day(int zi,int hour_start, int hour_end)
        {
            int i, j, k;
            bool is_valid = true;

  
                for (j = 0; j < 31; ++j)
                {
                    is_valid = true;
                    for (k = hour_start; k < hour_end && is_valid == true; ++k)
                        if (sali[zi, j, k].is_empty != true)
                        {
                            is_valid = false;
                        }
                    if (is_valid)
                    {
                        ListViewItem temp = new ListViewItem(numar_sala[j].ToString());
                        temp.SubItems.Add(zile[zi]);
                        temp.SubItems.Add(hour_start + " - " + hour_end);
                        listView1.Items.Add(temp);

                    }

                }

            if (listView1.Items.Count == 0)
            {
                ListViewItem temp = new ListViewItem("Nu este nicio clasa valabila");
                listView1.Items.Add(temp);
            }



        }

        public void Find_Day_For_Class_And_Hours(int sala,int ora_inceput,int ora_final)
        {
            int i, k;
            bool is_valid;

            for (i = 0; i < 5; ++i)
            {
                    is_valid = true;
                for (k = ora_inceput; k <= ora_final && is_valid; ++k)
                    if (sali[i, Array.IndexOf(numar_sala,sala), k].is_empty == false)
                        is_valid = false;


                if (is_valid)
                {

                    ListViewItem temp = new ListViewItem(sala.ToString());
                    temp.SubItems.Add(zile[i]);
                    temp.SubItems.Add(ora_inceput + " - " + ora_final);
                    listView1.Items.Add(temp);

                }

                }

            if(listView1.Items.Count == 0)
            {
                ListViewItem temp = new ListViewItem("Nicio clasa nu este libera");
                listView1.Items.Add(temp);

            }

          }

        public void Show_Free_In_day(int zi)
        {
            int  i,j, k;
            for (i = 0; i < 31; ++i)
            {
                for (j = 0; j < 24; ++j)
                {
                    if (sali[zi, i, j].is_empty == true)
                    {
                        ListViewItem temp = new ListViewItem(numar_sala[i].ToString());
                        temp.SubItems.Add(zile[zi]);
                        k = j + 1;
                        temp.SubItems.Add(j + " - " + k);
                        listView1.Items.Add(temp);
                    }

                }
            }
            if (listView1.Items.Count == 0)
            {
                ListViewItem temp = new ListViewItem("Nicio clasa nu este libera");
                listView1.Items.Add(temp);

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

        public void Find_Class_No_Day(int ora_inceput,int  ora_final)
        {

            int i, j,k;
            bool is_valid = true;

            for (i = 0; i < 5; ++i)
            {

                for (j = 0; j < 32; ++j)
                {
                    is_valid = true;

                    for(k=ora_inceput;k<ora_final && is_valid;++k)
                    {
                        if (sali[i, j, k].is_empty == false)
                            is_valid = false;


                    }

                    if (is_valid == true)
                    {
                        ListViewItem temp = new ListViewItem(numar_sala[j].ToString());
                        temp.SubItems.Add(zile[i]);
                        temp.SubItems.Add(ora_inceput + " - " + ora_final);
                        listView1.Items.Add(temp);


                    }



                }


            }

            if (listView1.Items.Count == 0)
            {
                ListViewItem temp = new ListViewItem("Nicio clasa nu este libera");
                listView1.Items.Add(temp);


            }


        }
        public void Find_Day_And_Hours_For_Class(int sala)
        {

            int i, j,k;

            for (i = 0; i < 5; ++i)
            {
                for (j = 0; j < 24; ++j)
                {
                    if (sali[i, Array.IndexOf(numar_sala, sala), j].is_empty == true)
                    {

                        ListViewItem temp = new ListViewItem(sala.ToString());
                        temp.SubItems.Add(zile[i]);
                        k = j + 1;
                        temp.SubItems.Add(j + "-" + k);
                        listView1.Items.Add(temp);


                    }
                }
            }
           

                if (listView1.Items.Count == 0)
                {
                    ListViewItem temp = new ListViewItem("Nicio clasa nu este libera");
                    listView1.Items.Add(temp);


                }



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

            if (zi >= 0)
            {
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
                        if (ora_inceput <= ora_final && ora_final <24)
                        {
                            Find_Class_In_Hours_And_Day(zi, ora_inceput, ora_final);
                            Debug.Write("Gata");
                        }

                        else
                            MessageBox.Show("Incorect Time Input");
                        return;
                    }

                    catch
                    {

                        Debug.WriteLine("Everything Wong");
                        Show_Free_In_day(zi);
                        return;

                    }


                }

                try
                {

                    ora_inceput = int.Parse(hour_start);
                    ora_final = int.Parse(hour_end);
                    if (ora_inceput <= ora_final && ora_final < 24)
                        Is_Class_Free(day, sala, ora_inceput, ora_final);
                    else
                        MessageBox.Show("Incorect Time Input");
                    return;
                }

                catch
                {

                    Debug.WriteLine("Invalid Hours");
                    Find_FreeHours(day, sala);
                    return;


                }

            }



            // NO DAY-------------------


            else
            {
                try
                {
                    sala = int.Parse(classroom);
                    try
                    {
                        ora_inceput = int.Parse(hour_start);
                        ora_final = int.Parse(hour_end);
                        if (ora_inceput <= ora_final && ora_final < 24 )
                            Find_Day_For_Class_And_Hours(sala,ora_inceput,ora_final);
                        else
                            MessageBox.Show("Incorect Time Input");
                        return;

                    }

                    catch
                    {
                        Debug.WriteLine("No Day / No Hours");
                        Find_Day_And_Hours_For_Class(sala);
                        return;

                    }

                }
                catch 
                {
                    try
                    {
                        ora_inceput = int.Parse(hour_start);
                        ora_final = int.Parse(hour_end);
                        if (ora_inceput <= ora_final && ora_final <24)
                            Find_Class_No_Day(ora_inceput,ora_final);
                        else
                            MessageBox.Show("Incorect Time Input");
                        return;

                    }

                    catch
                    {
                        Debug.WriteLine("Everything Wong");
                        All_Free_CLasses();
                    
                        return;

                    }

              
           
                }

            }
       


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void  Get_Schedule()
        {
          
            int i,j,k;
         
            for (i = 0; i <= Form_Orar_Nou.groups.Count - 1; ++i)
            {
                cursuri = SaveXML.Load_Data(cursuri, "grupa_" + Form_Orar_Nou.groups[i] + ".xml");

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

            Convert_Data1(comboBox1.Text, maskedTextBox1.Text,maskedTextBox2.Text, maskedTextBox3.Text); // zi / inceput / final / sala


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
