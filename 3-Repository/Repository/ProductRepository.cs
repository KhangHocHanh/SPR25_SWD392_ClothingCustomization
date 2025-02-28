using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Model;
using Repository.IRepository;

namespace Repository.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ClothesCusShopContext context;

        public ProductRepository()
        {
        }

        public ProductRepository(ClothesCusShopContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _dbSet.AddAsync(product);
        }

        public void Delete(Product product)
        {
            _dbSet.Remove(product);
        }
        public void Update(Product product)
        {
            _dbSet.Update(product);
        }
    }
}
