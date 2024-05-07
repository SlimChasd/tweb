using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Application
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для проверки учетных данных пользователя
        public Customer AuthenticateUser(string username, string password)
        {
            var user = _context.Customers.FirstOrDefault(c => c.Username == username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        // Метод для проверки роли пользователя
        public bool HasRequiredRole(Customer user, string role)
        {
            return user.Role.Equals(role, StringComparison.OrdinalIgnoreCase);
        }

        // Метод для регистрации нового пользователя
        public Customer RegisterUser(string username, string password)
        {
            var passwordHash = HashPassword(password);

            var newUser = new Customer
            {
                Username = username,
                PasswordHash = passwordHash,
                Role = "User"
            };

            _context.Customers.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        // Метод для хеширования пароля
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Метод для проверки совпадения пароля
        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }

        public Customer RegisterUser(string username, string password, string firstName, string lastName, string email, string phoneNumber)
        {
            var passwordHash = HashPassword(password);

            var newUser = new Customer
            {
                Username = username,
                PasswordHash = passwordHash,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Role = "User"
            };

            _context.Customers.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

    }
}
