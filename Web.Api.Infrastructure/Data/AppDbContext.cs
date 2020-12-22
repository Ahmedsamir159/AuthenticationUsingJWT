using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Shared;


namespace Web.Api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product(1, "jacket", "1202", 150));
            modelBuilder.Entity<Product>().HasData(new Product(2, "shirt", "955545", 400));
            modelBuilder.Entity<Product>().HasData(new Product(3, "PC", "955545", 500));
            modelBuilder.Entity<User>(ConfigureUser);


        }


        public void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(User.RefreshTokens));
            
            //EF access the RefreshTokens collection property through its backing field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.Ignore(b => b.Email);
            builder.Ignore(b => b.PasswordHash);
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
                ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }
    }
}


