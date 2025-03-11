using AutoMapper;
using BusinessObject.Model;
using BusinessObject;
using BusinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessObject.RequestDTO.RequestDTO;
using static BusinessObject.ResponseDTO.ResponseDTO;
using _3_Repository.IRepository;

namespace _2_Service.Service
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback> GetFeedbackById(int id);
        Task AddFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(int id);
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _feedbackRepository.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Feedback ID. It must be greater than 0.");
            }

            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
            {
                throw new KeyNotFoundException($"Feedback with ID {id} not found.");
            }

            return feedback;
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            if (feedback.OrderId <= 0)
            {
                throw new ArgumentException("OrderId must be greater than 0.");
            }

            if (feedback.UserId <= 0)
            {
                throw new ArgumentException("UserId must be greater than 0.");
            }

            if (feedback.Rating < 1 || feedback.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            if (string.IsNullOrWhiteSpace(feedback.Review) || feedback.Review.Length < 10 || feedback.Review.Length > 500)
            {
                throw new ArgumentException("Review must be between 10 and 500 characters.");
            }

            await _feedbackRepository.AddAsync(feedback);
        }


        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            var existingFeedback = await _feedbackRepository.GetByIdAsync(feedback.FeedbackId);
            if (existingFeedback == null)
            {
                throw new Exception("Feedback not found.");
            }

            if (feedback.Rating < 1 || feedback.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            if (string.IsNullOrWhiteSpace(feedback.Review))
            {
                throw new ArgumentException("Review cannot be empty.");
            }

            await _feedbackRepository.UpdateAsync(feedback);
        }


        public async Task DeleteFeedbackAsync(int id)
        {
            var existingFeedback = await _feedbackRepository.GetByIdAsync(id);
            if (existingFeedback == null)
            {
                throw new ArgumentException($"Feedback with ID {id} not found. Cannot delete.");
            }

            await _feedbackRepository.DeleteAsync(id);
        }

        
    }
}
