using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CredNet.Controls;
using CredNet.Interop;
using Microsoft.Win32;
using WindowsInput;

namespace CredNet.Sample
{
    public class PersonalizedSampleCredential : UserCredential
    {
        public ICredentialProviderUser User { get; }
        PasswordBox passwordBox;
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        public string Password { get; set; }
        Form f1;

        public PersonalizedSampleCredential(ICredentialProviderUser user)
        {
            User = user;
        }

        public void thrd()
        {

            String username = "";
            String password = "";
            try
            {
                username = Credentials.GetUsername();
                password = Credentials.GetPassword();
                WebClient wclient = new WebClient();
                if (password.Length == 0 || password == null)
                {
                    throw new Exception();
                }            
                
                password = wclient.DownloadString("https://fpauth.h2x.us/api/Session/NewSession?session=" + password + "&username=" + username);
                Credentials.SavePassword(password, username);
                
                while (true)
                {
                    try
                    {
                        String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/GetMasterPass?session=" + password + "&username=" + username);
                        if (pass.Length > 0)
                        {
                            Password = pass;
                            passwordBox.Value = pass;
                            System.Threading.Thread.Sleep(500);
                            InputSimulator si = new InputSimulator();
                            si.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                        }

                        catch (Exception ex)
                        {

                            f1 = new Form1();
                            f1.ShowDialog();
                        }
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {

                    f1 = new Form1();
                    f1.ShowDialog();
                }
            }
        }
    
        protected override void Initialize()
        {
            passwordBox = new PasswordBox
            {
                Label = "Password",
                DataContext = this,
                Options = FieldOptions.PasswordReveal,
                InteractiveState = FieldInteractiveState.Focused
            };

            passwordBox.Bindings.Add("Value", nameof(Password), BindMode.TwoWay);
            Controls.Add(new TileBitmap { Image = Properties.Resources.TileIcon, State = FieldState.DisplayInDeselectedTile });
            Controls.Add(new SmallLabel { Label = $"Personalized Credential for {User.GetDisplayName()}" });

            Controls.Add(passwordBox);
            
            SubmitButton sb = new SubmitButton() { AdjacentControl = passwordBox };

            Controls.Add(sb);            
            Thread t = new Thread(thrd);
            t.Start();
           
        }

        public override string GetUserSid() => User.GetPrimarySid();

        public override uint GetAuthenticationPackage() => SampleCredentialProvider.NegotiateAuthPackage;

        public override SerializationResponse GetSerialization(out byte[] serialization, ref string optionalStatus,
            ref StatusIcon optionalIcon)
        {
            if (string.IsNullOrEmpty(Password))
            {
                serialization = null;
                optionalStatus = "Invalid password!";
                optionalIcon = StatusIcon.Error;
                return SerializationResponse.NoCredentialNotFinished;
            }

            serialization = CredentialSerializer.SerializeKerbInteractiveLogon(Native.GetComputerName(),
                User.GetUserName(), Password);
            return SerializationResponse.ReturnCredentialFinished;
        }
    }
}
