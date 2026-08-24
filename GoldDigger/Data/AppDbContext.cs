using Microsoft.EntityFrameworkCore;
using GoldDigger.Model;

namespace GoldDigger.Data
{
    public class AppDbContext : DbContext
    {

        // db tables for EF
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // db path (automatic creation in project file), @ to ensure \ is treated as path symbols
        // hard coded path = NOT best practice! no flexibility and adaptability, not secure enough!!
        public string DbPath => @"C:\Export\Gmeiner\Quali_C#\CS_07_02_Datenbanken_GoldDigger\golddigger.db";

        // CONSTRUCTOR
        public AppDbContext()
        {
            // check if db already exists
            Database.EnsureCreated();
        }

        // configure database connection (use SQLite with the specified path)
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");






        // example: db entity relation creation for one-to one, one-to-many, many-to-many

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. 1:1 Beziehung (One-to-One)
            // Beispiel: Ein User hat genau ein Profil, ein Profil gehört zu genau einem User.
            /*
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);
            */

            // 2. 1:N Beziehung (One-to-Many) - Dein Beispiel!
            // Beispiel: Ein Customer hat viele Rechnungen (Invoices), eine Rechnung gehört zu genau einem Customer.
            /*
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .IsRequired();
            */

            // 3. N:M Beziehung (Many-to-Many)
            // Beispiel: Ein Customer kann in vielen Kampagnen sein, eine Kampagne hat viele Customers.
            // (Ab .NET 5+ macht EF Core das oft automatisch, wenn du Listen in den Models hast, 
            // man kann es hier aber explizit steuern):
            /*
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Campaigns)
                .WithMany(camp => camp.Customers);
            */

    }
}