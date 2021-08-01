using Microsoft.EntityFrameworkCore;
using N8T.Infrastructure.EfCore;
using ProductService.AppCore.Core;

namespace ProductService.Infrastructure.Data
{
    public class MainDbContext : AppDbContextBase
    {
        private const string Schema = "prod";

        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductCode> ProductCodes { get; set; } = default!;
        public DbSet<Return> Returns { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension(Consts.UuidGenerator);

            // product
            modelBuilder.Entity<Product>().ToTable("products", Schema);
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnType("uuid")
                .HasDefaultValueSql(Consts.UuidAlgorithm);

            modelBuilder.Entity<Product>().Property(x => x.ProductCodeId).HasColumnType("uuid");
            modelBuilder.Entity<Product>().Property(x => x.Created).HasDefaultValueSql(Consts.DateAlgorithm);

            modelBuilder.Entity<Product>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<Product>().Ignore(x => x.DomainEvents);

            // product code
            modelBuilder.Entity<ProductCode>().ToTable("product_codes", Schema);
            modelBuilder.Entity<ProductCode>().HasKey(x => x.Id);
            modelBuilder.Entity<ProductCode>().Property(x => x.Id).HasColumnType("uuid")
                .HasDefaultValueSql(Consts.UuidAlgorithm);

            modelBuilder.Entity<ProductCode>().Property(x => x.Created).HasDefaultValueSql(Consts.DateAlgorithm);
            modelBuilder.Entity<ProductCode>().Property(x => x.Name).HasMaxLength(5);

            modelBuilder.Entity<ProductCode>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<ProductCode>().Ignore(x => x.DomainEvents);

            // return
            modelBuilder.Entity<Return>().ToTable("returns", Schema);
            modelBuilder.Entity<Return>().HasKey(x => x.Id);
            modelBuilder.Entity<Return>().Property(x => x.Id).HasColumnType("uuid")
                .HasDefaultValueSql(Consts.UuidAlgorithm);

            modelBuilder.Entity<Return>().Property(x => x.ProductId).HasColumnType("uuid");
            modelBuilder.Entity<Return>().Property(x => x.CustomerId).HasColumnType("uuid");
            modelBuilder.Entity<Return>().Property(x => x.Created).HasDefaultValueSql(Consts.DateAlgorithm);

            modelBuilder.Entity<Return>().HasIndex(x => x.Id).IsUnique();
            //modelBuilder.Entity<Return>().Ignore(x => x.DomainEvents);

            // relationship
            modelBuilder.Entity<Product>()
                .HasOne(x => x.Code)
                .WithMany()
                .HasForeignKey(x => x.ProductCodeId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasMany(x => x.Returns);
        }
    }
}
