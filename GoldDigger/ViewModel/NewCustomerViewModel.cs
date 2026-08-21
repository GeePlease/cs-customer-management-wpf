using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoldDigger.Data;
using GoldDigger.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows.Controls;

namespace GoldDigger.ViewModel
{
    public partial class NewCustomerViewModel : ObservableObject
    {

        // ATTRIBUTES & PROPERTIES

        // Reference to update customer list immediately
        private readonly CustomersViewModel _customersViewModel;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string street;

        [ObservableProperty]
        private string streetNumber;

        [ObservableProperty]
        private string postCode;

        [ObservableProperty]
        private string residence;

        [ObservableProperty]
        private string mail;

        [ObservableProperty]
        private string message;

        // EVENTS
        
        public event Action OnCustomerCreated;

        // CONSTRUCTOR

        public NewCustomerViewModel()
        {
        }

        // METHODS

        //----Relay Command: Create new Customer (button click "Anlegen")
        [RelayCommand]
        public void CreateNewCustomer()
        {
            // check if any of the fields are empty
            if (string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Street) ||
                string.IsNullOrWhiteSpace(StreetNumber) ||
                string.IsNullOrWhiteSpace(PostCode) ||
                string.IsNullOrWhiteSpace(Residence) ||
                string.IsNullOrWhiteSpace(Mail))
            {
                Message = "Bitte alle Felder ausfüllen!";
                return;
            }

            // create new customer
            var newCustomer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Street = Street,
                StreetNumber = StreetNumber,
                PostCode = PostCode,
                Residence = Residence,
                Mail = Mail
            };

            // connect to db and save customer in db
            using (var db = new AppDbContext())
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }

            // refresh changed customer list in ui
            OnCustomerCreated?.Invoke();

            // empty text fields
            FirstName = "";
            LastName = "";
            Street = "";
            StreetNumber = "";
            PostCode = "";
            Residence = "";
            Mail = "";

            // success message
            Message = "Kunde erfolgreich angelegt!";

        }


        // END CLASS
    }
}
