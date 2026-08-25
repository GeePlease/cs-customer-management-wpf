using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GoldDigger.ViewModel
{
    public partial class MainWindowViewModel : ObservableObject
    {

        // ATTRIBUTES & VIEWMODELS


        // Login-ViewModel, called in MainWindow.xaml 
        [ObservableProperty]
        private LoginViewModel loginVM = new LoginViewModel();

        // Customers-ViewModel, called in MainWindow.xaml (public! not oversable)
        public  CustomersViewModel CustomersVM { get; } = new CustomersViewModel();

        // NewCustomer View Model
        [ObservableProperty]
        private NewCustomerViewModel newCustomerVM = new NewCustomerViewModel();

        // EditCustomer View odel - no init. bc no customer selected at start/ no data for model
        [ObservableProperty]
        private EditCustomerViewModel editCustomerVM;


        // Menu Button Visbility property (Logout Button included)
        [ObservableProperty]
        private bool isMenuEnabled = false;

        // Visibility Login Mask
        [ObservableProperty]
        private bool isLoginVisible = true;

        // Visbility Customer Data (default View after successful login)
        [ObservableProperty] private bool isCustomersVisible = false;

        // Visibility New Customer Mask
        [ObservableProperty]
        private bool isNewCustomerVisible = false;

        // Visibility Edit Customer Mask
        [ObservableProperty]
        private bool isEditCustomerVisible;


        // User Navigation Message
        [ObservableProperty]
        private string userMessage = "Bitte einloggen oder registrieren.";


        // CONSTRUCTOR
        public MainWindowViewModel()
        {
            LoginVM.OnLoginSuccess += HandleLoginSuccess;

            // Event from NewCustomerViewModel:
            NewCustomerVM.OnCustomerCreated += () => {
                CustomersVM.LoadCustomers();
            };
        }


        // METHODS

        // ----On Login Success
        private void HandleLoginSuccess(string loggedInUserName)
        {
            // header message
            UserMessage = "Willkommen " + loggedInUserName +" !";

            // enable menu buttons
            IsMenuEnabled = true;

            // change login visibility and default visibility
            IsLoginVisible = false;
            IsCustomersVisible = true;

            // empty textboxes
            LoginVM.Username = "";
            LoginVM.Password = "";
        }

        // ----Relay Command: Button click "Kundendaten anzeigen" - change visibility
        [RelayCommand]
        private void ShowCustomersView()
        {

            IsNewCustomerVisible = false;
            IsCustomersVisible = true;
            IsEditCustomerVisible = false;
        }

        // ----Relay Command: Button  click "Neukunde anlegen" - change visibility
        [RelayCommand] private void ShowNewCustomerView()
        {
            IsCustomersVisible = false;
            IsNewCustomerVisible = true;
            IsEditCustomerVisible = false;
        }


        // ----Relay Command: Butno click "Kunde bearbeiten" - change visibility
        [RelayCommand]
        private void ShowEditCustomerView()
        {
            // check if customer = selected (marked in customer view)
            if (CustomersVM.SelectedCustomer == null)
            {
                UserMessage = "Bitte zuerst einen Kunden in der Liste auswählen.";
                return;
            }

            // Empty Message Box
            UserMessage = string.Empty;

            // Create edit viewmodel and pass on selected customer
            EditCustomerVM = new EditCustomerViewModel(CustomersVM.SelectedCustomer);

            // Subscribe to Event Event to update list after save
            EditCustomerVM.OnCustomerEdited += () => // defined here via lambda
            {
                CustomersVM.LoadCustomers(); // reload customers
                ShowCustomersView();         // back to customers view
            };

            // Change visibilities of views
            IsLoginVisible = false;
            IsCustomersVisible = false;
            IsNewCustomerVisible = false;
            IsEditCustomerVisible = true;
        }


        // ----Relay Command: Button click "Kunde löschen"
        [RelayCommand]
        private void DeleteCustomer()
        {
            // Change visibilities of views
            IsNewCustomerVisible = false;
            IsCustomersVisible = true;

            // delete method from customers view model
            CustomersVM.DeleteCustomer();
        }


        // ----Relay Command: Button click  "Beenden"
        [RelayCommand]
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }


        // End Class
    }
}