using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
           var product = await _productService.GetAllProduct();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTOAdd productDTOAdd)
        {
           var product=await _productService.AddProduct(productDTOAdd);
            return Ok(product);
        }
    }
}
