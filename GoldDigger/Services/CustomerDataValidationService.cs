using System.Text.RegularExpressions;

namespace GoldDigger.Services
{
    public static class CustomerValidationService
    {
        public static bool ValidateCustomer(
            string firstName,
            string lastName,
            string street,
            string streetNumber,
            string postCodeStr,
            string residence,
            string mail,
            out string errorMessage)
        {
            errorMessage = string.Empty;

            // 1.vorname, 1 - 50 chars
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > 50)
            {
                errorMessage = "Der Vorname muss zwischen 1 und 50 Zeichen lang sein.";
                return false;
            }

            if (Regex.IsMatch(firstName, @"\d"))
            {
                errorMessage = "Vorname darf keine Zahlen enthalten.";
                return false;
            }

            // 2. Nachname: 1 - 100 chars
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > 100)
            {
                errorMessage = "Der Nachname muss zwischen 1 und 100 Zeichen lang sein.";
                return false;
            }

            if (Regex.IsMatch(lastName, @"\d"))
            {
                errorMessage = "Nachname darf keine Zahlen enthalten.";
                return false;
            }

            // 3. Straße: 1 - 100 chars
            if (string.IsNullOrWhiteSpace(street) || street.Length > 100)
            {
                errorMessage = "Die Straße muss zwischen 1 und 100 Zeichen lang sein.";
                return false;
            }

            // 4. Hausnummer: 1 - 5 chars
            if (string.IsNullOrWhiteSpace(streetNumber) || streetNumber.Length > 5)
            {
                errorMessage = "Die Hausnummer muss zwischen 1 und 5 Zeichen lang sein.";
                return false;
            }

            // 5. PLZ: integer bewettn 1000 - 9999 - valid in austria
            if (!int.TryParse(postCodeStr, out int postCode) || postCode < 1000 || postCode > 9999)
            {
                errorMessage = "Die PLZ muss eine gültige österreichische Postleitzahl sein (1000–9999).";
                return false;
            }

            // 6. Wohnort: 1 - 100 letter hars
            if (string.IsNullOrWhiteSpace(residence) || residence.Length > 100)
            {
                errorMessage = "Der Wohnort muss zwischen 1 und 100 Zeichen lang sein.";
                return false;
            }

            if (Regex.IsMatch(residence, @"\d"))
            {
                errorMessage = "Der Wohnort darf keine Zahlen enthalten.";
                return false;
            }

            // 7. Einfache E-Mail Validierung
            if (string.IsNullOrWhiteSpace(mail))
            {
                errorMessage = "E-Mail-Adresse darf nicht leer sein.";
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(mail, emailPattern))
            {
                errorMessage = "Bitte gültige E-Mail-Adresse eingeben.";
                return false;
            }

            return true; 
        }

    // END CLASS
    }
}