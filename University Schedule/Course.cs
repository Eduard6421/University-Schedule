using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Schedule
{
    public class Course
    {

        private string profesor = string.Empty;
        private string sala = string.Empty;
        private string materia = string.Empty;
        private string ora = string.Empty;
        private string semigrupa = string.Empty;
        private string zi = string.Empty;

        public string access_profesor
        {
            get { return profesor; }
            set { profesor = value; }
        }
        public string access_sala
        {
            get { return sala; }
            set { sala = value; }
        }
        public string access_materia
        {
            get { return materia; }
            set { materia = value; }
        }
        public string access_ora
        {
            get { return ora; }
            set { ora = value; }
        }
        public string access_semigrupa
        {
            get { return semigrupa; }
            set { semigrupa = value; }
        }

        public string access_zi
        { get { return zi; }
          set { zi = value; }
        }


    }
}
