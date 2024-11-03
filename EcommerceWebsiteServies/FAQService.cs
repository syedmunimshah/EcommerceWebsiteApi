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
    public class FAQService
    {
        private readonly myContextDb _myContextDb;
        public FAQService(myContextDb myContextDb)
        {
            _myContextDb = myContextDb;
        }

        public async Task<IEnumerable<Faqs>> GetAllFAQ()
        {
            return await _myContextDb.tbl_Faq.ToListAsync();
        }

        public async Task<Faqs> GetBYIDFAQ(int id) 
        {
       var Faq=await _myContextDb.tbl_Faq.FindAsync(id);
            if (Faq ==null) {
                throw new KeyNotFoundException("Faq Not Found");
            }
            return Faq;
        }
        public async Task<Faqs> AddFaqs(FaqsDTO faqsDTO) 
        {
            Faqs faqs= new Faqs 
            {   Name = faqsDTO.Name,
                Description=faqsDTO.Description 
            };
            await _myContextDb.tbl_Faq.AddAsync(faqs);
            _myContextDb.SaveChanges();
            return faqs;


        }
        public async Task<Faqs> UpdateFaqs(FaqsDTOUpdate faqsDTO) 
        {
            var FaqEntity =await _myContextDb.tbl_Faq.FindAsync(faqsDTO.Id);
            if (FaqEntity ==null) {
                throw new KeyNotFoundException("Faqs Not Found");
            }
            FaqEntity.Name = faqsDTO.Name;
            FaqEntity.Description= faqsDTO.Description;
             _myContextDb.tbl_Faq.Update(FaqEntity);
            _myContextDb.SaveChanges();
            return FaqEntity;
        }
        public async Task<Faqs> DeleteFaqs(int id) 
        {
            var FaqsDelete =await _myContextDb.tbl_Faq.FindAsync(id);
            if (FaqsDelete==null) 
            {
                throw new KeyNotFoundException("Faqs Not Found");
            }
            _myContextDb.tbl_Faq.Remove(FaqsDelete);
           await _myContextDb.SaveChangesAsync();
            return FaqsDelete;
        }
    }
}
