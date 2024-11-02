﻿using EcommerceWebsiteDbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies.DTO
{
    public class UserRoleDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
      

        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
    public class UserRoleAddDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

    }

}
