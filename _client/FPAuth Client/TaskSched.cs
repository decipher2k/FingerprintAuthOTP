using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace FPAuth_Client
{
    public static class TaskSched
    {
        public static void enableTask()
        {
            bool found = false;
            foreach (Microsoft.Win32.TaskScheduler.Task t in TaskService.Instance.AllTasks)
            {
                if (t.Name == "FPAuth")
                {
                    t.Enabled = true;
                    found = true;
                }
            }
            if (!found)
            {

                String username = Environment.GetEnvironmentVariable("USERNAME");
                    TaskService.Instance.AddTask("FPAuth", QuickTriggerType.Logon,
                        AppDomain.CurrentDomain.BaseDirectory + "\\" +
                    System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName, "/hidden", logonType: TaskLogonType.InteractiveToken);
            }
        }

        public static void disableTask()
        {
            foreach (Microsoft.Win32.TaskScheduler.Task t in TaskService.Instance.AllTasks)
            {
                if (t.Name == "FPAuth")
                    t.Enabled = false;
            }
       
        }

        public static bool isTaskEnabled()
        {
            bool found = false;
            foreach (Microsoft.Win32.TaskScheduler.Task t in TaskService.Instance.AllTasks)
            {
                if (t.Name == "FPAuth")
                {          
                    if(t.Enabled)
                        found = true;
                }
            }
            return found;
        }
    }
}
