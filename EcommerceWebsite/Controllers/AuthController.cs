using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    //Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKd3RTdWJqZWN0IiwiSWQiOiIxIiwiRW1haWwiOiJBYmR1bCIsImV4cCI6MTczMDE1MTk0OCwiaXNzIjoiSnd0SXNzdWVyIiwiYXVkIjoiSnd0QXVkaWVuY2UifQ.8lQWlKr0p8gppIATCtK6ERSSAjKUyl0JSGKuPSl6px8


    public class AuthController : ControllerBase
    {
        private readonly AuthService _roleService;
        public AuthController(AuthService roleService)
        {
            _roleService = roleService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _roleService.GetAllUser();
            return Ok(users);


        }
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _roleService.GetUserById(id);
            if (user == null) 
            { return NotFound(); }
            return Ok(user);
        }


        [HttpPost]

        public async Task<IActionResult> AddUser(UserAddDTO user)
        {
            try
            {
                var userAdd= await _roleService.AddUser(user);
                return Ok(userAdd);
            }
            catch (Exception)
            {

                throw;
            }
        }
    
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO userDto)
        {
            try
            {
                var user= await _roleService.UpdateUser(userDto);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(new {message=ex.Message});
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _roleService.DeleteUser(id);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }

        // Role Controller


        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var Role= await _roleService.GetAllRoles();
            return Ok(Role);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleById(int id)
        {
           var Role=await _roleService.GetRoleById(id);
            return Ok(Role);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleDTO roleDTO) {
            var role=await _roleService.AddRole(roleDTO);
            return Ok(role);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleDTO roleDTO)
        {
          var role= await _roleService.UpdateRole(roleDTO);
            return Ok(role);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(int id) {
            await _roleService.DeleteRole(id);
            return Ok();
        }

        //tbl_userRoles controller



        [HttpGet]
        public async Task<IActionResult> GetAllUserRole()
        {
            var userRole= await _roleService.GetAllUserRole();
            return Ok(userRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole(UserRoleDTO userRoleADO) {
          var user= await _roleService.AddUserRole(userRoleADO);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUserRole(UserRoleDTO userRoleADO) {
       var updateUserRole= await _roleService.UpdateUserRole(userRoleADO);
            if (updateUserRole==null) {
                return NotFound("User role not found.");
            }
            return Ok(userRoleADO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            await _roleService.DeleteUserRole(id);
            return Ok();
        }

 

    }
}
