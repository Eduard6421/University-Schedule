using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Schedule
{
     public class Course
    {

        private string profesor;
        private string sala;
        private string materia;
        private string ora;
        private string semigrupa;







        public string access_profesor {
            get { return profesor; }
            set { profesor = value; }
        }
        public string access_sala {
            get { return sala; }
            set { sala = value; }
        }
        public string access_materia {
            get { return materia; }
            set { materia = value; }
        }
        public string access_ora{
            get { return ora; }
            set { ora = value; }
        }
        public string acces_semigrupa {
            get { return semigrupa; }
            set { semigrupa = value; }
        }
    }
}
