using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace University_Schedule
{
    static class Program
    {
        /// <summary>
      //aici este comentariul meu. Sper sa il gasesti!
      //ce faci bosulica bine: )))
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
