using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAuth_Client
{
    public static class Credentials
    {
        private static String PasswordName = "FPAuth";
        public static void SavePassword(string password)
        {
            try
            {
                using (var cred = new Credential())
                {
                    cred.Password = password;
                    cred.Target = PasswordName;
                    cred.Type = CredentialType.Generic;
                    cred.PersistanceType = PersistanceType.LocalComputer;
                    cred.Save();
                }
            }
            catch (Exception ex)
            {
            }
        }
        //retrieve password from the windows vault store using Credential Manager   
        public static string GetPassword()
        {
            try
            {
                using (var cred = new Credential())
                {
                    cred.Target = PasswordName;
                    cred.Load();
                    return cred.Password;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

    }
}
