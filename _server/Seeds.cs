using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace AOTA_Server
{
    public class Seeds
    {
        public Seeds()
        {
        }

        [Key]
        public long id {get; set;}
        public long idPlayer {get;set;}
        public string name { get; set; }
        public string seed { get; set; }        
    }
}
