using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPAuth_Client
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                WebClient wc = new WebClient();
                String ret = wc.DownloadString("https://fpauth.h2x.us/api/Session/Register?username=" + txtUsername.Text + "&password=" + txtPassword.Text);
                if (ret.Contains("AUTH"))
                    this.Close();
                else
                    MessageBox.Show("Error registering user.");
            }
        }
    }
}
