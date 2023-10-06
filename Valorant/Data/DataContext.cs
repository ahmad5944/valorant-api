﻿namespace Valorant.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=localhost;Database=valorant;Trusted_Connection=True;TrustServerCertificate=Yes");
        }
        public DbSet<User> Users => Set<User>();
    }
}
