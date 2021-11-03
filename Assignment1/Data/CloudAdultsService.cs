using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Assignment1.Models;


namespace Assignment1.Data
{
    public class CloudAdultsService:IAdultsData
    {
        private string uri = "https://localhost:5003";

        private readonly HttpClient client;
        
        public CloudAdultsService()
        {

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyError) =>
            {
                return true;
            };
            client = new HttpClient(handler);
        }
        
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri+"/Adults");
            CheckException(response);
            string message = await response.Content.ReadAsStringAsync();
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return result;
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            string adultAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(adultAsJson,
                Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(uri+"/Adults", content);
            CheckException(response);
            
            var objAsJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Adult>(objAsJson, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{uri}/Adults/{adultId}");
            CheckException(response);
        }

        public async Task<Adult> EditAsync(Adult adult)
        {
            string adultAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(adultAsJson, Encoding.UTF8,
                "application/json");
            var response = await client.PatchAsync(uri + $"/Adults/{adult.Id}", content);
            CheckException(response);
            var objAsJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Adult>(objAsJson, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<Adult> GetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/Adults/{id}");
            CheckException(response);
            var objAsJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Adult>(objAsJson, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        
        private void CheckException(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error:{response.StatusCode},{response.ReasonPhrase}");
        }
        
    }
    
}