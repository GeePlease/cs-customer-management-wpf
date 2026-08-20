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

        // Event Method
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox pwdBox)
            {
                vm.Password = pwdBox.Password;
            }
        }
    }
}