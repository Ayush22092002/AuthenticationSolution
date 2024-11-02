using AuthenticationDll.Models;
using AuthenticationDll.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationWebApi.DataServices
{
    public class AuthenticationDataServices
    {
        private readonly IAuthenticationServices _authenticationServices;

        public AuthenticationDataServices(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        // Asynchronous method to get a specific user
        public Task<User> GetUserAsync(int userId)
        {
            return Task.Run(() => _authenticationServices.Get(userId));
        }

        // Asynchronous method to add a new user
        public Task<int> AddUserAsync(User user)
        {
            return Task.Run(() => _authenticationServices.Add(user));
        }

        // Asynchronous method to delete a user
        public Task DeleteUserAsync(int userId)
        {
            return Task.Run(() => _authenticationServices.Delete(userId));
        }

        // Asynchronous method to get all users
        public Task<List<User>> GetAllUsersAsync()
        {
            return Task.Run(() => _authenticationServices.GetAll());
        }

        // Asynchronous method to update a user
        public Task UpdateUserAsync(User user)
        {
            return Task.Run(() => _authenticationServices.Update(user));
        }
        // Asynchronous method to validate user by username and password
        public async Task<User> ValidateUserAsync(string username, string password)
        {
            return await _authenticationServices.ValidateUserAsync(username, password);
        }

    }
}
