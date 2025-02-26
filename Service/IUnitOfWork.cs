using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepository;
namespace Service
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        ICustomizeProductRepository CustomizeProductRepository { get; }
        IDesignAreaRepository DesignAreaRepository { get; }
        IDesignElementRepository DesignElementRepository { get; }
        IFeedbackRepository FeedbackRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderStageRepository OrderStageRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IUserRepository UserRepository { get; }
        IProductRepository ProductRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task<int> SaveChangesAsync();
        void Dispose();

    }
}
