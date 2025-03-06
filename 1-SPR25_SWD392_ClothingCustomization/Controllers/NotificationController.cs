using _2_Service.Service;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using static BusinessObject.RequestDTO.RequestDTO;

namespace _1_SPR25_SWD392_ClothingCustomization.Controllers
{
    [Route("api/Notifications")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetAll()
        {
            return Ok(await _notificationService.GetAllNotifications());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetById(int id)
        {
            var notification = await _notificationService.GetNotificationById(id);
            if (notification == null)
                return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] NotificationDTO notificationDto)
        {
            var notification = new Notification
            {
                UserId = notificationDto.UserId,
                Subject = notificationDto.Subject,
                Message = notificationDto.Message,
                CreatedDate = notificationDto.CreatedDate,
                IsRead = notificationDto.IsRead,
            };
            await _notificationService.AddNotification(notification);
            return CreatedAtAction(nameof(GetById), new { id = notification.NotificationId }, notification);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] NotificationDTO notificationDto)
        {
            var existingFeedback = await _notificationService.GetNotificationById(id);
            if (existingFeedback == null)
            {
                return NotFound();
            }

            // Update only allowed properties
            existingFeedback.Subject = notificationDto.Subject;
            existingFeedback.Message = notificationDto.Message;

            await _notificationService.UpdateNotification(existingFeedback);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _notificationService.DeleteNotification(id);
            return NoContent();
        }
    }
}
