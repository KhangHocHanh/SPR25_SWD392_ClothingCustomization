using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repository
{
    public class CustomizeProductRepository : GenericRepository<CustomizeProduct>, ICustomizeProductRepository
    {
        public CustomizeProductRepository() { }

        public CustomizeProductRepository(ClothesCusShopContext context) => _context = context;

        public async Task AddAsync(CustomizeProduct customizeProduct)
        {
            _context.AddAsync(customizeProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customizeProduct = await _context.CustomizeProducts.FindAsync(id);
            if (customizeProduct != null)
            {
                _context.CustomizeProducts.Remove(customizeProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(CustomizeProduct customizeProduct)
        {
            _context.CustomizeProducts.Update(customizeProduct);
            await _context.SaveChangesAsync();
        }

        Task ICustomizeProductRepository.UpdateAsync(CustomizeProduct customizeProduct)
        {
            throw new NotImplementedException();
        }
        public async Task<CustomizeProduct> GetByIdAsync(int id)
        {
            return await _context.CustomizeProducts.FindAsync(id);
        }

        public async Task<IEnumerable<CustomizeProduct>> GetAllAsync()
        {
            return await _context.CustomizeProducts.ToListAsync();
        }
    }
}