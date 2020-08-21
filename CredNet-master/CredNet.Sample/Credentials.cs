using CredentialManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredNet.Sample
{
    public static class Credentials
    {
        private static String PasswordName = "FPAuth";
        public static void SavePassword(string password,string username, String name= "FPAuth")
        {
            try
            {
                using (var cred = new Credential())
                {
                    cred.Password = password;
                    cred.Username = username;
                    cred.Target = name;
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
        public static string GetPassword(String name = "FPAuth")
        {
            try
            {
                using (var cred = new Credential())
                {
                    cred.Target = name;
                    cred.Load();
                    return cred.Password;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }

        public static string GetUsername(String name = "FPAuth")
        {
            try
            {
                using (var cred = new Credential())
                {
                    cred.Target = name;
                    cred.Load();
                    return cred.Username;
                }
            }
            catch (Exception ex)
            {
            }
            return "";
        }


    }
}
