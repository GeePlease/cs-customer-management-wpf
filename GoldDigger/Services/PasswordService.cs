using Microsoft.AspNetCore.Identity;

namespace GoldDigger.Services
{
    public class PasswordService
    {

        // ATTRIBUTES
        // official ASP.NET Core PasswordHasher
        private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        // pw hashing
        public static string HashPassword(string password)
        {
            // user hasher
            return _passwordHasher.HashPassword(null, password);
        }

        // pw verifification
        public static bool VerifyPassword(string storedHash, string enteredPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, storedHash, enteredPassword);

            // Gibt true zurück, wenn das Passwort stimmt
            return result == PasswordVerificationResult.Success;
        }
    }
}