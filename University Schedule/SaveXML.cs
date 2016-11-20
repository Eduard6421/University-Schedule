using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace University_Schedule
{
    public class SaveXML
    {


        public static void Save_Data(List<Course>  obj, string filename)
        {
            XmlSerializer sr = new XmlSerializer(obj.GetType());
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StreamWriter(filename);
            sr.Serialize(writer, obj);
            writer.Close();

        }





    }
}
