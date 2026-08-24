using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GoldDigger.Data;
using GoldDigger.Model;
using System;
using System.IO;
using System.Security.Policy;

namespace GoldDigger.ViewModel
{
    public partial class EditCustomerViewModel : ObservableObject
    {

        // ATTRIBUTES & PROPERTIES

        private readonly int _customerId;

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

        public event Action OnCustomerEdited;

        // CONSTRUCTOR

        public EditCustomerViewModel(Customer customerToEdit)
        {
            if (customerToEdit != null)
            {
                _customerId = customerToEdit.CustomerId; // store ID to reload customer data

                // fill text boxes with customer data
                FirstName = customerToEdit.FirstName;
                LastName = customerToEdit.LastName;
                Street = customerToEdit.Street;
                StreetNumber = customerToEdit.StreetNumber;
                PostCode = customerToEdit.PostCode;
                Residence = customerToEdit.Residence;
                Mail = customerToEdit.Mail;
            }
        }

        // METHODS

        //----Relay Command: Edit Customer (button click "Speichern")
        [RelayCommand]
        public void EditCustomer()
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

            // connect to db and update customer in db
            using (var db = new AppDbContext())
            {
                var customerToUpdate = db.Customers.Find(_customerId);

                if (customerToUpdate != null)
                {
                    customerToUpdate.FirstName = FirstName;
                    customerToUpdate.LastName = LastName;
                    customerToUpdate.Street = Street;
                    customerToUpdate.StreetNumber = StreetNumber;
                    customerToUpdate.PostCode = PostCode;
                    customerToUpdate.Residence = Residence;
                    customerToUpdate.Mail = Mail;

                    db.SaveChanges();
                }
            }

            // refresh changed customer list in ui
            OnCustomerEdited?.Invoke();

            // success message
            Message = "Änderungen erfolgreich gespeichert!";
        }

        // END CLASS
    }
}