//using EcommerceWebsiteDbConnection;
//using EcommerceWebsiteServies.DTO;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Authorization;



//namespace EcommerceWebsiteServies
//{
//    [Authorize(Roles = "Admin")]
//    public class AdminService
//    {
//        private readonly myContextDb _myContextDb;
//        private readonly IWebHostEnvironment _env;
//        private readonly IConfiguration _configuration;

//        public AdminService(myContextDb myContextDb, IWebHostEnvironment env, IConfiguration configuration)
//        {
//            _myContextDb = myContextDb;
//            _env= env;
//            _configuration = configuration;
//        }
         
//        public async Task<IEnumerable<Admin>> GetAllAdmin()
//        {
//            return await _myContextDb.tbl_admin.ToListAsync();
//        }

//        public async Task<Admin> AddAdmin(AdminDTO admin)
//        {
//            try
//            {
//                Admin AddAdmin = new Admin();
//                AddAdmin.Name = admin.Name;
//                AddAdmin.Email = admin.Email;
//                AddAdmin.Password = admin.Password;

//                //image create and save it database 
//                string uniqueFileName = $"{Guid.NewGuid()}_{admin.AdminImage.FileName}";
//                string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", uniqueFileName);

//                //string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", $"{Guid.NewGuid()}", admin.AdminImage.FileName);

//                FileStream fs = new FileStream(ImagePath, FileMode.Create);
//                admin.AdminImage.CopyTo(fs);

//                //AddAdmin.Image = admin.AdminImage.FileName;
//                AddAdmin.Image = uniqueFileName;

//                await _myContextDb.tbl_admin.AddAsync(AddAdmin);
//                await _myContextDb.SaveChangesAsync();
//                return AddAdmin;
//            }
//            catch (Exception ex)
//            {

//                throw new Exception("An error occurred while adding the admin.", ex);
//            }
//        }

//        public async Task<string> AdminLogin(AdminLoginDTO adminLoginDTO) 
//        {
//            if (adminLoginDTO.Email != null && adminLoginDTO.Password != null)
//            {

//                var jwtSubject = _configuration["Jwt:Subject"];
//                Console.WriteLine(jwtSubject);

//                var admin = _myContextDb.tbl_admin.FirstOrDefault(x => x.Email == adminLoginDTO.Email && x.Password == adminLoginDTO.Password);
//                var AdimnId = admin.Id;
//                if (admin != null)
//                {
//                    var claims = new List<Claim>
//                {
//                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
//                    new Claim("Id",admin.Id.ToString()),
//                    new Claim("Email",admin.Name.ToString()),

//                };
//                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
//                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//                    var token = new JwtSecurityToken(
//                        _configuration["Jwt:Issuer"],
//                        _configuration["Jwt:Audience"],
//                        claims,
//                        expires: DateTime.UtcNow.AddMinutes(10),
//                        signingCredentials: signIn

//                        );
//                    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
//                    return tokenValue;

//                }
//                else
//                {
//                    throw new Exception("Customer is not valid");
//                }


//            }
//            else
//            {
//                throw new Exception("Credentials is not valid");
//            }
//        }

//        public async Task<Admin> UpdateAdmin(AdminDTO adminDTO)
//        {
//            var adminEntity = await _myContextDb.tbl_admin.FindAsync(adminDTO.Id);
//            if (adminEntity == null)
//            {
//                throw new KeyNotFoundException("Admin Not Found");
//            }

//            adminEntity.Name = adminDTO.Name;
//            adminEntity.Email = adminDTO.Email;
//            adminEntity.Password = adminDTO.Password;

//            //image create and save it database
//            string uniqueFileName = $"{Guid.NewGuid()}_{adminDTO.AdminImage.FileName}";
//            string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", uniqueFileName);
//            FileStream fs = new FileStream(ImagePath, FileMode.Create);
//            adminDTO.AdminImage.CopyTo(fs);

//            adminEntity.Image = uniqueFileName;
//            _myContextDb.tbl_admin.Update(adminEntity);
//            await _myContextDb.SaveChangesAsync();
//            return adminEntity;


//        }


//        public async Task<Admin> AdminGetById(int id) {

//            var Adminentity = await _myContextDb.tbl_admin.FindAsync(id);
//            if (Adminentity==null) {
//                throw new KeyNotFoundException("Admin Not Found");
//            }
//            return Adminentity;
            
//        }

//        public async Task<Admin> DeleteAdmin(int id) {

//            var deleteAdmin =await _myContextDb.tbl_admin.FindAsync(id);
//            if (deleteAdmin==null)
//            {
//                throw new KeyNotFoundException("Admin Not Found");
//            }
//            _myContextDb.tbl_admin.Remove(deleteAdmin);

//          await  _myContextDb.SaveChangesAsync();
//            return deleteAdmin;
//        }
//    }
//}
