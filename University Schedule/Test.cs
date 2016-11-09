using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace University_Schedule
{

  

    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
            Test_s a = new Test_s();
            a.Name = "Bob";
            a.Age = 33;
            a.XmlWriter("heyy.xml");

            Test_s b = new Test_s();
            b.Name = "Adam";
            b.Age = 23;
            b.XmlWriter("heyy.xml");
        }
    }
   

}
