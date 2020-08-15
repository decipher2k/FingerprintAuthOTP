using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace AOTA_Server
{
    public class PlayerData
    {
        public PlayerData()
        {
        }

        [Key]
        public long id {get; set;}
        public string Name {get;set;}
        public string Password { get; set; }
        public string Session { get; set; }
        public long Silver { get; set; } = 0;
        public long Gold { get; set; } = 0;
        public long Iron { get; set; } = 0;
        public long Mana { get; set; } = 0;
    }
}
