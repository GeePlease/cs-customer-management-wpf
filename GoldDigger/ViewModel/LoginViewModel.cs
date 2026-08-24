using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoldDigger.Data;
using GoldDigger.Model;
using GoldDigger.Services;

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

        // ---- Relay Command Login Button, async to implement delay for readbility
        [RelayCommand]
        private async Task Login()
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
                // get matching user data: usernanme
                var user = db.Users.FirstOrDefault(u => u.UserName == Username);

                // check if match exists, check if pw matches hash in db via PasswordService verification
                if (user != null && PasswordService.VerifyPassword(user.PasswordHash, Password))
                {
                    ErrorMessage = $"Erfolgreich angemeldet als {user.UserName}";

                    // readability delay
                    await Task.Delay(1200);

                    // Login success event
                    OnLoginSuccess?.Invoke();

                }
                else
                {
                    ErrorMessage = "Falscher Benutzername oder Passwort!";
                }
            }
        }

        // Relay Command "Registrieren" Button, async to implement delay for readbility
        [RelayCommand]
        public async Task Register()
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
                // check if user already exists
                var existingUser = db.Users.FirstOrDefault(u => u.UserName == Username);

                if (existingUser != null)
                {
                    ErrorMessage = "Benutzername existiert bereits";
                }
                else
                {

                    // hash password!
                    string hashedPassword = PasswordService.HashPassword(Password);

                    // create new user 
                    var newUser = new User
                    {
                        UserName = Username,
                        PasswordHash = hashedPassword
                    };

                    // add new user to db
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    ErrorMessage = "Erfolgreich registriert als" + newUser.UserName;

                    // readability delay
                    await Task.Delay(1200);

                    // Login Success Event
                    OnLoginSuccess?.Invoke();

                }
            }
            
        }
     
    // END CLASS
    }
}