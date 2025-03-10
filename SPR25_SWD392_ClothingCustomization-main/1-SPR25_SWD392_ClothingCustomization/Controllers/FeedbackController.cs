using _2_Service.Service;
using BusinessObject;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/Feedbacks")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public FeedbackController(IFeedbackService feedbackService, IOrderService orderService, IUserService userService)
        {
            _feedbackService = feedbackService;
            _orderService = orderService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetAll()
        {
            return Ok(await _feedbackService.GetAllFeedbacks());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetById(int id)
        {
            var feedback = await _feedbackService.GetFeedbackById(id);
            if (feedback == null)
            {
                return NotFound(new { message = $"Feedback with ID {id} not found." });
            }
            return Ok(feedback);
        }



        [HttpPost]
        public async Task<ActionResult> Create([FromBody] FeedbackDTO feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid input data", errors = ModelState });
            }

            // Kiểm tra OrderId có tồn tại không
            var existingOrder = await _orderService.GetOrderByIdAsync(feedbackDto.OrderId);
            if (existingOrder == null)
            {
                return NotFound(new { message = $"Order with ID {feedbackDto.OrderId} not found." });
            }

            // Kiểm tra UserId có tồn tại không
            var existingUser = await _userService.GetUserById(feedbackDto.UserId);
            if (existingUser == null)
            {
                return NotFound(new { message = $"User with ID {feedbackDto.UserId} not found." });
            }

            var feedback = new Feedback
            {
                OrderId = feedbackDto.OrderId,
                UserId = feedbackDto.UserId,
                Rating = feedbackDto.Rating,
                Review = feedbackDto.Review,
                CreatedDate = DateTime.UtcNow
            };

            try
            {
                await _feedbackService.AddFeedbackAsync(feedback);
                return CreatedAtAction(nameof(GetById), new { id = feedback.FeedbackId }, new { message = "Feedback created successfully!", feedback });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while creating feedback.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] FeedbackDTO feedbackDto)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Feedback ID. It must be greater than 0." });
            }

            var existingFeedback = await _feedbackService.GetFeedbackById(id);
            if (existingFeedback == null)
            {
                return NotFound(new { message = $"Feedback with ID {id} not found." });
            }

            // Kiểm tra Rating hợp lệ
            if (feedbackDto.Rating < 1 || feedbackDto.Rating > 5)
            {
                return BadRequest(new { message = "Rating must be between 1 and 5." });
            }

            // Kiểm tra Review không được để trống
            if (string.IsNullOrWhiteSpace(feedbackDto.Review))
            {
                return BadRequest(new { message = "Review cannot be empty." });
            }

            existingFeedback.Review = feedbackDto.Review;
            existingFeedback.Rating = feedbackDto.Rating;

            try
            {
                await _feedbackService.UpdateFeedbackAsync(existingFeedback);
                return Ok(new { message = "Feedback updated successfully!", updatedFeedback = existingFeedback });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while updating feedback.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "Invalid Feedback ID. It must be greater than 0." });
            }

            var existingFeedback = await _feedbackService.GetFeedbackById(id);
            if (existingFeedback == null)
            {
                return NotFound(new { message = $"Feedback with ID {id} not found." });
            }

            try
            {
                await _feedbackService.DeleteFeedbackAsync(id);
                return Ok(new { message = $"Feedback with ID {id} deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting the feedback.", error = ex.Message });
            }
        }

    }
}
