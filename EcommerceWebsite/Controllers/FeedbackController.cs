using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _FeedbackService;
        public FeedbackController(FeedbackService feedbackService)
        {
            _FeedbackService = feedbackService; 
        }
        [HttpGet]
        public async Task<IEnumerable<Feedback>> GetAllFeedback()
        {
            return await _FeedbackService.GetAllFeedback();
        }

        [HttpGet]
        public async Task<Feedback> GetBYIDFeedback(int id)
        {
            return await _FeedbackService.GetBYIDFeedback(id);
        }
        [HttpPost]
        public async Task<Feedback> AddFeedback(FeedbackDTO feedbackDTO)
        {
            return await _FeedbackService.AddFeedback(feedbackDTO);
        }

        [HttpPut]
        public async Task<Feedback> UpdateFeedback(FeedbackDTOUpdate FeedbackDTO)
        {
            return await _FeedbackService.UpdateFeedback(FeedbackDTO);
        }
        [HttpDelete]
        public async Task<Feedback> DeleteFeedback(int id)
        {
            return await _FeedbackService.DeleteFeedback(id);
        }
    }
}
