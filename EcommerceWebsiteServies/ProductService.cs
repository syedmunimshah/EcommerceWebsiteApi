using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
          return  Produts.Select(x => new ProductDTO { Id = x.Id, Name = x.Name, Description = x.Description, Price = x.Price, Image = x.Image, CategoryId = x.CategoryId, CategoryName = x.category.Name }).ToList();

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
        public async Task<ProductGetBYIDDTO> GetByIdProduct(int id)
        {
            //SqlParameter[] sqlParameter = new SqlParameter[]
            //{
            //new SqlParameter("@Id",id)
            //};
            //var product = await _myContextDb.ProductGetBYIDDTO.FromSqlRaw("Exec Sp_GetByIdProduct @Id", sqlParameter).FirstOrDefaultAsync();
            //return product;


            var GetIdProduct = await _myContextDb.tbl_Product.Include(c => c.category).FirstOrDefaultAsync(x => x.Id == id);
            if (GetIdProduct == null)
            {
                throw new KeyNotFoundException("This Product Id is not Avaliable in my Record");
            }
            return new ProductGetBYIDDTO()
            {
                Id = GetIdProduct.Id,
                Name = GetIdProduct.Name,
                Price = GetIdProduct.Price,
                Image = GetIdProduct.Image,
                Description = GetIdProduct.Description,
                CategoryId = GetIdProduct.CategoryId,
                CategoryName = GetIdProduct.category.Name
            };


        }
        public async Task<ProductGetBYIDDTO> AddProduct(ProductDTOAdd productDTO)
        {
            try
            {
                var productExist = await _myContextDb.tbl_Product.FirstOrDefaultAsync(x=>x.Name==productDTO.Name);
                if (productExist != null) {
                    throw new KeyNotFoundException("This Product Is already addeed");
                }

                
                Product product=new Product();

                product.Name = productDTO.Name;
                product.Price = productDTO.Price;
                product.Description = productDTO.Description;
                product.CategoryId = productDTO.CategoryId;

                string uniqueFileName=$"{Guid.NewGuid()}_{productDTO.UploadImage.FileName}";
                string ImagePath = Path.Combine(_env.WebRootPath, "ProductImage", uniqueFileName);
                FileStream fs = new FileStream(ImagePath, FileMode.Create);
                productDTO.UploadImage.CopyTo(fs);

                product.Image = uniqueFileName;


                await _myContextDb.tbl_Product.AddAsync(product);
                await _myContextDb.SaveChangesAsync();

                var SaveDataShow =await _myContextDb.tbl_Product.Include(c => c.category).FirstOrDefaultAsync(x => x.Id == product.Id);

                ProductGetBYIDDTO productGetBYIDDTO = new ProductGetBYIDDTO() 
                {
                    Id = SaveDataShow.Id,
                    Name = SaveDataShow.Name,
                    Price = SaveDataShow.Price,
                    Description = SaveDataShow.Description,
                    Image = SaveDataShow.Image,
                    CategoryId = SaveDataShow.CategoryId,
                    CategoryName = SaveDataShow.category.Name,
                };

                return productGetBYIDDTO;
            }
            catch (Exception)
            {

                throw;
            }

           

        }
        public async Task UpdateProduct(ProductDTOAdd productDTO)
        {
            try
            {
                string uniqueFileName = $"{Guid.NewGuid()}_{productDTO.UploadImage.FileName}";
                string ImagePath = Path.Combine(_env.WebRootPath, "ProductImage", uniqueFileName);
                FileStream fs = new FileStream(ImagePath, FileMode.Create);
                productDTO.UploadImage.CopyTo(fs);

                SqlParameter[] sqlParam = new SqlParameter[]
                {
                    new SqlParameter("@Id",productDTO.Id),
                    new SqlParameter("@Name",productDTO.Name),
                    new SqlParameter("@Price",productDTO.Price),
                    new SqlParameter("@Image",uniqueFileName),
                    new SqlParameter("@Description",productDTO.Description),
                    new SqlParameter("@CategoryId",productDTO.CategoryId)

                };
                //await _myContextDb.Database.ExecuteSqlRawAsync("Sp_UpdateProduct", sqlParam);
                await _myContextDb.Database.ExecuteSqlRawAsync("Sp_UpdateProduct @Id, @Name, @Price, @Image, @Description, @CategoryId", sqlParam);
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while updating the product.", ex);
            }
        }


        public async Task<Product> DeleteProduct(int id)
        {
          
            var deleteproduct = await _myContextDb.tbl_Product.FindAsync(id);
            if (deleteproduct == null)
            {
                throw new KeyNotFoundException("Product Not Found");
            }
            _myContextDb.tbl_Product.Remove(deleteproduct);

            await _myContextDb.SaveChangesAsync();
            return deleteproduct;
        }
        
    }
}
