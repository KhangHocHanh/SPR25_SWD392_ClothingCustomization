using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class OrderStageRepository : GenericRepository<OrderStage>, IOrderStageRepository
    {
        public OrderStageRepository() { }
        public OrderStageRepository(ClothesCusShopContext context) => _context = context;
    }
}
