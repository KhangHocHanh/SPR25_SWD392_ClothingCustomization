﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository() { }
        public RoleRepository(ClothesCusShopContext context) => _context = context;
    }
}
