using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _AdminService;
        public AdminController(AdminService adminService)
        {
            _AdminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            try
            {
             var admin=  await  _AdminService.GetAllAdmin();
              return  Ok(admin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminDTO admin) {
        await _AdminService.AddAdmin(admin);
            return Ok(admin);
        }

        
        [HttpPut]
        public async Task<IActionResult> UpdateAdmin(AdminDTO admin)
        {
            await _AdminService.UpdateAdmin(admin);
            return Ok(admin);
        }
        [HttpGet]
        public async Task<IActionResult> AdminGetById(int id)
        {
            var admin=await _AdminService.AdminGetById(id);
            return Ok(admin);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(int id) {
            var deleteAdmin = await _AdminService.DeleteAdmin(id);
            return Ok(deleteAdmin);
        }
    }
}
