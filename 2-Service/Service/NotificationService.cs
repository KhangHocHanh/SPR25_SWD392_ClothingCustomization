using _3_Repository.IRepository;
using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Service.Service
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification> GetNotificationById(int id);
        Task AddNotification(Notification notification);
        Task UpdateNotification(Notification notification);
        Task DeleteNotification(int id);
    }
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _notificationRepository.GetAllAsync();
        }

        public async Task<Notification> GetNotificationById(int id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        public async Task AddNotification(Notification notification)
        {
            await _notificationRepository.AddAsync(notification);
        }

        public async Task UpdateNotification(Notification notification)
        {
            await _notificationRepository.UpdateAsync(notification);
        }

        public async Task DeleteNotification(int id)
        {
            await _notificationRepository.DeleteAsync(id);
        }
    }
}
