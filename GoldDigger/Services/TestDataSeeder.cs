using GoldDigger.Data;
using GoldDigger.Model;
using GoldDigger.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GoldDigger.Services
{
    internal class TestDataSeeder
    {
        //ATTRIBUTES

        // CONSTRUCTOR

        // METHODS

        // ---- CreateTestData: create test data and save in db
        public static void CreateTestData()
        {
            // create customer test data and save in list
            var testDataCustomers = new List<Customer>
            {
                new Customer { FirstName = "Max", LastName = "Mustermann", Street = "Goldweg", StreetNumber = "1", PostCode = "10115", Residence = "Berlin", Mail = "max@gold.de" },
                new Customer { FirstName = "Anna", LastName = "Schürf", Street = "Minenstraße", StreetNumber = "12", PostCode = "80331", Residence = "München", Mail = "anna@schuerf.de" },
                new Customer { FirstName = "Tom", LastName = "Nugget", Street = "Schatzsucherweg", StreetNumber = "5", PostCode = "50667", Residence = "Köln", Mail = "tom@nugget.de" },
                new Customer { FirstName = "Lisa", LastName = "Minner", Street = "Erzweg", StreetNumber = "42", PostCode = "20095", Residence = "Hamburg", Mail = "lisa@minner.de" },
                new Customer { FirstName = "John", LastName = "Smith", Street = "Hauptstraße", StreetNumber = "100", PostCode = "60311", Residence = "Frankfurt", Mail = "john@smith.de" },
                new Customer { FirstName = "Sarah", LastName = "Connor", Street = "Widerstandsweg", StreetNumber = "84", PostCode = "70173", Residence = "Stuttgart", Mail = "sarah@resistance.de" },
                new Customer { FirstName = "Bruce", LastName = "Wayne", Street = "Millionärsallee", StreetNumber = "1", PostCode = "45127", Residence = "Essen", Mail = "bruce@wayne.com" },
                new Customer { FirstName = "Clark", LastName = "Kent", Street = "Metropolisweg", StreetNumber = "99", PostCode = "01067", Residence = "Dresden", Mail = "clark@dailyplanet.com" },
                new Customer { FirstName = "Diana", LastName = "Prince", Street = "Amazonenpfad", StreetNumber = "7", PostCode = "90402", Residence = "Nürnberg", Mail = "diana@themyscira.de" },
                new Customer { FirstName = "Peter", LastName = "Parker", Street = "Fotoweg", StreetNumber = "15", PostCode = "30159", Residence = "Hannover", Mail = "peter@bugle.de" }
            };

            // create user test data and save in list (include pw hashing!!!!)
            var testDataUsers = new List<User>
            {
                new User { UserName = "admin", PasswordHash = PasswordService.HashPassword("123") },
                new User { UserName = "boss", PasswordHash = PasswordService.HashPassword("456") },
                new User { UserName = "worker", PasswordHash = PasswordService.HashPassword("789") }
            };

            // connect to db and save data
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();

                // add customer data only if table empty
                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(testDataCustomers);
                }

                // add user data only if table empty
                if (!context.Users.Any())
                {
                    context.Users.AddRange(testDataUsers);
                }

                // sasve chantes in db
                context.SaveChanges();
            }
        }

       


    // END CLASS
    }
}
