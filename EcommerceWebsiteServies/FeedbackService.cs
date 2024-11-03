using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public class FeedbackService
    {
        private readonly myContextDb _myContextDb;
        public FeedbackService(myContextDb myContextDb)
        {
            _myContextDb = myContextDb;
        }
        public async Task<IEnumerable<Feedback>> GetAllFeedback()
        {
            return await _myContextDb.tbl_Feedback.ToListAsync();
        }

        public async Task<Feedback> GetBYIDFeedback(int id)
        {
            var Fed = await _myContextDb.tbl_Feedback.FindAsync(id);
            if (Fed == null)
            {
                throw new KeyNotFoundException("Faq Not Found");
            }
            return Fed;
        }
        public async Task<Feedback> AddFeedback(FeedbackDTO feedbackDTO)
        {
            Feedback Feedback = new Feedback
            {
                Name = feedbackDTO.Name,
                Description = feedbackDTO.Description
            };
            await _myContextDb.tbl_Feedback.AddAsync(Feedback);
            _myContextDb.SaveChanges();
            return Feedback;


        }
        public async Task<Feedback> UpdateFeedback(FeedbackDTOUpdate FeedbackDTO)
        {
            var FedEntity = await _myContextDb.tbl_Feedback.FindAsync(FeedbackDTO.Id);
            if (FedEntity == null)
            {
                throw new KeyNotFoundException("Faqs Not Found");
            }
            FedEntity.Name = FeedbackDTO.Name;
            FedEntity.Description = FeedbackDTO.Description;
            _myContextDb.tbl_Feedback.Update(FedEntity);
            _myContextDb.SaveChanges();
            return FedEntity;
        }
        public async Task<Feedback> DeleteFeedback(int id)
        {
            var FeedbackDelete = await _myContextDb.tbl_Feedback.FindAsync(id);
            if (FeedbackDelete == null)
            {
                throw new KeyNotFoundException("Faqs Not Found");
            }
            _myContextDb.tbl_Feedback.Remove(FeedbackDelete);
            await _myContextDb.SaveChangesAsync();
            return FeedbackDelete;
        }
    }
}
