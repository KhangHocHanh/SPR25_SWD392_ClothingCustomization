using _2_Service;
using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Model;
using BusinessObject.ResponseDTO;
using Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;

namespace Service.Service
{
    public interface IOrderStageService
    {
        Task<ResponseDTO> GetAllOrderStagesAsync();
        Task<ResponseDTO> GetOrderStageByIdAsync(int id);
        Task<ResponseDTO> CreateOrderStageAsync(OrderStageCreateDTO orderStageDto);
        Task<ResponseDTO> DeleteOrderStageAsync(int id);
    }
    public class OrderStageService : IOrderStageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderStageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> GetAllOrderStagesAsync()
        {
            var orderStages = await _unitOfWork.OrderStageRepository.GetAllOrderStagesAsync();
            var result = _mapper.Map<List<ResponseDTO.OrderStageListDTO>>(orderStages);
            return new ResponseDTO(200, "Success", result);
        }

        public async Task<ResponseDTO> GetOrderStageByIdAsync(int id)
        {
            var orderStage = await _unitOfWork.OrderStageRepository.GetOrderStageByIdAsync(id);
            if (orderStage == null) return new ResponseDTO(404, "OrderStage not found");

            var result = new OrderStageResponseDTO
            {
                OrderStageId = orderStage.OrderStageId,
                OrderId = orderStage.OrderId,
                OrderStageName = orderStage.OrderStageName.ToString(),
                UpdatedDate = orderStage.UpdatedDate
            };

            return new ResponseDTO(200, "Success", result);
        }


        public async Task<ResponseDTO> CreateOrderStageAsync(OrderStageCreateDTO orderStageDto)
        {
            // Kiểm tra OrderId có tồn tại không
            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderStageDto.OrderId);
            if (existingOrder == null)
            {
                return new ResponseDTO(400, $"OrderId {orderStageDto.OrderId} does not exist.");
            }

            // Kiểm tra giá trị hợp lệ của OrderStageName
            if (!Enum.IsDefined(typeof(OrderStageEnum), orderStageDto.OrderStageName))
            {
                return new ResponseDTO(400, $"Invalid OrderStageName '{orderStageDto.OrderStageName}'.");
            }

            var orderStage = _mapper.Map<OrderStage>(orderStageDto);
            await _unitOfWork.OrderStageRepository.AddOrderStageAsync(orderStage);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(201, "OrderStage created successfully", orderStage);
        }


        public async Task<ResponseDTO> DeleteOrderStageAsync(int id)
        {
            var orderStage = await _unitOfWork.OrderStageRepository.GetOrderStageByIdAsync(id);
            if (orderStage == null)
            {
                return new ResponseDTO(404, $"OrderStage with ID {id} not found.");
            }

            await _unitOfWork.OrderStageRepository.DeleteOrderStageAsync(orderStage);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(200, "OrderStage deleted successfully.");
        }

    }
}
