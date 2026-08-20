using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoldDigger.Data;

namespace GoldDigger.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {

        // ATTRIBUTES

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password; 

        [ObservableProperty]
        private string errorMessage;


        // EVENTS

        public event Action OnLoginSuccess;



        // METHODS

        // Relax Command Login Button
        [RelayCommand]
        private void Login()
        {
            // check if field(s) are empty
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Bitte alle Felder ausfüllen!";
                return;
            }

            // db connection
            using (var db = new AppDbContext())
            {
                // get matching user data
                var user = db.Users.FirstOrDefault(u => u.UserName == Username && u.PasswordHash == Password);

                // check if match exists
                if (user != null)
                {
                    ErrorMessage = "";
                    // Login success event
                    OnLoginSuccess?.Invoke();

                }
                else
                {
                    ErrorMessage = "Falscher Benutzername oder Passwort!";
                }
            }
        }
    }
}