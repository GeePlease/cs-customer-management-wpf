using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GoldDigger.ViewModel
{

    public partial class MainWindow: ObservableObject
    {

        // METHODS

        // Relay Command: Button "Beenden"
        [RelayCommand]
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }



    // END CLASS
    }
}