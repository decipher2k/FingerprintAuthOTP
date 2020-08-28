using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using OtpNet;

namespace AotaSrvNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly Models.BuildingContext _buildingContext;

        public SessionController(Models.BuildingContext buildingContext)
        {
            _buildingContext = buildingContext;
        }
        public static string GetMD5Hash(string TextToHash)
        {
            //Prüfen ob Daten übergeben wurden.
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return string.Empty;
            }

            //MD5 Hash aus dem String berechnen. Dazu muss der string in ein Byte[]
            //zerlegt werden. Danach muss das Resultat wieder zurück in ein string.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = System.Text.Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);
            String ret = System.BitConverter.ToString(result).ToLower().Replace("-", "");
            return ret;
        }


        [HttpGet("{username, password}")]
        [Route("GetMasterPass")]
        public String GetMasterPass(String username, String password)
        {

            PlayerData u = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            Session s = _buildingContext.Session.Where(a => a.idUser == u.id).ToList()[0];
            s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            s.idUser = u.id;
            if (u.Password == GetMD5Hash(password))
            {
                if(passes.ContainsKey(s.sessionKey))
                {
                    // String seed = passes[s.sessionKey];

                    // Seeds seeds = _buildingContext.Seeds.Where<Seeds>((a => a.idPlayer == u.id)).Where(a => a.seed == seed).Single();
                    // if (seeds != null)
                    // {
                    //  if (passes.ContainsKey(u.Session))
                    // {
                    // String _session = passes[u.Session];
                    //  passes.Remove(u.Session);
                    String seed = "";
                    passes.TryGetValue(s.sessionKey, out seed);
                    if (seed.Substring(0, 1).Equals("#"))
                    {
                        passes.Remove(s.sessionKey);
                        return seed.Substring(1);
                    }

                        var sha1 = new SHA1CryptoServiceProvider();

                        string result = null;

                        
                        var base32Bytes = Base32Encoding.ToBytes(seed);
                        //var arrayResult = sha1.ComputeHash(arrayData);
                        var totp = new Totp(base32Bytes);
                        var totpCode = totp.ComputeTotp();
                        passes.Remove(s.sessionKey);
                        return totpCode;
                        //}
                   // }
                }
            }
            return "";
        }

        public static Dictionary<String, String> passes = new Dictionary<string, string>();
        // GET: api/Building
        [HttpGet("{username, password,masterpass}")]
        [Route("Login")]
        public String Login([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password, [FromQuery(Name = "masterpass")] string masterpass)
        {
            Session s;
            
            PlayerData u = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            
            
            List<Session> sessions = _buildingContext.Session.Where(a => a.idUser == u.id).ToList();
            if (sessions.Count == 0)
            {
                s = new Session();
                s.sessionKey = genSessionKey();
                s.idUser = u.id;
                _buildingContext.Add(s);
                _buildingContext.SaveChanges();
            }
            else
            {
                s = sessions[0];
            }
            s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            if (u.Password == GetMD5Hash(password))
                {                    
                    u.Session = s.sessionKey;
                  
                    _buildingContext.Update(u);
                    _buildingContext.SaveChanges();
                    if (!passes.ContainsKey(u.Session))
                    {
                        passes.Add(u.Session, masterpass);
                    }

                }
            
            return s.sessionKey;
        }
        [HttpGet("{username, password}")]
        [Route("DoLogin")]
        public String DoLogin([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password)
        {
            
            PlayerData u = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();

            Session s;
            List<Session> sessions = _buildingContext.Session.Where(a => a.idUser == u.id).ToList();
            if (sessions.Count == 0)
            {
                 s = new Session();
                s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                s.idUser = u.id;
            }
            else
            {
                s = sessions[0];
            }

            if (u.Password == GetMD5Hash(password))
            {
                return "AUTH";
            }
            return "ERR";
        }

        [HttpGet("{username, password}")]
        [Route("Register")]
        public String Register([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password)
        {

            if (_buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Count() > 0)
            {
                return "ERR";
            }
            else
            {
                PlayerData userData = new PlayerData();
                userData.Name = username;
                userData.Password = GetMD5Hash(password);
                _buildingContext.Add(userData);
                _buildingContext.SaveChanges();
                return "AUTH";
            }
        }

        [HttpGet("{username, password}")]
        [Route("GetSeeds")]
        public SeedsCapsule GetSeeds([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password)
        {
            PlayerData p = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            if (p == null)
            {
                return null;
            }
            else
            {
                if (p.Password == GetMD5Hash(password))
                {
                    List<Seeds> s = _buildingContext.Seeds.Where(a => a.idPlayer == p.id).ToList();
                    SeedsCapsule sc = new SeedsCapsule();

                    for(int i=0;i<s.Count; i++)
                    {
                        s[i].seed = StringCipher.Decrypt(s[i].seed, password);
                    }
                    sc.seeds = s;
                    return sc;
                }
            }
            return null;
        }




        [HttpGet("{username}")]
        [Route("Init")]
        public String Init([FromQuery(Name = "username")] String username)
        {
            PlayerData p = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            if (p == null)
            {
                return null;
            }
            else
            {
                if (!inits.ContainsKey(username))
                {
                    return null;
                }
                else
                {
                    String pass = inits[username];
                    inits.Remove(username);
                    return pass;
                }
            }
        }

        [HttpGet("{username, password}")]
        [Route("InitInit")]
        public String InitInit([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password)
        {
            PlayerData p = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            if (p == null)
            {
                return null;
            }
            else
            {
                if (!inits.ContainsKey(username))
                    inits.Add(username, password);
                else
                    inits[username] = password;
            }
            return null;
        }
        public static Dictionary<String, String> inits = new Dictionary<string, string>();
        [HttpGet("{username, password,seed,name,isstp}")]
        [Route("AddSeed")]
        public String AddSeed([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password, [FromQuery(Name = "seed")] String seed, [FromQuery(Name = "name")] String name, [FromQuery(Name = "isstp")] String isstp)
        {
            PlayerData p = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            if (p == null)
            {
                return "ERR";
            }
            else
            {
                if (p.Password == GetMD5Hash(password))
                {
                    if( _buildingContext.Seeds.Where<Seeds>((a => a.idPlayer == p.id)).Where(a => a.name == name).ToList().Count==0)
                    
                    {
                        Seeds mseed = new Seeds();
                        mseed.name = name;
                        if (isstp == "TRUE")
                        {
                            seed = "#" + seed;
                        }
                        mseed.seed = StringCipher.Encrypt(seed,password);
                        mseed.idPlayer = p.id;
                        
                        
                        _buildingContext.Add(mseed);
                        _buildingContext.SaveChanges();
                        return "AUTH";
                    }
                }
            }
            return "ERR";
        }


        private string genSessionKey()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 40)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
