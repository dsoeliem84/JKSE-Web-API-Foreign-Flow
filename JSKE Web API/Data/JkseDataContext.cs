using JKSE_Web_API.Models;
using Microsoft.EntityFrameworkCore;

namespace JKSE_Web_API.Data
{
    public class JkseDataContext:DbContext
    {
        public DbSet<ForeignFlow> ForeignFlow { get; set; }

       // public DbSet<List<ForeignFlow>> ForeignFlows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=NETCORE-PROJECT;initial catalog=JKSE_ForeignFlow;user id=sa;password=123qweasd;Trusted_Connection=SSPI;TrustServerCertificate=true");
        }
    }
}

   
