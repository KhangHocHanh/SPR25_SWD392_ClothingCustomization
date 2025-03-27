using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Repository.IRepository;
using AutoMapper;
using BusinessObject.Model;
using Repository.IRepository;
using Service;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace _2_Service.Service
{
    public interface ICustomizeProductService
    {
        Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProducts();
        Task<CustomizeProduct> GetCustomizeProductById(int id);
        Task AddCustomizeProduct(CustomizeProduct customizeProduct);
        Task UpdateCustomizeProduct(CustomizeProduct customizeProduct);
        Task DeleteCustomizeProduct(int id);
        Task<List<ProductCustomizationCountDto>> GetProductCustomizationCounts();
        Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProductsAsync();
        Task<CustomizeProduct> GetCustomizeProductByIdAsync(int id);
        Task<IEnumerable<CustomizeProduct>> GetCustomizeProductsByCurrentUserAsync(int userId);
        Task UpdateCustomizeProductAsync(CustomizeProduct product);
        Task DeleteCustomizeProductAsync(int id);
        Task<CustomizeProduct> CreateCustomizeProductAsync(CustomizeProduct product);
        Task<CustomizeProductWithOrderResponse> CreateCustomizeProductWithOrderAsync(CreateCustomizeDto dto);


    }
    public class CustomizeProductService : ICustomizeProductService
    {
        private readonly ICustomizeProductRepository _customizeProductRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderStageRepository _orderStageRepo;
        public CustomizeProductService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICustomizeProductRepository customizeProductRepo,
        IOrderRepository orderRepo,
        IOrderStageRepository orderStageRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _customizeProductRepository = customizeProductRepo;
            _orderRepo = orderRepo;
            _orderStageRepo = orderStageRepo;
        }

        public async Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProducts()
        {
            return await _customizeProductRepository.GetAllAsync();
        }

        public async Task<CustomizeProduct> GetCustomizeProductById(int id)
        {
            return await _customizeProductRepository.GetByIdAsync(id);
        }

        public async Task AddCustomizeProduct(CustomizeProduct customizeProduct)
        {
            await _customizeProductRepository.AddAsync(customizeProduct);
        }

        public async Task UpdateCustomizeProduct(CustomizeProduct customizeProduct)
        {
            await _customizeProductRepository.UpdateAsync(customizeProduct);
        }

        public async Task DeleteCustomizeProduct(int id)
        {
            await _customizeProductRepository.DeleteAsync(id);
        }



        public async Task<List<ProductCustomizationCountDto>> GetProductCustomizationCounts()
        {
            return await _customizeProductRepository.GetProductCustomizationCounts();
        }

        public async Task<IEnumerable<CustomizeProduct>> GetAllCustomizeProductsAsync()
        {
            return await _customizeProductRepository.GetAllWithProductAndUserAsync();
        }

        public async Task<CustomizeProduct> GetCustomizeProductByIdAsync(int id)
        {
            return await _customizeProductRepository.GetWithElementsAsync(id);
        }

        public async Task<IEnumerable<CustomizeProduct>> GetCustomizeProductsByCurrentUserAsync(int userId)
        {
            return await _customizeProductRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateCustomizeProductAsync(CustomizeProduct product)
        {
            await _customizeProductRepository.UpdateAsync(product);
        }

        public async Task DeleteCustomizeProductAsync(int id)
        {
            var product = await _customizeProductRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _customizeProductRepository.RemoveAsync(product);
            }
        }

        public async Task<CustomizeProduct> CreateCustomizeProductAsync(CustomizeProduct product)
        {
            await _customizeProductRepository.CreateAsync(product);
            return product;
        }
        public async Task<CustomizeProductWithOrderResponse> CreateCustomizeProductWithOrderAsync(CreateCustomizeDto dto)
        {
            // Start transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // 1. Get the product to get its image and price
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {dto.ProductId} not found");
                }

                // 2. Create CustomizeProduct using the tuple mapping
                var customizeProduct = _mapper.Map<CustomizeProduct>((dto, product));

                await _customizeProductRepository.AddAsync(customizeProduct);
                await _unitOfWork.SaveChangesAsync();

                // 3. Create Order
                var order = new Order
                {
                    CustomizeProductId = customizeProduct.CustomizeProductId,
                    OrderDate = DateTime.UtcNow,
                    DeliveryDate = dto.DeliveryDate,
                    RecipientName = dto.RecipientName,
                    DeliveryAddress = dto.DeliveryAddress,
                    ShippingMethod = dto.ShippingMethod,
                    ShippingFee = (double)dto.ShippingFee,
                    Notes = dto.Notes,
                    Price = product.Price, // Use product's price
                    Quantity = dto.Quantity,
                    TotalPrice = (product.Price * dto.Quantity) + dto.ShippingFee
                };

                await _orderRepo.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                // 4. Create initial OrderStage
                var orderStage = new OrderStage
                {
                    OrderId = order.OrderId,
                    OrderStageName = "Created",
                    UpdatedDate = DateTime.UtcNow
                };

                await _orderStageRepo.AddOrderStageAsync(orderStage);
                await _unitOfWork.SaveChangesAsync();

                // Commit transaction
                await _unitOfWork.CommitAsync();

                return new CustomizeProductWithOrderResponse
                {
                    CustomizeProductId = customizeProduct.CustomizeProductId,
                    OrderId = order.OrderId,
                    OrderStageId = orderStage.OrderStageId,
                    Message = "Customize product and order created successfully"
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception($"Error creating customize product and order: {ex.Message}", ex);
            }
        }
    }

}
