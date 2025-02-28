using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository() { }

        public UserRepository(ClothesCusShopContext context) => _context = context;
    }
}
