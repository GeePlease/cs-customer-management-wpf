using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoldDigger.Data;
using GoldDigger.Model;
using GoldDigger.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GoldDigger.ViewModel
{
    public partial class CustomersViewModel : ObservableObject
    {
        // ATTRIBUTES

        [ObservableProperty]
        private ObservableCollection<Customer> _customersList = new ObservableCollection<Customer>();

        [ObservableProperty]
        private Customer _selectedCustomer;

        // CONSTRUCTOR

        public CustomersViewModel()
        {
            // load customer data
            LoadCustomers();
        }

        // METHODS

        // ---- load customers method to show in datagrid
        public void LoadCustomers()
        {
            using (var db = new AppDbContext())
            {
                var customersFromDb = db.Customers.ToList();

                // HIER: Nutze die vom Toolkit generierte Property (Großgeschrieben) 
                // und erzeuge direkt eine neue Collection. Das sendet nur EINMALEIG ein Signal an die UI!
                CustomersList = new ObservableCollection<Customer>(customersFromDb);
            }
        }

        // ----Relay Command: Kunde löschen button
        [RelayCommand]
        public void DeleteCustomer()
        {
            // Prüfen, ob überhaupt ein Kunde ausgewählt wurde
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Bitte wählen Sie zuerst einen Kunden aus.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Sicherheitsfrage (MessageBox)
            var result = MessageBox.Show(
                $"Möchten Sie den Kunden {SelectedCustomer.FirstName} {SelectedCustomer.LastName} wirklich löschen?",
                "Kunden löschen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    // WICHTIG bei EF Core: Da das Objekt aus einer vorherigen DB-Abfrage stammt (detached),
                    // suchen wir es am besten über die ID frisch aus der Datenbank und löschen es dann.
                    var customerToDelete = db.Customers.Find(SelectedCustomer.CustomerId);
                    if (customerToDelete != null)
                    {
                        db.Customers.Remove(customerToDelete);
                        db.SaveChanges();
                    }
                }

                // Sofort aus der lokalen ObservableCollection entfernen, damit die UI aktualisiert wird
                CustomersList.Remove(SelectedCustomer);
            }
        }
    }
}