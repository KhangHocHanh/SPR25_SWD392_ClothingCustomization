using BusinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _2_Service.IService
{
    public interface IOrderStageService
    {
        Task<ResponseDTO> GetAllOrderStagesAsync();
        Task<ResponseDTO> GetOrderStageByIdAsync(int id);
        Task<ResponseDTO> CreateOrderStageAsync(OrderStageCreateDTO orderStageDto);
        Task<ResponseDTO> DeleteOrderStageAsync(int id);
    }
}
