﻿using _2_Service.Service;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static BusinessObject.RequestDTO.RequestDTO;
using System.ComponentModel.DataAnnotations;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return Ok(await _orderService.GetAllOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data", errors = ModelState });
            }

            var order = new Order
            {
                CustomizeProductId = orderDto.CustomizeProductId,
                OrderDate = orderDto.OrderDate,
                DeliveryDate = orderDto.DeliveryDate,
                RecipientName = orderDto.RecipientName,
                DeliveryAddress = orderDto.DeliveryAddress,
                ShippingMethod = orderDto.ShippingMethod,
                ShippingFee = orderDto.ShippingFee,
                Notes = orderDto.Notes,
                Price = orderDto.Price,
                Quantity = orderDto.Quantity,
                TotalPrice = orderDto.TotalPrice
            };

            try
            {
                await _orderService.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
            }
            catch (ArgumentException ex) // Xử lý lỗi do nhập dữ liệu sai
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex) // Xử lý các lỗi khác
            {
                return StatusCode(500, new { message = "An unexpected error occurred", error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }

            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Order ID. It must be greater than 0." });
            }

            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            // 🛑 Kiểm tra `CustomizeProductId` có tồn tại không
            var existingProduct = await _orderService.CheckCustomizeProductExists(orderDto.CustomizeProductId);
            if (!existingProduct)
            {
                return BadRequest(new { message = $"CustomizeProductId {orderDto.CustomizeProductId} does not exist. Please provide a valid CustomizeProductId." });
            }

            // 🛑 Kiểm tra các giá trị quan trọng
            if (orderDto.Price <= 0 || orderDto.Quantity <= 0 || orderDto.TotalPrice <= 0)
            {
                return BadRequest(new { message = "Price, Quantity, and TotalPrice must be greater than zero." });
            }

            // ✅ Cập nhật dữ liệu Order
            existingOrder.CustomizeProductId = orderDto.CustomizeProductId;
            existingOrder.OrderDate = orderDto.OrderDate;
            existingOrder.DeliveryDate = orderDto.DeliveryDate;
            existingOrder.RecipientName = orderDto.RecipientName;
            existingOrder.DeliveryAddress = orderDto.DeliveryAddress;
            existingOrder.ShippingMethod = orderDto.ShippingMethod;
            existingOrder.ShippingFee = orderDto.ShippingFee;
            existingOrder.Notes = orderDto.Notes;
            existingOrder.Price = orderDto.Price;
            existingOrder.Quantity = orderDto.Quantity;
            existingOrder.TotalPrice = orderDto.TotalPrice;

            try
            {
                await _orderService.UpdateOrderAsync(existingOrder);
                return Ok(new { message = "Order updated successfully!", updatedOrder = existingOrder });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while updating the order.", error = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Order ID. It must be greater than 0." });
            }

            // 🛑 Kiểm tra Order có tồn tại không
            var existingOrder = await _orderService.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }

            try
            {
                await _orderService.DeleteOrderAsync(id);
                return Ok(new { message = $"Order with ID {id} deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting the order.", error = ex.Message });
            }
        }

    }
}
