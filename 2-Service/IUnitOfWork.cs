using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Repository.IRepository;
using Repository.IRepository;
namespace Service
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        Task<int> SaveChangesAsync();
        void Dispose();

    }
}
