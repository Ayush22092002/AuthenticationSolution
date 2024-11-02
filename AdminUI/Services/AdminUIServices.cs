using AuthenticationDll.Models;
namespace AdminUI.Services
{

    public interface IAdminUIServices
    {
        Task<List<User>> GetAll();
       // Task DeleteUser(int id);
    }
    public class AdminUIServices : IAdminUIServices
    {
        private readonly HttpClient _httpClient;

        public AdminUIServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5218/api/");
        }
       

        private Task<List<User>?> GetAll()
        {
            return _httpClient.GetFromJsonAsync<List<User>>("Authentication");
        }
    }
       

}
