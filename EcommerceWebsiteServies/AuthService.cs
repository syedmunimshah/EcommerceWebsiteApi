using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace EcommerceWebsiteServies
{
    public class AuthService
    {
        private readonly myContextDb _myContextDb;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;

        public AuthService(myContextDb myContextDb, IWebHostEnvironment env, IConfiguration configuration, IRoleService roleService)
        {
            _myContextDb = myContextDb;
            _env = env;
            _configuration = configuration;
            _roleService = roleService;
        }


        public async Task<List<UserDTO>> GetAllUser()
        {
            try
            {
                var users = await _myContextDb.tbl_users.ToListAsync();
                return users.Select(x => new UserDTO { Id = x.Id,Name=x.Name,Email=x.Email,Password=x.Password,Image=x.Image,CreateAt=x.CreateAt}).ToList();
                 
            }
              
            
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var users = await _myContextDb.tbl_users.FindAsync(id);
            if (users==null) {
                throw new KeyNotFoundException("User Not Found");
            }
            UserDTO userDTO = new UserDTO()
            {
                Id=users.Id,
                Name=users.Name,
                Email=users.Email,
                Password=users.Password,
                Image=users.Image,
                CreateAt=users.CreateAt,
            };
            return userDTO;

        }


        public async Task<UserDTO> AddUser(UserAddDTO user)
        {
            try
            {
                var username = await _myContextDb.tbl_users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
                if (username != null)
                {
                    throw new KeyNotFoundException("User Email and Password is Already");
                }

                User Adduser = new User();
                Adduser.Name=user.Name;
                Adduser.Email = user.Email;
                Adduser.Password = user.Password;
                string uniqueFileName = $"{Guid.NewGuid()}_{user.userImage.FileName}";
                string ImagePath=Path.Combine(_env.WebRootPath, "UserImages", uniqueFileName);
                FileStream fs = new FileStream(ImagePath, FileMode.Create);
                user.userImage.CopyTo(fs);
                Adduser.Image = uniqueFileName;

                await _myContextDb.tbl_users.AddAsync(Adduser);
                await  _myContextDb.SaveChangesAsync();

                UserDTO userDTO = new UserDTO()
                {
                    Id = Adduser.Id,
                    Name = Adduser.Name,
                    Email = Adduser.Email,
                    Password = Adduser.Password,
                    Image = Adduser.Image,
                    CreateAt= Adduser.CreateAt
                };

                return userDTO;

            }
            catch (Exception)
            {

                throw;
            }

        }


        //public async Task<string> userLogin(UserLoginDTO userLoginDTO)
        //{
        //    if (userLoginDTO.Email != null && userLoginDTO.Password != null)
        //    {

        //        //var jwtSubject = _configuration["Jwt:Subject"];
        //        //Console.WriteLine(jwtSubject);



        //        var user = _myContextDb.tbl_users.FirstOrDefault(x => x.Email == userLoginDTO.Email && x.Password == userLoginDTO.Password);
        //        if (user != null)
        //        {
        //            var claims = new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
        //            new Claim("Id",user.Id.ToString()),
        //            new Claim("Email",user.Name.ToString()),

        //        };
        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //            var token = new JwtSecurityToken(
        //                _configuration["Jwt:Issuer"],
        //                _configuration["Jwt:Audience"],
        //                claims,
        //                expires: DateTime.UtcNow.AddMinutes(10),
        //                signingCredentials: signIn

        //                );
        //            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        //            return tokenValue;

        //        }
        //        else
        //        {
        //            throw new Exception("User is not valid");
        //        }


        //    }
        //    else
        //    {
        //        throw new Exception("Credentials is not valid");
        //    }
        //}

        public async Task<string> UserLogin(UserLoginDTO userLoginDTO)
        {
            if (string.IsNullOrEmpty(userLoginDTO.Email) || string.IsNullOrEmpty(userLoginDTO.Password))
            {
                throw new Exception("Credentials are not valid");
            }

            // Retrieve the user from the database
            var user = await _myContextDb.tbl_users
                .FirstOrDefaultAsync(x => x.Email == userLoginDTO.Email && x.Password == userLoginDTO.Password);

            if (user == null)
            {
                throw new Exception("User is not valid");
            }

            // Fetch the roles for the user
            var userRoles = await _myContextDb.tbl_UserRole
                .Where(ur => ur.UserId == user.Id)
                .Join(_myContextDb.tbl_role, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                .ToListAsync();

            // Prepare the claims for the JWT token
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),  // Subject claim
        new Claim("Id", user.Id.ToString()),                                    // User ID claim
        new Claim("Email", user.Email),                                          // Email claim
    };

            // Add roles as claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));  // Add each role as a claim
            }

            // Generate the signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],   // Issuer
                _configuration["Jwt:Audience"], // Audience
                claims,                         // Claims
                expires: DateTime.UtcNow.AddMinutes(60),  // Token expiration (e.g., 1 hour)
                signingCredentials: signingCredentials
            );

            // Return the JWT token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserDTO> UpdateUser(UserUpdateDTO userDto) 
        {
          
            var entity = await _myContextDb.tbl_users.FindAsync(userDto.Id);
            if (entity == null)
            {

                 throw new KeyNotFoundException("User Not Found");
            }

            entity.Name = userDto.Name;
            entity.Email = userDto.Email;
            entity.Password = userDto.Password;

            string uniqueFileName = $"{Guid.NewGuid()}_{userDto.userImage.FileName}";
            string ImagePath = Path.Combine(_env.WebRootPath, "UserImages", uniqueFileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            userDto.userImage.CopyTo(fs);

            entity.Image = uniqueFileName;




            _myContextDb.tbl_users.Update(entity);
             await _myContextDb.SaveChangesAsync();

            UserDTO UserFrontView = new UserDTO();
            UserFrontView.Id = entity.Id;
            UserFrontView.Name= entity.Name;
            UserFrontView.Email = entity.Email;
            UserFrontView.Password = entity.Password;
            UserFrontView.Image = entity.Image;
            UserFrontView.CreateAt= entity.CreateAt;


            return UserFrontView;


        }

        public async Task<bool> DeleteUser(int id) {
            var entity =await _myContextDb.tbl_users.FindAsync(id);
            if (entity==null) {
                return false;
            }
            _myContextDb.tbl_users.Remove(entity);
            await _myContextDb.SaveChangesAsync();
            return true;
        }


            // Role services
        public async Task<IEnumerable<Role>> GetAllRoles() {
            try
            {
              return await _myContextDb.tbl_role.ToListAsync();
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Role> GetRoleById(int id) {
            var Roles =await _myContextDb.tbl_role.FindAsync(id);
            if (Roles == null)
            {
                throw new KeyNotFoundException("Role Not Found");
            }
           
            return Roles;

        }

        public async Task<RoleDTO> AddRole(RoleDTO role) {

            var RoleName =await _myContextDb.tbl_role.FirstOrDefaultAsync(x=>x.Name==role.Name);
     
            if (RoleName!=null) 
            {
                throw new KeyNotFoundException("Role Name Already Exist.");
            }
            Role roleAdd = new Role() 
            { 
                Name=role.Name
            };
            await _myContextDb.AddAsync(roleAdd);
            await _myContextDb.SaveChangesAsync();
            return role;
        }

        public async Task<RoleDTO> UpdateRole(RoleDTO roleDTO) {
            var entity = await _myContextDb.tbl_role.FindAsync(roleDTO.id);
            if (entity==null) {
                throw new KeyNotFoundException("Not found Role");
            }

            entity.Name=roleDTO.Name;

            _myContextDb.tbl_role.Update(entity);
            await _myContextDb.SaveChangesAsync();


            return roleDTO;

        }

        public async Task<bool> DeleteRole(int id) {

            var entity =await _myContextDb.tbl_role.FindAsync(id);
            if (entity==null) {

                return false;
                    }
            _myContextDb.tbl_role.Remove(entity);
            await _myContextDb.SaveChangesAsync();
            return true;

        }

        //tbl_userRoles Service

        public async Task<IEnumerable<UserRoleDTO>> GetAllUserRole()
        {
            try
            {
                return await _myContextDb.tbl_UserRole.Include(u => u.user).Include(r => r.role).Select(ur => new UserRoleDTO { Id = ur.Id, UserId = ur.UserId, UserName = ur.user.Name, RoleId = ur.RoleId, RoleName = ur.role.Name }).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task<UserRoleDTO> GetUserRoleById(int id)
        {
            var AssignRole = await _myContextDb.tbl_UserRole.Include(u => u.user).Include(r => r.role).FirstOrDefaultAsync(x => x.Id == id);
            if (AssignRole == null)
            {
                throw new KeyNotFoundException("User Not Found");
            }

            UserRoleDTO userRoleDTO = new UserRoleDTO
            {
                Id = AssignRole.Id,
                UserId = AssignRole.UserId,
                UserName = AssignRole.user.Name,
                RoleId = AssignRole.RoleId,
                RoleName = AssignRole.role.Name

            };

            return userRoleDTO;



        }

        public async Task<UserRoleAddDTO> AddUserRole(UserRoleAddDTO userRoleADO)
        {
            //Object Initializer(Approach 1)
            //AssignRole userRole = new AssignRole()
            //{
            //    UserId = userRoleADO.UserId,
            //    RoleId = userRoleADO.RoleId
            //};
            //await _myContextDb.tbl_UserRole.AddAsync(userRole);
            //await _myContextDb.SaveChangesAsync();
            //return userRole;


            //Separate Assignment(Approach 2)
            UserRole userRole = new UserRole();
            userRole.UserId = userRoleADO.UserId;
            userRole.RoleId = userRoleADO.RoleId;
            await _myContextDb.tbl_UserRole.AddAsync(userRole);
            await _myContextDb.SaveChangesAsync();
            return userRoleADO;


        }

        public async Task<UserRole> UpdateUserRole(UserRoleAddDTO userRoleADO) {

            var entity = await _myContextDb.tbl_UserRole.FindAsync(userRoleADO.Id);
            if (entity==null) 
            {
                throw new KeyNotFoundException("Not Found UserRole"); 
            }
            entity.UserId=userRoleADO.UserId;
            entity.RoleId=userRoleADO.RoleId;
            _myContextDb.tbl_UserRole.Update(entity);
            await _myContextDb.SaveChangesAsync();
            return entity;

        }

        public async Task<UserRole> DeleteUserRole(int id)
        {

            var entity = await _myContextDb.tbl_UserRole.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Not Found UserRole");
            }
          
            _myContextDb.tbl_UserRole.Remove(entity);
            await _myContextDb.SaveChangesAsync();
            return entity;

        }

     



    }
}
