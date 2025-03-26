using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.Model;
using _3_Repository.IRepository;
using Repository.IRepository;
using System;
using Repository.Repository;

namespace _2_Service.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);

        Task<bool> CheckCustomizeProductExists(int customizeProductId);
        Task<decimal> CalculateRevenueAsync(int? day, int? month, int? year);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStageRepository _orderStageRepository;

        public OrderService(IOrderRepository orderRepository, IOrderStageRepository orderStageRepository)
        {
            _orderRepository = orderRepository;
            _orderStageRepository = orderStageRepository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        //public async Task AddOrderAsync(Order order)
        //{
        //    if (order.CustomizeProductId <= 0)
        //    {
        //        throw new ArgumentException("CustomizeProductId must be greater than 0.");
        //    }

        //    // Kiểm tra `CustomizeProductId` có tồn tại trong database hay không
        //    var existingProduct = await _orderRepository.GetCustomizeProductByIdAsync(order.CustomizeProductId);
        //    if (existingProduct == null)
        //    {
        //        throw new ArgumentException($"CustomizeProductId {order.CustomizeProductId} does not exist. Please provide a valid CustomizeProductId.");
        //    }

        //    if (order.Price <= 0 || order.Quantity <= 0 || order.TotalPrice <= 0)
        //    {
        //        throw new ArgumentException("Price, Quantity, and TotalPrice must be greater than zero.");
        //    }

        //    await _orderRepository.AddAsync(order);


        //    // Add order and ensure OrderId is generated
        //    await _orderRepository.AddAsync(order); // Save order

        //    // **Reload the order from DB to get the generated OrderId**
        //    var savedOrder = await _orderRepository.GetByIdAsync(order.OrderId);

        //    if (savedOrder == null)
        //    {
        //        throw new Exception("Failed to retrieve saved order.");
        //    }

        //    // Now, add order stage using the saved OrderId
        //    OrderStage orderStage = new OrderStage
        //    {
        //        OrderId = savedOrder.OrderId,
        //        OrderStageName = "Place Order",
        //        UpdatedDate = DateTime.Now
        //    };

        //    await _orderStageRepository.AddOrderStageAsync(orderStage);
        //}

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                if (order.CustomizeProductId <= 0)
                {
                    throw new ArgumentException("CustomizeProductId must be greater than 0.");
                }

                var existingProduct = await _orderRepository.GetCustomizeProductByIdAsync(order.CustomizeProductId);
                if (existingProduct == null)
                {
                    throw new ArgumentException($"CustomizeProductId {order.CustomizeProductId} does not exist. Please provide a valid CustomizeProductId.");
                }

                if (order.Price <= 0 || order.Quantity <= 0 || order.TotalPrice <= 0)
                {
                    throw new ArgumentException("Price, Quantity, and TotalPrice must be greater than zero.");
                }

                // Add order
                await _orderRepository.AddAsync(order);

                // Fetch the saved order
                var savedOrder = await _orderRepository.GetByIdAsync(order.OrderId);
                if (savedOrder == null)
                {
                    throw new Exception("Failed to retrieve saved order.");
                }

                // Add order stage
                OrderStage orderStage = new OrderStage
                {
                    OrderId = savedOrder.OrderId,
                    OrderStageName = "Place Order",
                    UpdatedDate = DateTime.Now
                };

                await _orderStageRepository.AddOrderStageAsync(orderStage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }


        public async Task UpdateOrderAsync(Order order)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(order.OrderId);
            if (existingOrder == null)
            {
                throw new Exception("Order not found.");
            }

            // Kiểm tra CustomizeProductId có hợp lệ không
            if (order.CustomizeProductId <= 0)
            {
                throw new ArgumentException("CustomizeProductId must be greater than 0.");
            }

            // Kiểm tra các giá trị quan trọng
            if (order.Price <= 0 || order.Quantity <= 0 || order.TotalPrice <= 0)
            {
                throw new ArgumentException("Price, Quantity, and TotalPrice must be greater than zero.");
            }

            await _orderRepository.UpdateAsync(order);
        }
        public async Task<bool> CheckCustomizeProductExists(int customizeProductId)
        {
            return await _orderRepository.CheckCustomizeProductExists(customizeProductId);
        }


        public async Task DeleteOrderAsync(int id)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
            {
                throw new ArgumentException($"Order with ID {id} not found. Cannot delete.");
            }

            await _orderRepository.DeleteAsync(id);
        }


        // view revenue
        public async Task<decimal> CalculateRevenueAsync(int? day, int? month, int? year)
        {
            var orders = await _orderRepository.GetAllAsync();

            var filteredOrders = orders.Where(order =>
                (!day.HasValue || (order.OrderDate.HasValue && order.OrderDate.Value.Day == day)) &&
                (!month.HasValue || (order.OrderDate.HasValue && order.OrderDate.Value.Month == month)) &&
                (!year.HasValue || (order.OrderDate.HasValue && order.OrderDate.Value.Year == year))
            );

            return filteredOrders.Sum(order => order.TotalPrice.GetValueOrDefault());
        }

    }
}
