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
    public partial class frmAdd2FA : Form
    {
        String username;
        String password;
        public frmAdd2FA(String username, String password)
        {
            this.username = username;
            this.password = password;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtName.Text!="" && txtSeed.Text!="")
            {
                WebClient wc = new WebClient();
                String ret = wc.DownloadString("https://fpauth.h2x.us/api/Session/AddSeed?username=" +  username+ "&password=" + password+"&seed="+txtSeed.Text+"&name="+txtName.Text);
                if (ret.Contains("AUTH"))
                    this.Close();
                else
                    MessageBox.Show("Error creating entry.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
