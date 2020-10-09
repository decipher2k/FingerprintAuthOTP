using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using CredNet.Sample;

namespace Typer
{
    class Program
    {
        static void Main(string[] args)
        {

            String username = "";
            String password = "";
            // username = //"dehe@xmail.net";//Credentials.GetUsername();//"dehe@xmail.net";//key.GetValue("Username").ToString();

            {
                /*    username = File.ReadAllText(Environment.GetEnvironmentVariable("public") + "\\fpauth.conf");

                    if (!File.Exists(Environment.GetEnvironmentVariable("public") + "\\fpauth_init.conf"))
                    {
                        WebClient wclient1 = new WebClient();
                        String pass1 = wclient1.DownloadString("https://fpauth.h2x.us/api/Session/Init?username=" + username);
                        if (pass1.Length == 0)
                        {
                            MessageBox.Show("Security breach!\nPlease contact your administrator.");
                        }
                        else
                        {
                            Credentials.SavePassword(pass1, username);
                            File.WriteAllText(Environment.GetEnvironmentVariable("public") + "\\fpauth_init.conf", "INIT");
                        }
                    }

                    try
                    {
                        password = Credentials.GetPassword();
                        if(password.Length==0||password==null)
                        {
                            throw new Exception();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Possible security problem!\nContact your administrator.");
                    }

                    */
                if (args.Length == 3)
                {
                    username = args[1];
                    password = args[2];
                }
                    if (password.Length == 0 || password == null)
                    {
                        throw new Exception();
                    }
                    WebClient wclient = new WebClient();
                    
                    /*  try
                      {
                          RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Heine\FPLogin");
                          if (key != null && key.GetValue("Username") != null)
                          {
                                 username = key.GetValue("Username").ToString();
                              //    password = "Deskjet1";//Credentials.GetPassword();
                          }

                      }
                      catch (Exception ex) { }*/

                    while (true)
                    {

                    try
                    {
                        String pass = wclient.DownloadString("https://fpauth.h2x.us/api/Session/GetMasterPass?session=" + password + "&username=" + username);
                        if (pass.Length > 0)
                        {

                            System.Threading.Thread.Sleep(500);
                            InputSimulator si = new InputSimulator();
                            //InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                            si.Keyboard.TextEntry(pass);
                            si.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                            break;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception ex) { }

                    }
                    System.Threading.Thread.Sleep(1000);
                    //new sleepFrm().ShowDialog();
                    //new sleepFrm().ShowDialog();
               
            }

        }
    }
}
