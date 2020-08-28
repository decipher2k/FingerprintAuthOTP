using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AotaSrvNew.Models
{
    public class BuildingContext: DbContext
    {
        public BuildingContext(DbContextOptions<BuildingContext> options)
            : base(options)
        {
        }

   
        public DbSet<Session> Session { get; set; }
        public DbSet<Seeds> Seeds { get; set; }
        public DbSet<PlayerData> PlayerData { get; set; }

    }
}
