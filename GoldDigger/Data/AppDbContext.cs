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
    }
}