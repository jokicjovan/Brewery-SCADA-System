using Brewery_SCADA_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Brewery_SCADA_System.Database
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<Alarm> Alarm { get; set; }
        public DbSet<AnalogInput> AnalogInput { get; set; }
        public DbSet<AnalogOutput> AnalogOutput { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DigitalInput> DigitalInput { get; set; }
        public DbSet<DigitalOutput> DigitalOutput { get; set; }
        public DbSet<IOAnalogData> IOAnalogData { get; set; }
        public DbSet<IODigitalData> IODigitalData { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasData(new Device("111111",10), new Device("222222", 10),new Device("333333", 10), new Device("444444", 10),new Device("666666", 10));
        }


    }
}
