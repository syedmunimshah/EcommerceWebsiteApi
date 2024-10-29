using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory() {
        var CategoryList= await _categoryService.GetAllCategory();
            return Ok(CategoryList);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO) {

            var category=await  _categoryService.AddCategory(categoryDTO);
            return Ok(category);
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var category=await _categoryService.GetByIdCategory(id);

            return Ok(category);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            var deleteupdate=await _categoryService.UpdateCategory(categoryDTO);
            return Ok(deleteupdate);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           await _categoryService.DeleteCategory(id);
            return Ok();
        }

    }
}
