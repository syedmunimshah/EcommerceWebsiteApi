//using EcommerceWebsiteDbConnection;
//using EcommerceWebsiteServies;
//using EcommerceWebsiteServies.DTO;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace EcommerceWebsite.Controllers
//{
//    [Route("[controller]/[action]")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly AdminService _AdminService;
//        public AdminController(AdminService adminService)
//        {
//            _AdminService = adminService;
   
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllAdmin()
//        {
//            try
//            {
//             var admin=  await  _AdminService.GetAllAdmin();
//              return  Ok(admin);
//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> RegisterAdmin(AdminDTO admin) {
//        var addadmin= await _AdminService.AddAdmin(admin);
//            return Ok(addadmin);
//        }

      
//        [HttpPost]
//        public async Task<IActionResult> AdminLogin(AdminLoginDTO adminLoginDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            try
//            {
//                var token = await _AdminService.AdminLogin(adminLoginDTO);
//                return Ok(new { Token = token });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateAdmin(AdminDTO adminDTO)
//        {
//            var Admin=await _AdminService.UpdateAdmin(adminDTO);
//            return Ok(Admin);
//        }
//        [HttpGet]
//        public async Task<IActionResult> AdminGetById(int id)
//        {
//            var admin=await _AdminService.AdminGetById(id);
//            return Ok(admin);
//        }

//        [HttpDelete]
//        public async Task<IActionResult> DeleteAdmin(int id) {
//            var deleteAdmin = await _AdminService.DeleteAdmin(id);
//            return Ok(deleteAdmin);
//        }
//    }
//}
