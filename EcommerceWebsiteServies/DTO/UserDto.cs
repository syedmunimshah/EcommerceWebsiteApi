using Microsoft.AspNetCore.Http;
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
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public DateTime CreateAt { get; set; }
    }
    public class UserAddDTO
    {


        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile userImage { get; set; }

    }
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile userImage { get; set; }

    }
    public class UserLoginDTO
    {
        

        public string Email { get; set; }
        public string Password { get; set; }


    }
}
