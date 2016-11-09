using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace University_Schedule
{
    public class Test_s
    {
        public string Name { get; set; }
        public int Age { get; set; }
      
        public void XmlWriter(string filename)
        {
            #region On Error Retry and retrycount with xml file
            
            try
            {
                if (!File.Exists(filename))
                {
                    XmlSerializer xSeriz = new XmlSerializer(typeof(Test_s));
                    FileStream fs = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    xSeriz.Serialize(fs, this);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);

                    XmlNode xnode = doc.CreateNode(XmlNodeType.Element, "New", null);
                    XmlSerializer xSeriz = new XmlSerializer(typeof(Test_s));
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    XmlWriterSettings writtersetting = new XmlWriterSettings();
                    writtersetting.OmitXmlDeclaration = true;
                    StringWriter stringwriter = new StringWriter();
                    using (XmlWriter xmlwriter = System.Xml.XmlWriter.Create(stringwriter, writtersetting))
                    {
                        xSeriz.Serialize(xmlwriter, this, ns);
                    }
                    xnode.InnerXml = stringwriter.ToString();
                    XmlNode bindxnode = xnode.SelectSingleNode("Test_s");
                    doc.DocumentElement.AppendChild(bindxnode);
    
                
                    doc.Save(filename);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }
    }


}
