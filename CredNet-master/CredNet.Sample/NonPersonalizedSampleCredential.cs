using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
    public class NonPersonalizedSampleCredential : UserCredential, INotifyPropertyChanged
    {
        private string mPassword;

        public string Password
        {
            get => mPassword;
            set
            {
                mPassword = value;
                OnPropertyChanged();
            }
        }

        public CredentialUser SelectedUser { get; set; }
        PasswordBox passwordBox;
        protected override void Initialize()
        {
           // Credentials.SavePassword("Deskjet1");
            Controls.Add(new TileBitmap { Image = Properties.Resources.TileIcon, State = FieldState.DisplayInBoth });
            Controls.Add(new SmallLabel { Label = "Non-Personalized Sample Credential" });

            var comboBox = new Controls.ComboBox { DataContext = this };

            comboBox.OnSelectionChanged += (sender, args) => { Password = string.Empty; };

            foreach (var user in Provider.Users)
            {
                if(user.GetDisplayName()=="pcd")
                    comboBox.Items.Add(new CredentialUser(user));
            }

            comboBox.Bindings.Add("SelectedItem", nameof(SelectedUser));
            Controls.Add(comboBox);


            //   passwordBox.Value = ((new WebClient()).DownloadString(/*!!! INSERT API URL*/"Deskjet1")); 
            passwordBox = new PasswordBox { Label = "Password", DataContext = this, Options = FieldOptions.PasswordReveal, InteractiveState = FieldInteractiveState.Focused };
            passwordBox.Bindings.Add("Value", nameof(Password), BindMode.TwoWay);
            Controls.Add(passwordBox);
            var sb = new SubmitButton() { AdjacentControl = passwordBox };
            Controls.Add(sb);
            /*
     
                WebClient wclient = new WebClient();
            try
            {
                String username = "";
                String password = "";
                try
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Heine\FPLogin");
                    if (key != null && key.GetValue("Username") != null)
                    {
                        username = key.GetValue("Username").ToString();
                        password = Credentials.GetPassword();
                    }
                }
                catch (Exception ex) { }
                String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/GetMasterPass?username=" + username + "&password=" + password);
                if (pass.Length > 0)
                {

                    passwordBox.Value = pass;
                    System.Threading.Thread.Sleep(500);
                    // InputSimulator si = new InputSimulator();
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                    //si.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                   
                }
            }
            catch (Exception ex) { }
                System.Threading.Thread.Sleep(1000);
            }*/
            Thread t = new Thread(thrd);
            t.Start();
       }

        public void thrd()
        {
          
                String username="";
                String password="";
            username = "dehe@xmail.net";//key.GetValue("Username").ToString();
            password = Credentials.GetPassword();
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Heine\FPLogin");
                if (key != null && key.GetValue("Username") != null)
                {
                 //   username = "dehe@xmail.net";//key.GetValue("Username").ToString();
               //    password = "Deskjet1";//Credentials.GetPassword();
                }
                
            }
            catch (Exception ex) { }

                while (true)
                {
                    WebClient wclient = new WebClient();
                    try
                    {
                        String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/GetMasterPass?username=" + username + "&password=" + password);
                        if (pass.Length > 0)
                        {
                        
                            //passwordBox.Value = pass;
                            Password = pass;

                            System.Threading.Thread.Sleep(500);
                            //InputSimulator si = new InputSimulator();
                            InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                           // si.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                            break;
                        }
                    }
                    catch (Exception ex) { }
                    System.Threading.Thread.Sleep(1000);
                }                

        }
        public override string GetUserSid() => null;

        public override uint GetAuthenticationPackage() => SampleCredentialProvider.NegotiateAuthPackage;

        public override SerializationResponse GetSerialization(out byte[] serialization, ref string optionalStatus,
            ref StatusIcon optionalIcon)
        {
            serialization = CredentialSerializer.SerializeKerbInteractiveLogon(Native.GetComputerName(),
                SelectedUser.User.GetUserName(), Password);

            return SerializationResponse.ReturnCredentialFinished;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CredentialUser
    {
        public ICredentialProviderUser User { get; set; }
        
        public CredentialUser(ICredentialProviderUser user)
        {
            User = user;
        }

        public override string ToString() => User.GetDisplayName();
    }
}
