using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Repository.IRepository;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace _3_Repository.Repository
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
            _context.CustomizeProducts.Update(customizeProduct);
            return _context.SaveChangesAsync();
        }
        public async Task<CustomizeProduct> GetByIdAsync(int id)
        {
            return await _context.CustomizeProducts
       .Include(cp => cp.DesignElements)
       .Include(cp => cp.Product)
       .Include(cp => cp.User)
       .FirstOrDefaultAsync(cp => cp.CustomizeProductId == id);
        }

        public async Task<IEnumerable<CustomizeProduct>> GetAllAsync()
        {
            return await _context.CustomizeProducts
        .Include(cp => cp.DesignElements)  // Lấy danh sách các DesignElement liên quan
        .Include(cp => cp.Product)         // Lấy thông tin sản phẩm
        .Include(cp => cp.User)            // Lấy thông tin người dùng
        .ToListAsync();
        }
    }

}
