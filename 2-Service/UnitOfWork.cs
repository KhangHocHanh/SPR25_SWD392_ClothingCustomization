using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Repository.IRepository;
using _3_Repository.Repository;
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
        private IFeedbackRepository _feedbackRepository;
        private IRoleRepository _roleRepository;

        public UnitOfWork(ClothesCusShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UnitOfWork()
        {
            _userRepository ??= new UserRepository();
            _productRepository ??= new ProductRepository();
            _feedbackRepository ??= new FeedbackRepository();
            _roleRepository ??= new RoleRepository();
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

        public IFeedbackRepository FeedbackRepository
        {
            get
            {
                return _feedbackRepository ??= new FeedbackRepository(_context);
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
