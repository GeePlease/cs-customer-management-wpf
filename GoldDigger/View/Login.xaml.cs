using System.Windows;
using System.Windows.Controls;
using GoldDigger.ViewModel;

namespace GoldDigger.View
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        // Event Method for Password Box Binding (normal text binding not allowed for safety)
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox pwdBox)
            {
                vm.Password = pwdBox.Password;
            }
        }

        // Event Method for Password Box Confirmation Binding (normal text binding not allowed for safety)
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}