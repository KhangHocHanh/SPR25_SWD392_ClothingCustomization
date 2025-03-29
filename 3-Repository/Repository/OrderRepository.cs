using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using _3_Repository.IRepository;
using Repository.IRepository;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace Repository.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ClothesCusShopContext _context;

        public OrderRepository(ClothesCusShopContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckCustomizeProductExists(int customizeProductId)
        {
            return await _context.CustomizeProducts.AnyAsync(p => p.CustomizeProductId == customizeProductId);
        }


        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }


        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<CustomizeProduct?> GetCustomizeProductByIdAsync(int id)
        {
            return await _context.CustomizeProducts.FindAsync(id);
        }

        public async Task<List<ProductOrderQuantityDto>> GetOrderedProductQuantities()
        {
            return await _context.Orders
                .Join(_context.CustomizeProducts,
                      o => o.CustomizeProductId,
                      cp => cp.CustomizeProductId,
                      (o, cp) => new { Order = o, CustomizeProduct = cp }) // Assign names to properties
                .Join(_context.Products,
                      ocp => ocp.CustomizeProduct.ProductId,
                      p => p.ProductId,
                      (ocp, p) => new { ocp.Order, p }) // Assign correct structure
                .GroupBy(x => new { x.p.ProductId, x.p.ProductName })
                .Select(g => new ProductOrderQuantityDto
                {
                    Product_ID = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    TotalOrderedQuantity = g.Sum(x => (x.Order.Quantity ?? 0)) // Correctly access order quantity
                })
                .OrderByDescending(x => x.TotalOrderedQuantity)
            .ToListAsync();
        }

        public async Task<List<OrderDetailDTO>> GetOrdersByCustomerIdAsync(int userId)
        {
            //First time
            //return await _context.Orders
            //    .Join(_context.CustomizeProducts,
            //          o => o.CustomizeProductId,
            //          cp => cp.CustomizeProductId,
            //          (o, cp) => new { Order = o, UserId = cp.UserId }) // Fetch UserId from CustomizeProduct
            //    .Where(x => x.UserId == userId) // Filter by UserId
            //    .GroupJoin(_context.OrderStages,
            //               oc => oc.Order.OrderId,
            //               os => os.OrderId,
            //               (oc, os) => new { oc.Order, OrderStages = os }) // Left join with OrderStages
            //    .Select(x => new OrderDetailDTO
            //    {
            //        Order_ID = x.Order.OrderId,
            //        CustomizeProduct_ID = x.Order.CustomizeProductId,
            //        OrderDate = x.Order.OrderDate,
            //        DeliveryDate = x.Order.DeliveryDate,
            //        RecipientName = x.Order.RecipientName,
            //        DeliveryAddress = x.Order.DeliveryAddress,
            //        ShippingMethod = x.Order.ShippingMethod,
            //        ShippingFee = (float?)x.Order.ShippingFee ?? 0f, // Cast & set default to 0
            //        Notes = x.Order.Notes,
            //        Price = x.Order.Price ?? 0m, // Set default to 0m
            //        Quantity = x.Order.Quantity ?? 0, // Set default to 0
            //        TotalPrice = x.Order.TotalPrice ?? 0m, // Set default to 0m
            //        LatestOrderStage = x.OrderStages.OrderByDescending(os => os.UpdatedDate)
            //                                .Select(os => os.OrderStageName)
            //                                .FirstOrDefault() // Fetch latest order stage
            //    })
            //    .OrderByDescending(x => x.OrderDate)
            //    .ToListAsync();

            //Second time
            //        return await _context.Orders
            //.Join(_context.CustomizeProducts,
            //      o => o.CustomizeProductId,
            //      cp => cp.CustomizeProductId,
            //      (o, cp) => new { Order = o, UserId = cp.UserId }) // Fetch UserId from CustomizeProduct
            //.Where(x => x.UserId == userId) // Filter by UserId
            //.Select(x => new
            //{
            //    x.Order,
            //    LatestOrderStage = _context.OrderStages
            //        .Where(os => os.OrderId == x.Order.OrderId) // Filter OrderStages for this order
            //        .OrderByDescending(os => os.UpdatedDate) // Order by latest UpdatedDate
            //        .Select(os => os.OrderStageName)
            //        .FirstOrDefault() // Get the latest order stage
            //})
            //.Select(x => new OrderDetailDTO
            //{
            //    Order_ID = x.Order.OrderId,
            //    CustomizeProduct_ID = x.Order.CustomizeProductId,
            //    OrderDate = x.Order.OrderDate,
            //    DeliveryDate = x.Order.DeliveryDate,
            //    RecipientName = x.Order.RecipientName,
            //    DeliveryAddress = x.Order.DeliveryAddress,
            //    ShippingMethod = x.Order.ShippingMethod,
            //    ShippingFee = (float?)x.Order.ShippingFee ?? 0f, // Cast & set default to 0
            //    Notes = x.Order.Notes,
            //    Price = x.Order.Price ?? 0m, // Set default to 0m
            //    Quantity = x.Order.Quantity ?? 0, // Set default to 0
            //    TotalPrice = x.Order.TotalPrice ?? 0m, // Set default to 0m
            //    LatestOrderStage = x.LatestOrderStage // Latest order stage from subquery
            //})
            //.OrderByDescending(x => x.OrderDate)
            //.ToListAsync();


            return await _context.Orders
    .Join(_context.CustomizeProducts,
          o => o.CustomizeProductId,
          cp => cp.CustomizeProductId,
          (o, cp) => new { Order = o, UserId = cp.UserId }) // Fetch UserId from CustomizeProduct
    .Where(x => x.UserId == userId) // Filter by UserId
    .GroupJoin(_context.OrderStages,
               oc => oc.Order.OrderId,
               os => os.OrderId,
               (oc, os) => new {
                   oc.Order,
                   LatestOrderStage = os
                                   .OrderByDescending(os => os.UpdatedDate) // Order by latest update
                                   .Select(os => os.OrderStageName)
                                   .FirstOrDefault()
               }) // Fetch latest order stage
    .Select(x => new OrderDetailDTO
    {
        Order_ID = x.Order.OrderId,
        CustomizeProduct_ID = x.Order.CustomizeProductId,
        OrderDate = x.Order.OrderDate,
        DeliveryDate = x.Order.DeliveryDate,
        RecipientName = x.Order.RecipientName,
        DeliveryAddress = x.Order.DeliveryAddress,
        ShippingMethod = x.Order.ShippingMethod,
        ShippingFee = (float?)x.Order.ShippingFee ?? 0f, // Cast & set default to 0
        Notes = x.Order.Notes,
        Price = x.Order.Price ?? 0m, // Set default to 0m
        Quantity = x.Order.Quantity ?? 0, // Set default to 0
        TotalPrice = x.Order.TotalPrice ?? 0m, // Set default to 0m
        LatestOrderStage = x.LatestOrderStage // Now this is correctly included before Select()
    })
    .OrderByDescending(x => x.OrderDate)
    .ToListAsync();


        }




    }
}
