﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteDbConnection
{
    public  class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}