using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class DesignAreaRepository : GenericRepository<DesignArea>, IDesignAreaRepository
    {
        public DesignAreaRepository() { }

        public DesignAreaRepository(ClothesCusShopContext context) => _context = context;
    }
}
