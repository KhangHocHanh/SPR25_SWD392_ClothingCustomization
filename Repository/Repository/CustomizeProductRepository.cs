using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class CustomizeProductRepository : GenericRepository<CustomizeProduct>, ICustomizeProductRepository
    {
        public CustomizeProductRepository() { }

        public CustomizeProductRepository(ClothesCusShopContext context) => _context = context;
    }
}
