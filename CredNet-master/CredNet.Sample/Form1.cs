using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CredNet.Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static void StartShutDown(string param)
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.FileName = "cmd";
            proc.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Arguments = "/C shutdown " + param;
            Process.Start(proc);
        }
        public static void Restart()
        {
            StartShutDown("-f -r -t 1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && maskedTextBox1.Text != "")
            {
                WebClient wclient = new WebClient();
                String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/DoLogin?username=" + textBox1.Text + "&password=" + maskedTextBox1.Text);


                if (pass.Length > 0)
                {
                    
                        Credentials.SavePassword(pass, textBox1.Text);
                        MessageBox.Show(this, "Done. Restarting.");
                        Restart();                    
                }
                else
                {
                    MessageBox.Show(this, "Invalid user data.");
                }

            }
            else
            {
                MessageBox.Show(this, "Please enter user data.");
            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
