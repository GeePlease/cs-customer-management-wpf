using System.Windows;
using GoldDigger.Data;
using GoldDigger.Services; // namespace


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
                // catch errors and origiginal error messages
                MessageBox.Show($"Datenbankfehler beim Start: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    // END CLASS
    }
}