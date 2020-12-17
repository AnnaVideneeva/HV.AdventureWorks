using Microsoft.EntityFrameworkCore;
using HV.AdventureWorks.Data.EntityConfigurations;

namespace HV.AdventureWorks.Data.Contexts
{
    public class AdventureWorksContext : DbContext
    {
        public string _connectionString { get; set; }

        public AdventureWorksContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, conf =>
            {
                conf.UseHierarchyId();
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Production");
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
        }
    }
}
