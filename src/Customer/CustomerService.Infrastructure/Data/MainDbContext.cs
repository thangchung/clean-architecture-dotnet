using CustomerService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.EfCore;

namespace CustomerService.Infrastructure.Data
{
    public class MainDbContext : AppDbContextBase
    {
        private const string Schema = "customer";

        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = default!;
        public DbSet<CreditCard> CreditCards { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension(Consts.UuidGenerator);

            // customer
            modelBuilder.Entity<Customer>().ToTable("customers", Schema);
            modelBuilder.Entity<Customer>().HasKey(x => x.Id);
            modelBuilder.Entity<Customer>().Property(x => x.Id).HasColumnType("uuid")
                .HasDefaultValueSql(Consts.UuidAlgorithm);

            modelBuilder.Entity<Customer>().Property(x => x.CountryId).HasColumnType("uuid");
            modelBuilder.Entity<Customer>().Property(x => x.Created).HasDefaultValueSql(Consts.DateAlgorithm);

            modelBuilder.Entity<Customer>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Customer>().Ignore(x => x.DomainEvents);

            // credit card
            modelBuilder.Entity<CreditCard>().ToTable("credit_cards", Schema);
            modelBuilder.Entity<CreditCard>().HasKey(x => x.Id);
            modelBuilder.Entity<CreditCard>().Property(x => x.Id).HasColumnType("uuid")
                .HasDefaultValueSql(Consts.UuidAlgorithm);

            modelBuilder.Entity<CreditCard>().Property(x => x.Created).HasDefaultValueSql(Consts.DateAlgorithm);
            modelBuilder.Entity<CreditCard>().Property(x => x.NameOnCard).HasMaxLength(20);
            modelBuilder.Entity<CreditCard>().Property(x => x.CardNumber).HasMaxLength(16);

            modelBuilder.Entity<CreditCard>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<CreditCard>().Ignore(x => x.DomainEvents);

            // relationship
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.CreditCards);
        }
    }
}
