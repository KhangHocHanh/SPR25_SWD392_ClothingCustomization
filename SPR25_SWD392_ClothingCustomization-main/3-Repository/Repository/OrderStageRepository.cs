using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class OrderStageRepository : GenericRepository<OrderStage>, IOrderStageRepository
    {
        private readonly ClothesCusShopContext _context;

        public OrderStageRepository(ClothesCusShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<OrderStage>> GetAllOrderStagesAsync()
        {
           
            return await _context.OrderStages.ToListAsync();

        }

        public async Task<OrderStage?> GetOrderStageByIdAsync(int id)
        {
            
            return await _context.OrderStages.FirstOrDefaultAsync(o => o.OrderStageId == id);
        }

        public async Task AddOrderStageAsync(OrderStage orderStage)
        {
            await _context.OrderStages.AddAsync(orderStage);
        }

        public async Task UpdateOrderStageAsync(OrderStage orderStage)
        {
            _context.OrderStages.Update(orderStage);
        }

        public async Task DeleteOrderStageAsync(OrderStage orderStage)
        {
            _context.OrderStages.Remove(orderStage);
        }
    }
}
