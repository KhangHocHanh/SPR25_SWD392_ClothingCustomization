using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using static BusinessObject.RequestDTO.RequestDTO;

namespace SPR25_SWD392_ClothingCustomization.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        [HttpGet("FeedbackList")]
        public async Task<IActionResult> GetListFeedback()
        {
            var result = await _feedbackService.GetListFeedbacksAsync();

            if (result.Status != Const.SUCCESS_READ_CODE)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("getFeedbackBy{id}")]
        public async Task<IActionResult> GetFeedbackById(int id) 
            => Ok(await _feedbackService.GetFeedbackByIdAsync(id));

        [HttpPost("createFeedback")]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackCreateDTO feedbackDto) 
            => Ok(await _feedbackService.CreateFeedbackAsync(feedbackDto));

        [HttpPut("updateFeedback")]
        public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackUpdateDTO feedbackDto) 
            => Ok(await _feedbackService.UpdateFeedbackAsync(feedbackDto));

        [HttpDelete("deleteFeedbackBy{id}")]
        public async Task<IActionResult> DeleteFeedback(int id) 
            => Ok(await _feedbackService.DeleteFeedbackAsync(id));
    }
}
