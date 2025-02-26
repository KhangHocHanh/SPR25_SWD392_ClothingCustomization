﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;

namespace Repository.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
      Task<User> GetUserAccount(string username, string password);

    }
}
