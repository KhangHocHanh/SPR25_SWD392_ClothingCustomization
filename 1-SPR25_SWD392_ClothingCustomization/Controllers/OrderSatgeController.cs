
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using static BusinessObject.RequestDTO.RequestDTO;

namespace SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/order-stages")]
    [ApiController]
    public class OrderStageController : ControllerBase
    {
        private readonly IOrderStageService _orderStageService;

        public OrderStageController(IOrderStageService orderStageService)
        {
            _orderStageService = orderStageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStages() => Ok(await _orderStageService.GetAllOrderStagesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStageById(int id) => Ok(await _orderStageService.GetOrderStageByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateOrderStage([FromBody] OrderStageCreateDTO orderStageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data", errors = ModelState });
            }

            try
            {
                var response = await _orderStageService.CreateOrderStageAsync(orderStageDto);
                return StatusCode(response.Status, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderStageUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }

            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid OrderStage ID. It must be greater than 0." });
            }

            var existing = await _orderStageService.GetOrderStageByIdRawAsync(id); // 🆕 lấy entity thuần

            if (existing == null)
            {
                return NotFound(new { message = $"OrderStage with ID {id} not found." });
            }

            var orderExists = await _orderStageService.CheckOrderExists(dto.OrderId);
            if (!orderExists)
            {
                return BadRequest(new { message = $"Order with ID {dto.OrderId} does not exist." });
            }

            // ✅ Cập nhật thông tin
            existing.OrderId = dto.OrderId;
            existing.OrderStageName = dto.OrderStageName;
            existing.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _orderStageService.UpdateOrderStageAsync(existing);
                return Ok(new { message = "OrderStage updated successfully!", updatedOrderStage = existing });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating.", error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStage(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid OrderStage ID. It must be greater than 0." });
            }

            try
            {
                var response = await _orderStageService.DeleteOrderStageAsync(id);
                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred", error = ex.Message });
            }
        }


    }
}
