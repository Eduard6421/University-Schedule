using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

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
        }

        public static List<Course> Load_Data(List<Course> obj, string filename)
        {
            XmlSerializer sr = new XmlSerializer(obj.GetType());
       
            if (File.Exists(filename))
            {
                
                FileStream read = new FileStream(filename, FileMode.Open,FileAccess.Read,FileShare.Read);
                List<Course> lista = (List<Course>)sr.Deserialize(read);
                read.Close();
                return lista;

            }

            List<Course> lista1 = new List<Course>() ;
            return lista1;

    

        }

        public static List<Course> Delete_Last_Entry(List<Course> obj)
        {   if(obj.Count > 0)
            obj.RemoveRange(obj.Count - 1, 1);
            return obj;
        }

        public static List<Course> Delete_List(List<Course> obj)
        {

            obj.RemoveRange(0, obj.Count);

            return obj;


        }




    }
}
