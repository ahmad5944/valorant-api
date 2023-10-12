using Valorant.Models;
using Microsoft.EntityFrameworkCore;

namespace Valorant.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // public DataContext(DbContextOptions<DataContext> options) : base(options)
        // {

        // }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=valorant;Trusted_Connection=True;TrustServerCertificate=Yes");
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Character> Characters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
    }
}
