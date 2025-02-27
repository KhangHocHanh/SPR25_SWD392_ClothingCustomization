using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<Feedback?> GetByIdAsync(int id);
        Task AddAsync(Feedback feedback);
        void Update(Feedback feedback);
        void Delete(Feedback feedback);
    }
}
