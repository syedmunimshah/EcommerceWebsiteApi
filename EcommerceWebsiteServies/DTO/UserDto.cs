using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
    }
    public class UserAddDTO
    {
       

        public string Name { get; set; }
  
    }
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
  
    }
}
