using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Repository.IRepository;
using BusinessObject.Model;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _2_Service.Service
{
    public interface ICustomizeProductService
    {
        Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProducts();
        Task<CustomizeProduct> GetCustomizeProductById(int id);
        Task AddCustomizeProduct(CustomizeProduct customizeProduct);
        Task UpdateCustomizeProduct(CustomizeProduct customizeProduct);
        Task DeleteCustomizeProduct(int id);
        Task<List<ProductCustomizationCountDto>> GetProductCustomizationCounts();
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



        public async Task<List<ProductCustomizationCountDto>> GetProductCustomizationCounts()
        {
            return await _customizeProductRepository.GetProductCustomizationCounts();
        }
    }

}
