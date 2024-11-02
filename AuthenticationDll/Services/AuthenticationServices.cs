using AuthenticationDll.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationDll.Services
{
    public interface IAuthenticationServices
    {
        int Add(User user);               // Add a new user
        void Update(User user);           // Update an existing user
        void Delete(int userId);          // Delete a user by ID
        User Get(int userId);             // Get a user by ID
        List<User> GetAll();              // Get all users

        Task<User> ValidateUserAsync(string username, string password);
    }

    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserDbContext _context;

        public AuthenticationServices(UserDbContext context)
        {
            _context = context;
        }

        // Add a new user
        public int Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Ensure that Password is handled securely
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        // Update an existing user
        public void Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var userEntity = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);

            if (userEntity == null)
            {
                throw new Exception("User not found to update.");
            }

            userEntity.Username = user.Username;
            userEntity.Password = user.Password; // Ensure password is handled securely

            _context.SaveChanges();
        }

        // Get a specific user by ID
        public User Get(int userId)
        {
            var userEntity = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (userEntity == null)
            {
                throw new Exception("User not found.");
            }

            return userEntity;
        }

        // Delete a user by ID
        public void Delete(int userId)
        {
            var userEntity = _context.Users.Find(userId);

            if (userEntity == null)
            {
                throw new Exception("User not found.");
            }

            _context.Users.Remove(userEntity);
            _context.SaveChanges();
        }

        // Get all users
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            // Simple username and password check without hashing
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
