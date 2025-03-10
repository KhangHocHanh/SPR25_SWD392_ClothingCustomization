using BusinessObject.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Repository.IRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(int id);
        Task<Role> GetIdByNameAsync(string name);
        Task AddAsync(Role role);
        Task DeleteAsync(int id);
        Task UpdateAsync(Role role);
        Task<bool> IsRoleUsedAsync(int id);
    }
}
