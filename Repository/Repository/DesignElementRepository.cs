using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace Repository.Repository
{
    public class DesignElementRepository : GenericRepository<DesignElement>, IDesignElementRepository
    {
        public DesignElementRepository() { }

        public DesignElementRepository(ClothesCusShopContext context) => _context = context;

        public async Task<IEnumerable<DesignElement>> GetAllAsync()
        {
            return await _context.DesignElements.ToListAsync();
        }

        public async Task<DesignElement> GetByIdAsync(int id)
        {
            return await _context.DesignElements.FindAsync(id);
        }

        public async Task AddAsync(DesignElement designElement)
        {
            await _context.DesignElements.AddAsync(designElement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DesignElement designElement)
        {
            _context.DesignElements.Update(designElement);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var designElement = await _context.DesignElements.FindAsync(id);
            if (designElement != null)
            {
                _context.DesignElements.Remove(designElement);
                await _context.SaveChangesAsync();
            }
        }
    }
}