using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies.DTO
{
    public class ProductDTOAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public IFormFile UploadImage { get; set; }
        public int CategoryId { get; set; }
     
    }
}
