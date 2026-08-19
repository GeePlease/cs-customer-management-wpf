using Microsoft.EntityFrameworkCore;
using System;
using GoldDigger.Model; //namespace of classes user, customer

namespace GoldDigger.Data
{
    public class AppDbContext : DbContext
    {
        // real tables in db
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public string DbPath { get; }


        // db context and patzh
        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
           
            DbPath = System.IO.Path.Join(path, "golddigger.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}