using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies.DTO
{
    public class FaqsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class FaqsDTOUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
