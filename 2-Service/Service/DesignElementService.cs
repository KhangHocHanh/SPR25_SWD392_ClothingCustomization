using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Repository.IRepository;
using BusinessObject.Model;

namespace _2_Service.Service
{
    public interface IDesignElementService
    {
        Task<IEnumerable<DesignElement>> GetAllDesignElements();
        Task<DesignElement> GetDesignElementById(int id);
        Task AddDesignElement(DesignElement designElement);
        Task UpdateDesignElement(DesignElement designElement);
        Task DeleteDesignElement(int id);
    }
    public class DesignElementService : IDesignElementService
    {
        private readonly IDesignElementRepository _designElementRepository;
        public DesignElementService(IDesignElementRepository designElementRepository)
        {
            _designElementRepository = designElementRepository;
        }
        public async Task AddDesignElement(DesignElement designElement)
        {
            await _designElementRepository.AddAsync(designElement);
        }

        public async Task DeleteDesignElement(int id)
        {
            await _designElementRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DesignElement>> GetAllDesignElements()
        {
            return await _designElementRepository.GetAllAsync();
        }

        public Task<DesignElement> GetDesignElementById(int id)
        {
            return _designElementRepository.GetByIdAsync(id);
        }

        public async Task UpdateDesignElement(DesignElement designElement)
        {
            await _designElementRepository.UpdateAsync(designElement);
        }
    }

}
