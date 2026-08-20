using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using GoldDigger.Data;
using GoldDigger.Model;

namespace GoldDigger.ViewModel
{
    public partial class CustomersViewModel : ObservableObject
    {
        // ATTRIBUTES
        [ObservableProperty]
        private ObservableCollection<Customer> _customersList = new ObservableCollection<Customer>();

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
    }
}