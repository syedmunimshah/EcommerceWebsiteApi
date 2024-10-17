using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteDbConnection
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int Product_Id { get; set; }
        public int Category_Id { get; set; }
        public int Cart_Quantity { get; set; }
        public int Cart_Status { get; set; }

    }
}
