using FakeStoreApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeStoreApp.Services
{
    public interface IFakeStoreService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<List<string>> GetCategories();
    }
}
