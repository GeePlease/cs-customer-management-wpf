using System;
using System.Configuration;
using System.Data;
using System.Windows;
using GoldDigger.Data;
using GoldDigger.Services; // namespace

//TODO: Passwörter hashen!

namespace GoldDigger
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {

        // OnStartup: when App ist started - db init. and connection
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            // try catch for error feedback

            try
            {
                // automatic EnsureCreated() check included
                using (var db = new AppDbContext())
                {
                    Console.WriteLine("Datenbank erfolgreich initialisiert!");
                }

                // create test data (from Serviess/ TestDataSeeder)
                TestDataSeeder.CreateTestData();
                Console.WriteLine("Testdaten erfolgreich kreiert.");

            }
            catch (Exception ex)
            {
                // Hier landet jeder echte Fehler (Pfad falsch, Rechte fehlen etc.)
                MessageBox.Show($"Datenbankfehler beim Start: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    // END CLASS
    }
}