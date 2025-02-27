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
    public class DesignAreaRepository : GenericRepository<DesignArea>, IDesignAreaRepository
    {
        public DesignAreaRepository() { }

        public DesignAreaRepository(ClothesCusShopContext context) => _context = context;

        public async Task AddDesignAreaAsync(DesignArea designArea)
        {
            _context.Add(designArea);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDesignAreaAsync(int id)
        {
            var designArea = await _context.DesignAreas.FindAsync(id);
            if (designArea != null)
            {
                _context.DesignAreas.Remove(designArea);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DesignArea>> GetAllDesignAreaAsync()
        {
            return await _context.DesignAreas.ToListAsync();
        }

        public async Task<DesignArea> GetDesignAreaByIdAsync(int id)
        {
            return await _context.DesignAreas.FindAsync(id);
        }

        public async Task UpdateDesignAreaAsync(DesignArea designArea)
        {
            _context.Update(designArea);
            await _context.SaveChangesAsync();
        }
    }
}