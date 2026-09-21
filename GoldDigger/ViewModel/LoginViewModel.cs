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

        [ObservableProperty]
        private string confirmPassword; // for pw verification field in register mode

        [ObservableProperty]
        private bool isRegisterMode = false; // for register mode

        [ObservableProperty]
        private string registerButtonText = "Registrieren"; // Text ändert sich dynamisch


        // EVENTS

        public event Action<string> OnLoginSuccess; // pass on username

        // METHODS

        // ---- Relay Command Login Button, async to implement delay for readability
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
                    OnLoginSuccess?.Invoke(user.UserName);

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

            // change in to register mode on click
            if (!IsRegisterMode)
            {
                IsRegisterMode = true;
                ErrorMessage = "Bitte gib dein Passwort zur Bestätigung erneut ein.";
                return; // return, no save yet
            }


            // ---- start register mode:
            // check if field(s) are empty
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Bitte alle Felder ausfüllen!";
                return;
            }

            // check if passwords are matching
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Die Passwörter stimmen nicht überein!";
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
                    OnLoginSuccess?.Invoke(newUser.UserName);

                }


            }
            
        }

        // relay command for existing register mode
        [RelayCommand]
        private void CancelRegister()
        {
            IsRegisterMode = false;
            ConfirmPassword = string.Empty;
            ErrorMessage = string.Empty;
        }


        // END CLASS
    }
}