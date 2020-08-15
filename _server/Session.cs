using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AOTA_Server
{
    public class Session
    {
        [Key]
        public long id { get; set; }
        public String sessionKey { get; set; }
        public long lastUpdate { get; set; }        
        public long idUser { get; set; }
    }
}
