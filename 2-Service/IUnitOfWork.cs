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
        ICategoryRepository CategoryRepository { get; }  // Sửa kiểu object thành ICategoryRepository

        Task<int> SaveChangesAsync();
    }
}
