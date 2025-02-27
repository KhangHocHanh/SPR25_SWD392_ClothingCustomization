using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository;
using Repository.IRepository;

namespace Service.Service
{
    public interface ICustomizeProductService
    {
        Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProducts();
        Task<CustomizeProduct> GetCustomizeProductById(int id);
        Task AddCustomizeProduct(CustomizeProduct customizeProduct);
        Task UpdateCustomizeProduct(CustomizeProduct customizeProduct);
        Task DeleteCustomizeProduct(int id);
    }
    public class CustomizeProductService : ICustomizeProductService
    {
        private readonly ICustomizeProductRepository _customizeProductRepository;
        public CustomizeProductService(ICustomizeProductRepository customizeProductRepository)
        {
            _customizeProductRepository = customizeProductRepository;
        }
        public async Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProducts()
        {
            return await _customizeProductRepository.GetAllAsync();
        }

        public async Task<CustomizeProduct> GetCustomizeProductById(int id)
        {
            return await _customizeProductRepository.GetByIdAsync(id);
        }

        public async Task AddCustomizeProduct(CustomizeProduct customizeProduct)
        {
            await _customizeProductRepository.AddAsync(customizeProduct);
        }

        public async Task UpdateCustomizeProduct(CustomizeProduct customizeProduct)
        {
            await _customizeProductRepository.UpdateAsync(customizeProduct);
        }

        public async Task DeleteCustomizeProduct(int id)
        {
            await _customizeProductRepository.DeleteAsync(id);
        }
    }
}