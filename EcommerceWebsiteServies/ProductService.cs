using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public class ProductService
    {
        private readonly myContextDb _myContextDb;
        private readonly IWebHostEnvironment _env;
        public ProductService(myContextDb myContextDb, IWebHostEnvironment env)
        {
            _myContextDb=myContextDb;
            _env=env;
        }
        public async Task<IEnumerable<ProductDTO>> GetAllProduct()
        {
            var Produts = await _myContextDb.tbl_Product.Include(c => c.category).ToListAsync();
           return Produts.Select(x => new ProductDTO { Id = x.Id, Name = x.Name, Description = x.Description, Price = x.Price, Image = x.Image, CategoryId = x.CategoryId, CategoryName = x.category.Name }).ToList();

            //List<ProductDTO> productList = new List<ProductDTO>();

            //foreach (var product in Produts)
            //{
            //    ProductDTO ProductDto = new ProductDTO();

            //    ProductDto.Id = product.Id;
            //    ProductDto.Name = product.Name;
            //    ProductDto.Price = product.Price;
            //    ProductDto.Description = product.Description;
            //    ProductDto.CategoryId = product.CategoryId;
            //    ProductDto.CategoryName = product.category.Name;
            //    ProductDto.Image = product.Image;
             


            //    productList.Add(ProductDto);


            //}
            //return productList;


        }

        public async Task<ProductDTOAdd> AddProduct(ProductDTOAdd productDTO)
        {
            try
            {
                var productExist = await _myContextDb.tbl_Product.FirstOrDefaultAsync(x=>x.Name==productDTO.Name);
                if (productExist != null) {
                    throw new KeyNotFoundException("This Product Is already addeed");
                }

                Product product = new Product();

                product.Name = productDTO.Name;
                product.Price = productDTO.Price;
                product.Description = productDTO.Description;
                product.CategoryId = productDTO.CategoryId;

                string uniqueFileName=$"{Guid.NewGuid()}_{productDTO.UploadImage.FileName}";
                string ImagePath = Path.Combine(_env.WebRootPath, "ProductImage", uniqueFileName);
                Stream fs = new FileStream(ImagePath, FileMode.Create);
                productDTO.UploadImage.CopyTo(fs);

                product.Image = uniqueFileName;





                await _myContextDb.tbl_Product.AddAsync(product);
                await _myContextDb.SaveChangesAsync();
                return productDTO;
            }
            catch (Exception)
            {

                throw;
            }



        }
    }
}
