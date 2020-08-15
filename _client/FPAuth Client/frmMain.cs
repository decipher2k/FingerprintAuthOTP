using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;
using System.Security.Cryptography;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using Action = System.Action;
using System.Reflection;
using System.Diagnostics;

namespace FPAuth_Client
{
    public partial class frmMain : Form
    {     
        private String Username = "";
        private String Password = "";
        private String Masterpass="";
        private bool PressEnter = false;
        private bool isLoggedIn = false;

        public frmMain(bool hidden)
        {
            InitializeComponent();
            backgroundWorker.RunWorkerAsync();
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Heine\FPLogin");
                if (key != null && key.GetValue("Username")!=null)
                {
                    Username = key.GetValue("Username").ToString();
                    txtUsername.Text = Username;
                    Password = Credentials.GetPassword();
                    if (Password != null && Password != "")
                    {
                        txtPassword.Text = Password;
                        doLogin();
                        cbAutostart.Checked = TaskSched.isTaskEnabled();
                        if(key.GetValue("PressEnter") != null)
                        {
                            cbPressEnter.Checked=key.GetValue("PressEnter").ToString() =="true";
                        }
                    }
                }
            }
            catch (Exception ex) { }
            if (hidden)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
                
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                WebClient wclient = new WebClient();
                try
                {
                    String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/GetMasterPass?username="+Username+"&password="+Password);
                    if (pass.Length > 0)
                    {
                     
                            SendKeys(pass);
                            if(cbPressEnter.Checked)
                                SendEnterKey();
                        
                    }
                }
                catch (Exception ex) { }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void bnLogin_Click(object sender, EventArgs e)
        {
            if (isLoggedIn)
                doLogout();
            else
                doLogin();
        }

        private void doLogout()
        {
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            grpSettings.Enabled = false;
            bnLogin.Text = "Login";
            isLoggedIn = false;
        }
        private void bnSetMasterpass_Click(object sender, EventArgs e)
        {
//            setMasterpass();
        }
        private void doLogin()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show(this, "Please enter user data.\nYou can register using the Android app.", "Error");
            }
            else
            {
                WebClient wclient = new WebClient();
                String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/DoLogin?username=" + txtUsername.Text + "&password=" + txtPassword.Text);
                try
                {
                    
                    if (pass.Length > 0)
                    {
                        if (pass.Contains("AUTH"))
                        {
                            Username = txtUsername.Text;
                            Password = txtPassword.Text;
                            try
                            {
                                RegistryKey mkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Heine");
                                mkey.CreateSubKey("FPLogin");
                            }
                            catch (Exception ex) { }
                            try
                            {
                                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Heine\FPLogin", true);
                                key.SetValue("Username", Username);
                            }
                            catch (Exception ex) { }

                            try
                            {
                              
                            }
                            catch (Exception ex) { }
                            Credentials.SavePassword(Password);
                           
                            txtUsername.Enabled = false;
                            txtPassword.Enabled = false;
                            isLoggedIn = true;
                            grpSettings.Enabled = true;
                            bnLogin.Text = "Logout";
                        }
                        else
                        {
                            MessageBox.Show(this, "Login Error.\nYou can register using the Android app.", "Error");
                        }

                    }
                }
                catch (Exception ex) { }


            }
        }
        
        
        private void SendKeys(String text)
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.TextEntry(text);
        }

        private void SendEnterKey()
        {
            InputSimulator sim = new InputSimulator();
            sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            PeanutButter.TrayIcon.TrayIcon i = new PeanutButter.TrayIcon.TrayIcon(AppDomain.CurrentDomain.BaseDirectory + "\\" +
                    "-fingerprint_90738.ico");
            i.AddMenuItem("Show", new Action(ShowWindow));
            i.AddMenuItem("Exit", new Action(ExitApp));
            i.AddMouseClickHandler(PeanutButter.TrayIcon.MouseClicks.Double, MouseButtons.Left, new Action(ShowWindow));

            i.Show();
        }

        private void ShowWindow()
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void ExitApp()
        {
            Application.Exit();
        }

        private void cbAutostart_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAutostart.Checked)
            {
                RunAsAdmin("/taskon");
            }
            else
            {
                RunAsAdmin("/taskoff");
            }
        }

        private static void RunAsAdmin(String parameter)
        {
            var path = Assembly.GetExecutingAssembly().Location;
            using (var process = Process.Start(new ProcessStartInfo(path,parameter)
            {
                Verb = "runas"
            }))
            {
                process?.WaitForExit();
            }
        }

        private void cbPressEnter_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Heine\FPLogin", true);
            if (key != null)
            {
                if (cbPressEnter.Checked)
                {
                    key.SetValue("PressEnter", "true");
                }
                else
                {
                    key.SetValue("PressEnter", "false");
                }
            }
        }
    }
}
