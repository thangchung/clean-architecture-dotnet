using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.EfCore;
using SettingService.Core.Entities;

namespace SettingService.Infrastructure.Data
{
    public class MainDbContext : AppDbContextBase
    {
        private const string Schema = "setting";

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension(Consts.UuidGenerator);

            // country
            modelBuilder.Entity<Country>().ToTable("countries", Schema);
            modelBuilder.Entity<Country>().HasKey(x => x.Id);
            modelBuilder.Entity<Country>().Property(x => x.Id).HasColumnType("uuid")
                .HasDefaultValueSql(Consts.UuidAlgorithm);

            modelBuilder.Entity<Country>().Property(x => x.Created).HasDefaultValueSql(Consts.DateAlgorithm);

            modelBuilder.Entity<Country>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Country>().Ignore(x => x.DomainEvents);
        }
    }
}
