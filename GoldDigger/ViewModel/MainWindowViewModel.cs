using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GoldDigger.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        // ATTRIBUTES & VIEWMODELS

        // Visibility Menu Buttons

        // Das Login-ViewModel, das in der MainWindow.xaml aufgerufen wird
        [ObservableProperty]
        private LoginViewModel loginVM = new LoginViewModel();

        // Visibility Login Mask
        [ObservableProperty]
        private bool isLoginVisible = true;

        // Visibility New Customer Mask
        [ObservableProperty]
        private bool isNewCustomerVisible = false;

        // Visibility Login Mask
        [ObservableProperty]
        private string userMessage = "Bitte einloggen";

        // CONSTRUCTOR
        public MainWindowViewModel()
        {
            // Hier abonnieren wir das Event aus dem LoginViewModel!
            LoginVM.OnLoginSuccess += HandleLoginSuccess;
        }



        // METHODS

        // On Login Success
        private void HandleLoginSuccess()
        {
            // header message
            UserMessage = "Erfolgreich angemeldet!";

            // enable menu buttons
            //IsMenuEnabled = true;

            // change login visibility and default visibility
            IsLoginVisible = false;
            // TODO: defdault customer view

            // empty textboxes
            LoginVM.Username = "";
            LoginVM.Password = "";
        }


        // Relay Command: Button "Beenden"
        [RelayCommand]
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        // End Class
    }
}