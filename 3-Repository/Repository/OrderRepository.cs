using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using _3_Repository.IRepository;
using Repository.IRepository;

namespace Repository.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ClothesCusShopContext _context;

        public OrderRepository(ClothesCusShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckCustomizeProductExists(int customizeProductId)
        {
            return await _context.CustomizeProducts.AnyAsync(p => p.CustomizeProductId == customizeProductId);
        }


        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }


        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<CustomizeProduct?> GetCustomizeProductByIdAsync(int id)
        {
            return await _context.CustomizeProducts.FindAsync(id);
        }



    }
}
