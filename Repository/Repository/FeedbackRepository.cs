using BusinessObject.Model;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private ClothesCusShopContext context;

        public FeedbackRepository()
        {
        }

        public FeedbackRepository(ClothesCusShopContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Feedback feedback)
        {
            await _dbSet.AddAsync(feedback);
        }

        public void Delete(Feedback feedback)
        {
            _dbSet.Remove(feedback);
        }
        public void Update(Feedback feedback)
        {
            _dbSet.Update(feedback);
        }
    }
}
