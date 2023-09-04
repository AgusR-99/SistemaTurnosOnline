using Microsoft.EntityFrameworkCore;
using SistemaTurnosOnline.Shared;

namespace SistemaTurnosOnline.Api.Data
{
    public class SistemaTurnosOnlineDbContextEf : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<CarreraEf> CarreraSet { get; set; }
        
        public SistemaTurnosOnlineDbContextEf(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraEf>().ToTable("Carrera");
        }
    }
}
