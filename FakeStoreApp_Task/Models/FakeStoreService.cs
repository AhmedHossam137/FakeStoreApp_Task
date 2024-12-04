using FakeStoreApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FakeStoreApp.Services
{
    public class FakeStoreService : IFakeStoreService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FakeStoreService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Product>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://fakestoreapi.com/products");
            response.EnsureSuccessStatusCode();

            var products = JsonConvert.DeserializeObject<List<Product>>(await response.Content.ReadAsStringAsync());
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://fakestoreapi.com/products/{id}");
            response.EnsureSuccessStatusCode();

            var product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
            return product;
        }

        public async Task<List<string>> GetCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://fakestoreapi.com/products/categories");
            response.EnsureSuccessStatusCode();

            var categories = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
            return categories;
        }
    }
}
