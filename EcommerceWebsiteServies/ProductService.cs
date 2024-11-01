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
            //Produts.Select(x => new ProductDTO { Id = x.Id, Name = x.Name, Description = x.Description,Price=x.Price,Image=x.Image,CategoryId=x.CategoryId,CategoryName=x.category.Name}).ToList();

            List<ProductDTO> productList = new List<ProductDTO>();

            foreach (var product in Produts)
            {
                ProductDTO ProductDto = new ProductDTO();

                ProductDto.Id = product.Id;
                ProductDto.Name = product.Name;
                ProductDto.Price = product.Price;
                ProductDto.Description = product.Description;
                ProductDto.CategoryId = product.CategoryId;
                ProductDto.CategoryName = product.category.Name;
                ProductDto.Image = product.Image;
             


                productList.Add(ProductDto);


            }
            return productList;


        }

        public async Task<Product> AddProduct(ProductDTOAdd productDTO)
        {
            try
            {
                var productExist = await _myContextDb.tbl_Product.FirstOrDefaultAsync(x=>x.Name==productDTO.Name);
                if (productExist != null) {
                    throw new KeyNotFoundException("This Product Is already addeed");
                }



                productExist.Name = productDTO.Name;
                productExist.Price = productDTO.Price;
                productExist.Description = productDTO.Description;
                productExist.CategoryId = productDTO.CategoryId;

                string uniqueFileName=$"{Guid.NewGuid()}_{productDTO.UploadImage.FileName}";
                string ImagePath = Path.Combine(_env.WebRootPath, "ProductImage", uniqueFileName);
                Stream fs = new FileStream(ImagePath, FileMode.Create);
                productDTO.UploadImage.CopyTo(fs);

                productExist.Image = uniqueFileName;


                await _myContextDb.tbl_Product.AddAsync(productExist);
                await _myContextDb.SaveChangesAsync();
                return productExist;
            }
            catch (Exception)
            {

                throw;
            }
<<<<<<< HEAD



=======
>>>>>>> parent of 4e9f7dc (save changes)
        }
    }
}
