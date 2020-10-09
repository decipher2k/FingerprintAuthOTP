using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AotaSrvNew
{
    public class Session
    {
        [Key]
        public long id { get; set; }
        public String sessionKey { get; set; }
        public long lastUpdate { get; set; }        
        public long idUser { get; set; }
        public String type { get; set; }
    }
}
