using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                try
                {
                    
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\Credential Providers\", true);
                    try
                    {
                        key = key.CreateSubKey("{f264df76-2c20-4884-8f05-7b75bb455b35}", true);
                        key.SetValue("@", "Credential Provider.NET");
                    }
                    catch (Exception ex) { }                    
                }
                catch (Exception ex) { }
                return;
            }
            else if (arguments?.Contains("/taskoff") == true)
            {
                TaskSched.disableTask();
                try
                {
                    Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\Credential Providers\{f264df76-2c20-4884-8f05-7b75bb455b35}", false);
                }
                catch (Exception ex) { }
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
