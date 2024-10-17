using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EcommerceWebsiteDbConnection
{
    public class myContextDb: DbContext
    {
        public myContextDb(DbContextOptions<myContextDb> options):base(options) { }


        public DbSet<Customer> tbl_Customer { get; set; }
        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Category> tbl_Category { get; set; }
        public DbSet<Product> tbl_Product { get; set; }

        public DbSet<Cart> tbl_Cart { get; set; }
        public DbSet<Faqs> tbl_Faq { get; set; }
        public DbSet<Feedback> tbl_Feedback { get; set; }
    }
  
}
