using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoldDigger.Data;
using GoldDigger.Model;
using System.Collections.ObjectModel;
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
            using (var db = new AppDbContext()) // open safe db connection and close automat.
            {
                var customersFromDb = db.Customers.ToList();

                //create new collection with property
                // 1!! UI signal only
                CustomersList = new ObservableCollection<Customer>(customersFromDb);
            }
        }

        // ----Relay Command: Kunde löschen Button
        [RelayCommand]
        public void DeleteCustomer()
        {
            // check if customer selected
            if (SelectedCustomer == null)
            {
                MessageBox.Show("Bitte wählen Sie zuerst einen Kunden aus.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // safety message check (MessageBox)
            var result = MessageBox.Show(
                $"Möchten Sie den Kunden {SelectedCustomer.FirstName} {SelectedCustomer.LastName} wirklich löschen?",
                "Kunden löschen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    // EF Core: object from former DB-bquery(detached),
                    // search via id, then delete
                    var customerToDelete = db.Customers.Find(SelectedCustomer.CustomerId);
                    if (customerToDelete != null)
                    {
                        db.Customers.Remove(customerToDelete);
                        db.SaveChanges();
                    }
                }

                // remove from observable collection for ui refresh
                CustomersList.Remove(SelectedCustomer);
            }
        }
    }
}