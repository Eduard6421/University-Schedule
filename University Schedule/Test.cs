using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace University_Schedule
{
    /// <summary>
    /// Trebuie o clasa Orar care sa cuprinda - zile care zile cuprind cursurile pe ore
    /// 
    /// Orar -> Zile(5) ( Luni,Marti,Miercuri,Joi,Vineri) -> Fiecare contin Cursuri-> Fiecare Curs contine
    ///                                 (Materia,Nume Prof,Sala,Semigrupa(poate sa fie omisa),si orele dupa care vor fi sortate cand sunt introduse in zi)
    ///       !!! Astfel in Zile Cursurile vor fi in ordine gen curs ora 8-10 urmat de Curs ora 14-16 daca nu exista nimic intre ele.
    /// </summary>
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();



            //Test_s a = new Test_s();
            //a.Name = "Braien";
            //a.Age = 24;
            //a.newstruct = new newStruct("dd",3);

            //Test_s b = new Test_s();
            //b.Name = "Adam";
            //b.Age = 23;

            ////SerializeToXmlFile(a, "new.xml", typeof(Test_s));
            ////SerializeToXmlFile(b, "new.xml", typeof(Test_s));
            //List<Test_s> l = new List<Test_s>();
            //l.Add(a);
            //l.Add(b);
            //SerializeToXmlFile(l, "new.xml");


            DeserializeToXmlFile();
        }

        public static void SerializeToXmlFile(object obj,string file,Type type)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(type);

            using (var textWriter = new StreamWriter(file))
            {
                xmlSerializer.Serialize(textWriter, obj);
            }
        }
        public static void SerializeToXmlFile(List<Test_s> list,string filename)
        {
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (TextWriter writer = new StreamWriter(filename))
            {
                serializer.Serialize(writer, list);
            }
        }
        [XmlRoot("StepList")]
        public class StepList
        {
            [XmlElement("Step")]
            public List<Step> Steps { get; set; }
        }

        public class Step
        {
            [XmlElement("Name")]
            public string Name { get; set; }
            [XmlElement("Desc")]
            public string Desc { get; set; }
        }
        public static void DeserializeToXmlFile()
        {
            StepList result;
            string testData = @"<StepList>
                        <Step>
                            <Name>Name1</Name>
                            <Desc>Desc1</Desc>
                        </Step>
                        <Step>
                            <Name>Name2</Name>
                            <Desc>Desc2</Desc>
                        </Step>
                    </StepList>";

            XmlSerializer serializer = new XmlSerializer(typeof(StepList));
            using (TextReader reader = new StringReader(testData))
            {
                 result = (StepList)serializer.Deserialize(reader);
            }
            foreach(var i in result.Steps)
            {
                MessageBox.Show(i.Name+ "   "+ i.Desc);
            }
        }

    }
   

}
