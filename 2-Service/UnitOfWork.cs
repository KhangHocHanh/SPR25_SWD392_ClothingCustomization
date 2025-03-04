﻿using System;
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
        private IProductRepository _productRepository;
        private IUserRepository _userRepository;
        private IFeedbackRepository _feedbackRepository;
        private IRoleRepository _roleRepository;
        private ICategoryRepository _categoryRepository;

        public UnitOfWork(ClothesCusShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                _categoryRepository ??= new CategoryRepository(_context);
                return _categoryRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_context);
                return _productRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_context);
                return _userRepository;
            }
        }

        public IFeedbackRepository FeedbackRepository
        {
            get
            {
                _feedbackRepository ??= new FeedbackRepository(_context);
                return _feedbackRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                _roleRepository ??= new RoleRepository(_context);
                return _roleRepository;
            }
        }

        // Xóa dòng này đi:
        // object IUnitOfWork.CategoryRepository => throw new NotImplementedException();

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes to database", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }

}
