using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPAuth_Client
{
    public partial class frmLicense : Form
    {
        public frmLicense()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Thank you.", "Licensing");
            if (File.Exists("license.lic"))
                File.Delete("license.lic");
            File.WriteAllText("license.lic", textBox1.Text + "\r\n" + textBox2.Text);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
