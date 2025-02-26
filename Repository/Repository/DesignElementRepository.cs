using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class DesignElementRepository : GenericRepository<DesignElement>, IDesignElementRepository
    {
        public DesignElementRepository() { }

        public DesignElementRepository(ClothesCusShopContext context) => _context = context;
    }
}
