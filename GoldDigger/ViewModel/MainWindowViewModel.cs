using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GoldDigger.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {
        // ATTRIBUTES & VIEWMODELS

        // Visibility Menu Buttons

        // Login-ViewModel, called in MainWindow.xaml 
        [ObservableProperty]
        private LoginViewModel loginVM = new LoginViewModel();

        // Customers-ViewModel, called in MainWindow.xaml (public! not oversable)
        public  CustomersViewModel CustomersVM { get; } = new CustomersViewModel();

        // Visibility Login Mask
        [ObservableProperty]
        private bool isLoginVisible = true;

        // Visbility Customer Data (default View after successful login)
        [ObservableProperty] private bool isCustomersVisible = false;

        // Visibility New Customer Mask
        [ObservableProperty]
        private bool isNewCustomerVisible = false;

        // User Navigation Message
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

            // TODO: enable menu buttons
            //IsMenuEnabled = true;

            // delay for usability (time to read)

            // change login visibility and default visibility
            IsLoginVisible = false;
            IsCustomersVisible = true;

            // empty textboxes
            LoginVM.Username = "";
            LoginVM.Password = "";
        }

        // Relay Command: Button  click "Neukunde anlegen" - change visibility
        [RelayCommand] private void ShowNewCustomerView()
        {
            IsCustomersVisible = false;
            IsNewCustomerVisible = true;
        }


        // Relay Command: Button click  "Beenden"
        [RelayCommand]
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        // End Class
    }
}