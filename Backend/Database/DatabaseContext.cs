using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Database
{
    public class Context : DbContext
    {
        protected readonly IConfiguration Configuration;

        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AnalogInput> AnalogInput { get; set; }
        public DbSet<AnalogOutput> AnalogOutput { get; set; }
        public DbSet<DigitalInput> DigitalInput { get; set; }
        public DbSet<DigitalOutput> DigitalOutput { get; set; }
    }
}
