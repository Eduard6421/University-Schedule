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
            writer.Dispose();
            //med
        }

        public static List<Course> Load_Data(List<Course> obj, string filename)
        {

            if (File.Exists(filename))
            {
                XmlSerializer sr = new XmlSerializer(obj.GetType());
                FileStream read = new FileStream(filename, FileMode.Open,FileAccess.Read,FileShare.Read);
                List<Course> lista = (List<Course>)sr.Deserialize(read);
                read.Close();
                return lista;

            }

            List<Course> lista1 = new List<Course>() ;
            return lista1;

    

        }

        public static List<Course> Delete_Last_Entry(List<Course> obj)
        {
            obj.RemoveRange(obj.Count - 1, 1);
            return obj;




        }





    }
}
