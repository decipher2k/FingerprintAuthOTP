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
        private static Dictionary<String, String> masterpasses = new Dictionary<string, string>();
        public static Dictionary<String, String> passes = new Dictionary<string, string>();
        public static Dictionary<String, String> inits = new Dictionary<string, string>();
        private readonly Models.BuildingContext _buildingContext;

        public SessionController(Models.BuildingContext buildingContext)
        {
            _buildingContext = buildingContext;
        }

        [HttpGet("{session,username}")]
        [Route("GetMasterPass")]
        public String GetMasterPass([FromQuery(Name = "session")] String session, [FromQuery(Name = "username")] String username)
        {

            Session s = _buildingContext.Session.Where(a => a.sessionKey== session).ToList()[0];
            s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
           
                if(passes.ContainsKey(username))
                {
                   
                    String seed = "";
                    passes.TryGetValue(username, out seed);
                    if (seed.Substring(0, 1).Equals("#"))
                    {
                        passes.Remove(username);
                        return seed.Substring(1);
                    }

                        var sha1 = new SHA1CryptoServiceProvider();

                        string result = null;

                        
                        var base32Bytes = Base32Encoding.ToBytes(seed);
                        
                        var totp = new Totp(base32Bytes);
                        var totpCode = totp.ComputeTotp();
                        passes.Remove(username);
                        return totpCode;                        
           }
            return "";
        }

        [HttpGet("{session,masterpass,username}")]
        [Route("Login")]
        public String Login([FromQuery(Name = "session")] String session, [FromQuery(Name = "masterpass")] string masterpass, [FromQuery(Name = "username")] string username)
        {
            Session s;
            List<Session> sessions = _buildingContext.Session.Where(a => a.sessionKey==session).ToList();
            s = sessions[0];            
            s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            if(passes.ContainsKey(username))
            {
                passes.Remove(username);
            }
            passes.Add(username, masterpass);                        
            return s.sessionKey;
        }

        [HttpGet("{username, password,type}")]
        [Route("DoLogin")]
        public String DoLogin([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password, [FromQuery(Name = "type")] String type)
        {
            bool existing = false;
            PlayerData u = _buildingContext.PlayerData.Where<PlayerData>(a => a.Name == username).Single();
            
            Session s;
            List<Session> sessions = _buildingContext.Session.Where(a => a.idUser == u.id && a.type == type).ToList();
            if (sessions.Count != 0)
            {
                foreach (Session s1 in sessions)
                    _buildingContext.Session.Remove(s1);
            }
            else
            {
                existing = true;
            }
            s = new Session();
            s.sessionKey = genSessionKey();
            s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            s.idUser = u.id;
            s.type = type;
            _buildingContext.SaveChanges();
            _buildingContext.Session.Add(s);
            _buildingContext.SaveChanges();

            if (masterpasses.ContainsKey(s.sessionKey))
                masterpasses.Remove(s.sessionKey);
            masterpasses.Add(s.sessionKey, password);

            if(passes.ContainsKey(s.sessionKey))
                passes.Remove(s.sessionKey);

            return s.sessionKey;         
        }

        [HttpGet("{session,username}")]
        [Route("NewSession")]
        public String NewSession([FromQuery(Name = "session")] String session, [FromQuery(Name = "username")] String username="")
        {
            bool existing = false;
            Session s;
            List<Session> sessions = _buildingContext.Session.Where(a => a.sessionKey==session).ToList();
            if (sessions.Count == 0)
                throw new Exception();

            if(username!="")
            {
                if(passes.ContainsKey(username))
                    passes.Remove(username);
            }

            s = new Session();
            s.sessionKey = genSessionKey();
            s.lastUpdate = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            s.idUser = sessions[0].idUser;
            s.type = sessions[0].type;

            if (masterpasses.ContainsKey(session))
            {
                masterpasses.Add(s.sessionKey, masterpasses[session]);
                masterpasses.Remove(session);
            }
            else
            {
                throw new Exception();
            }

            if (sessions.Count != 0)
            {
                foreach (Session s1 in sessions)
                    _buildingContext.Session.Remove(s1);
            }
            else
            {
                existing = true;
            }

            _buildingContext.SaveChanges();
            _buildingContext.Session.Add(s);
            _buildingContext.SaveChanges();
          
            return s.sessionKey;           
        }

        [HttpGet("{session}")]
        [Route("DoLogout")]
        public String DoLogout([FromQuery(Name = "session")] String session)
        {
            bool existing = false;

            Session s;
            List<Session> sessions = _buildingContext.Session.Where(a => a.sessionKey == session).ToList();
            PlayerData u = _buildingContext.PlayerData.Where<PlayerData>(a =>a.id==sessions[0].idUser).Single();
            if (sessions.Count != 0)
            {
                foreach (Session s1 in sessions)
                    _buildingContext.Session.Remove(s1);
            }
            _buildingContext.SaveChanges();
            masterpasses.Remove(session);
            passes.Remove(session);
            return "OK";
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

        [HttpGet("{session}")]
        [Route("GetSeeds")]
        public SeedsCapsule GetSeeds( [FromQuery(Name = "session")] String session)
        {
            List<Session> sessions = _buildingContext.Session.Where(a => a.sessionKey == session).ToList();

            PlayerData p = _buildingContext.PlayerData.Where<PlayerData>(a => a.id == sessions[0].idUser).Single();
            if (p == null)
            {
                throw new Exception();
            }
            else
            {
                    List<Seeds> s = _buildingContext.Seeds.Where(a => a.idPlayer == p.id).ToList();
                    SeedsCapsule sc = new SeedsCapsule();

                    for(int i=0;i<s.Count; i++)
                    {
                        s[i].seed = StringCipher.Decrypt(s[i].seed, masterpasses[session]);
                    }
                    sc.seeds = s;
                    return sc;
            }
            return null;
        }

        [HttpGet("{username}")]
        [Route("Init")]
        public String Init([FromQuery(Name = "username")] String username)
        {
            return null;
        }

        [HttpGet("{username, password}")]
        [Route("InitInit")]
        public String InitInit([FromQuery(Name = "username")] String username, [FromQuery(Name = "password")] String password)
        {
            return null;
        }

        [HttpGet("{session,seed,name,isstp}")]
        [Route("AddSeed")]
        public String AddSeed([FromQuery(Name = "session")] String session, [FromQuery(Name = "seed")] String seed, [FromQuery(Name = "name")] String name, [FromQuery(Name = "isstp")] String isstp)
        {
            List<Session> sessions = _buildingContext.Session.Where(a => a.sessionKey == session).ToList();
            PlayerData p = _buildingContext.PlayerData.Where<PlayerData>(a => a.id == sessions[0].idUser).Single();
            if (p == null)
            {
                return "ERR";
            }
            else
            {             
                {
                    if( _buildingContext.Seeds.Where<Seeds>((a => a.idPlayer == p.id)).Where(a => a.name == name).ToList().Count==0)
                    
                    {
                        Seeds mseed = new Seeds();
                        mseed.name = name;
                        if (isstp == "TRUE")
                        {
                            seed = "#" + seed;
                        }
                        mseed.seed = StringCipher.Encrypt(seed,masterpasses[session]);
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

        public static string GetMD5Hash(string TextToHash)
        {
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return string.Empty;
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = System.Text.Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);
            String ret = System.BitConverter.ToString(result).ToLower().Replace("-", "");
            return ret;
        }

    }
}
