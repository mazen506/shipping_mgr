using Microsoft.EntityFrameworkCore;
using ShippingMgr.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ShippingMgr.Core.Database.Context
{
    //[DbConfigurationType(typeof(Pomelo.EntityFrameworkCore.MySql.))]

    public sealed class AppDataContext : IdentityDbContext<AppUser>
    {

        //public AppDataContext() : base() { }
        public AppDataContext(DbContextOptions<AppDataContext> options)
         : base(options)
         {

         }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);

                var config = builder.Build();
                var connectionString = config.GetConnectionString(nameof(AppDataContext)); 
                var serverVersion = new MySqlServerVersion(new Version(5, 5, 27));
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);
            modelBuilder.HasCharSet(null, DelegationModes.ApplyToAll);
            modelBuilder.Entity<AppUser>()
                    .ToTable("Users").Property(p => p.PasswordHash).HasColumnName("Password");
            //modelBuilder.Entity<AppUser>().Property(u => u.Locale).HasDefaultValue("ar");
            modelBuilder.Entity<AppUser>().Property(u => u.UserName).HasMaxLength(255);
            modelBuilder.Entity<AppUser>().Property(u => u.Email).HasMaxLength(255);
            modelBuilder.Entity<IdentityRole>().Property(r => r.Name).HasMaxLength(255);
        }
    }

    //public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDataContext>
    //{
    //    AppDataContext IDesignTimeDbContextFactory<AppDataContext>.CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //                                           .AddJsonFile("appsettings.Staging.json")
    //                                           .Build();
    //        var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
    //        var connectionString = configuration.GetConnectionString(nameof(AppDataContext));
    //        var serverVersion = new MySqlServerVersion(new Version(5, 5, 27));
    //        optionsBuilder.UseMySql(connectionString, serverVersion);
    //        return new AppDataContext(optionsBuilder.Options);
    //    }
    //}

    public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
    {
        public string HashPassword(TUser user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }
        public PasswordVerificationResult VerifyHashedPassword(
          TUser user, string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentNullException(nameof(hashedPassword));
            if (string.IsNullOrWhiteSpace(providedPassword)) throw new ArgumentNullException(nameof(providedPassword));
            var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, 12))
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
