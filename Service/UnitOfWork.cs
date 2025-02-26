using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;
using Repository.Repository;

namespace Service
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ClothesCusShopContext _context;


        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ICustomizeProductRepository _customizeProductRepository;
        private IDesignAreaRepository _designAreaRepository;
        private IDesignElementRepository _designElementRepository;
        private IFeedbackRepository _feedbackRepository;
        private INotificationRepository _notificationRepository;
        private IOrderRepository _orderRepository;
        private IOrderStageRepository _orderStageRepository;
        private IPaymentRepository _paymentRepository;
        private IRoleRepository _roleRepository;




        public UnitOfWork(ClothesCusShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UnitOfWork()
        {
            _userRepository ??= new UserRepository();
            _productRepository ??= new ProductRepository();
            _categoryRepository ??= new CategoryRepository();
            _customizeProductRepository ??= new CustomizeProductRepository();
            _designAreaRepository ??= new DesignAreaRepository();
            _customizeProductRepository ??= new CustomizeProductRepository();
            _designElementRepository ??= new DesignElementRepository();
            _feedbackRepository ??= new FeedbackRepository();
            _notificationRepository ??= new NotificationRepository();
            _orderRepository ??= new OrderRepository();
            _orderStageRepository ??= new OrderStageRepository();
            _paymentRepository ??= new PaymentRepository();
        }
        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository ??= new UserRepository(_context);
            }
        }
         public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new ProductRepository(_context);
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new CategoryRepository(_context);
            }
        }

        public ICustomizeProductRepository CustomizeProductRepository
        {
            get
            {
                return _customizeProductRepository ??= new CustomizeProductRepository(_context);
            }
        }

        public IDesignAreaRepository DesignAreaRepository
        {
            get
            {
                return _designAreaRepository ??= new DesignAreaRepository(_context);
            }
        }

        public IDesignElementRepository DesignElementRepository
        {
            get
            {
                return _designElementRepository ??= new DesignElementRepository(_context);
            }
        }


        public IFeedbackRepository FeedbackRepository
        {
            get
            {
                return _feedbackRepository ??= new FeedbackRepository(_context);
            }
        }


        public INotificationRepository NotificationRepository
        {
            get
            {
                return _notificationRepository ??= new NotificationRepository(_context);
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ??= new OrderRepository(_context);
            }
        }

        public IOrderStageRepository OrderStageRepository
        {
            get
            {
                return _orderStageRepository ??= new OrderStageRepository(_context);
            }
        }

        public IPaymentRepository PaymentRepository
        {
            get
            {
                return _paymentRepository ??= new PaymentRepository(_context);
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ??= new RoleRepository(_context);
            }
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _context.SaveChangesAsync();
        }
    }
}
