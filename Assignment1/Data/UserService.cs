using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment1.Models;

namespace Assignment1.Data
{
    public class UserService:IUserService
    {
        private string uri = "https://localhost:5003";
        
        private readonly HttpClient client;
        
        public UserService()
        {

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyError) =>
            {
                return true;
            };
            client = new HttpClient(handler);
        }
        
        public async Task<User> ValidateUserAsync(string userName, string password)
        {
            string userAsJson = JsonSerializer.Serialize(new User() {UserName = userName, Password = password});
            HttpContent content = new StringContent(userAsJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri + $"/Users?userName={userName}&password={password}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error:{response.StatusCode},{response.ReasonPhrase}");
            
            var objAsJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(objAsJson, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        }
    }
}