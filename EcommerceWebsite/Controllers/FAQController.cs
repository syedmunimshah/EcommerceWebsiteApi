using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly FAQService _FAQService;
        public FAQController(FAQService fAQService)
        {
            _FAQService = fAQService;
        }
        [HttpGet]
        public async Task<IEnumerable<Faqs>> GetAllFAQ()
        {
         return await  _FAQService.GetAllFAQ();
        }

        [HttpGet]
        public async Task<Faqs> GetBYIDFAQ(int id) 
        {
          return await _FAQService.GetBYIDFAQ(id);
        }
        [HttpPost]
        public async Task<Faqs> AddFaqs(FaqsDTO faqsDTO)
        {
            return await _FAQService.AddFaqs(faqsDTO);
        }

        [HttpPut]
        public async Task<Faqs> UpdateFaqs(FaqsDTOUpdate faqsDTO)
        {
            return await _FAQService.UpdateFaqs(faqsDTO);
        }
        [HttpDelete]
        public async Task<Faqs> DeleteFaqs(int id)
        {
            return await _FAQService.DeleteFaqs(id);
        }
        
    }
}
