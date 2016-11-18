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
    public struct newStruct
    {
        public string name;
        public int data;
        public newStruct(string n,int d)
        {
            name = n;
            data = d;
        }
    }
    public class Test_s
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public newStruct newstruct { get; set; }
       
    }


}
