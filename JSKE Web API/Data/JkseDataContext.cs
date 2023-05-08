using JKSE_Web_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JKSE_Web_API.Data
{
    public class JkseDataContext:DbContext
    {
        public DbSet<ForeignFlow> ForeignFlow { get; set; }
        public DbSet<DailyStats> DailyStats { get; set; }


        //public DbSet<List<ForeignFlow>> ForeignFlows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            IConfiguration configuration = builder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ForeignFlowReport>().HasNoKey().ToView(null);
        }

    }
}

   
