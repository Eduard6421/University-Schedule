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
            a.Save("heyy.xml");

        }
    }
    class Test_s
    {
        public string Name;
        public int Age;
        public void Save(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(Test_s));
                XML.Serialize(stream, this);
            }
        }
    }

}
