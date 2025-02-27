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

namespace Service.Service
{
    public interface IFeedbackService
    {
        Task<ResponseDTO> GetListFeedbacksAsync();
        Task<ResponseDTO> GetFeedbackByIdAsync(int id);
        Task<ResponseDTO> CreateFeedbackAsync(FeedbackCreateDTO feedbackDto);
        Task<ResponseDTO> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackDto);
        Task<ResponseDTO> DeleteFeedbackAsync(int id);
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> CreateFeedbackAsync(FeedbackCreateDTO feedbackDto)
        {
            var feedback = _mapper.Map<Feedback>(feedbackDto);
            await _unitOfWork.FeedbackRepository.AddAsync(feedback);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(Const.SUCCESS_CREATE_CODE, "Feedback created successfully");
        }

        public async Task<ResponseDTO> DeleteFeedbackAsync(int id)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
            if (feedback == null) return new ResponseDTO(Const.WARNING_NO_DATA_CODE, "Feedback not found");

            _unitOfWork.FeedbackRepository.Delete(feedback);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(Const.SUCCESS_DELETE_CODE, "Feedback deleted successfully");
        }

        public async Task<ResponseDTO> GetListFeedbacksAsync()
        {
            try
            {
                var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync();

                if (feedbacks == null || !feedbacks.Any())
                {
                    return new ResponseDTO(Const.SUCCESS_CREATE_CODE, "Empty List");
                }

                var result = _mapper.Map<List<FeedbackListDTO>>(feedbacks);

                return new ResponseDTO(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, result);
            }
            catch (Exception e)
            {
                return new ResponseDTO(Const.ERROR_EXCEPTION, e.Message);
            }
        }

        public async Task<ResponseDTO> GetFeedbackByIdAsync(int id)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
            if (feedback == null) return new ResponseDTO(Const.WARNING_NO_DATA_CODE, "Feedback not found");

            var result = _mapper.Map<ProductListDTO>(feedback);
            return new ResponseDTO(Const.SUCCESS_READ_CODE, "Success", result);
        }

        public async Task<ResponseDTO> UpdateFeedbackAsync(FeedbackUpdateDTO feedbackDto)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackDto.FeedbackId);
            if (feedback == null) return new ResponseDTO(Const.WARNING_NO_DATA_CODE, "Feedback not found");

            _mapper.Map(feedbackDto, feedback);
            _unitOfWork.FeedbackRepository.Update(feedback);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Feedback updated successfully");
        }
    }
}
