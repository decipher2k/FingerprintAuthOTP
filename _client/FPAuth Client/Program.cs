using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPAuth_Client
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            bool hidden = false;
            if (arguments?.Contains("/taskon") == true)
            {
                TaskSched.enableTask();
                return;
            }
            else if (arguments?.Contains("/taskoff") == true)
            {
                TaskSched.disableTask();
                return;
            }
            else if (arguments?.Contains("/hidden") == true)
            {
                hidden = true;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain(hidden));
        }
    }
}
