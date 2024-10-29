using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public class CategoryService
    {
        private readonly myContextDb _myContextDb;
        public CategoryService(myContextDb myContextDb)
        {
            _myContextDb = myContextDb;
        }
        public async Task<IEnumerable<Category>> GetAllCategory() {

            return await _myContextDb.tbl_Category.ToListAsync();
        }
        public async Task<CategoryDTO> AddCategory(CategoryDTO categoryDTO) {
            Category category = new Category();
            category.Name= categoryDTO.Name;

            await _myContextDb.tbl_Category.AddAsync(category);
            await _myContextDb.SaveChangesAsync();

            return new CategoryDTO { Id= category.Id, Name= category.Name }; 
           
        }

        public async Task<Category> GetByIdCategory(int id)
        {
            var category = await _myContextDb.tbl_Category.FindAsync(id);
            if (category==null) {
                throw new KeyNotFoundException("Category Not Found");
            }
            return category;
        }
        public async Task<Category> UpdateCategory(CategoryDTO categoryDTO) {

            var CategoryEntity =await _myContextDb.tbl_Category.FindAsync(categoryDTO.Id);
            if (CategoryEntity==null) {
                throw new KeyNotFoundException("Category Not Found");
            }
            CategoryEntity.Name= categoryDTO.Name;
            _myContextDb.tbl_Category.Update(CategoryEntity);
            await _myContextDb.SaveChangesAsync();
            return CategoryEntity;
        }
        public async Task DeleteCategory(int id)
        {
            var CategoryEntity = await _myContextDb.tbl_Category.FindAsync(id);
            if (CategoryEntity == null)
            {
                throw new KeyNotFoundException("Category Not Found");
            }
            _myContextDb.tbl_Category.Remove(CategoryEntity);
            await _myContextDb.SaveChangesAsync();

        }
        }
}
