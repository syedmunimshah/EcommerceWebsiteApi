//using EcommerceWebsiteDbConnection;
//using EcommerceWebsiteRepository;
//using EcommerceWebsiteServies.DTO;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace EcommerceWebsiteServies
//{
//    public class CustomerService : ICustomerService
//    {
//        private readonly IGenericRepository<Customer> _IGenericRepository;

//        private readonly myContextDb _myContextDb;
//        private readonly IConfiguration _configuration;
//        private readonly IWebHostEnvironment _env;

//        public CustomerService(IGenericRepository<Customer> genericRepository, myContextDb myContextDb ,IConfiguration configuration, IWebHostEnvironment env)
//        {
//            _IGenericRepository = genericRepository;
//            _myContextDb = myContextDb;
//            _configuration = configuration;
//            _env = env;
            
//        }


//        public async Task<IEnumerable<Customer>> GetAllCustomer() 
        
//        {

//               return await _myContextDb.tbl_Customer.ToListAsync();
            


//        }

//        public async Task<Customer> RegisterCustomer(CustomerDTO customerDTO)
//        {
//            var existingCustomer = await _myContextDb.tbl_Customer.FirstOrDefaultAsync(x=>x.Email==customerDTO.Email);

//            if (existingCustomer !=null) 
//            {
//                throw new KeyNotFoundException("Emai is Already Exist");
//            }

//                Customer customer = new Customer();

//                customer.Name = customerDTO.Name;
//                customer.Email = customerDTO.Email;
//                customer.Phone = customerDTO.Phone;
//                customer.Password = customerDTO.Password;
//                customer.Country = customerDTO.Country;
//                customer.City = customerDTO.City;
//                customer.Address = customerDTO.Address;
//                customer.Gender = customerDTO.Gender;

//                //image create and save it database 
//                string uniqueFileName = $"{Guid.NewGuid()}_{customerDTO.ImageCustomer.FileName}";
//                string ImagePath = Path.Combine(_env.WebRootPath, "CustomerImage",uniqueFileName);
//                FileStream fs = new FileStream(ImagePath,FileMode.Create);
//                customerDTO.ImageCustomer.CopyTo(fs);

//            customer.Image = uniqueFileName;

//            //Image = customerDTO.Image,

//            await _myContextDb.tbl_Customer.AddAsync(customer);
//                await _myContextDb.SaveChangesAsync();
//            return customer;


//        }

//        public async Task<string> LoginCustomer(LoginCustomerDTO LoginCustomerDTO) 
//        {
//            if (LoginCustomerDTO.Email != null && LoginCustomerDTO.Password != null)
//            {
//                var user = _myContextDb.tbl_Customer.FirstOrDefault(x => x.Email == LoginCustomerDTO.Email && x.Password == LoginCustomerDTO.Password);
//                var UserId = user.Id;
//                if (user != null)
//                {
//                    var claims = new List<Claim>
//                {
//                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
//                    new Claim("Id",user.Id.ToString()),
//                    new Claim("Email",user.Name.ToString()),

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
                   
//                    await TokenSaveDatabase(tokenValue, UserId);
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
        
//        public async Task<TokenSave> TokenSaveDatabase(string tokenValue, int UserId) 
//        {
           

//            TokenSave tokenSave = new TokenSave();
//            tokenSave.UserId = UserId;
//            tokenSave.Token = tokenValue;



//            if (tokenSave == null)
//            {
//                throw new KeyNotFoundException("Token Is Null.");
//            }
//            await _myContextDb.tbl_tokenSave.AddAsync(tokenSave);
//            await _myContextDb.SaveChangesAsync();
//            return tokenSave;

//        }

//        public async Task<string> UpdateCustomer(CustomerDTO customerDTO)
//        {

//            var entity = await _myContextDb.tbl_Customer.FindAsync(customerDTO.Id);
//            if(entity == null)
//            {
//                return $"Customer with ID {customerDTO.Id} not found.";
//            }

//            entity.Name = customerDTO.Name;
//            entity.Email = customerDTO.Email;
//            entity.Phone = customerDTO.Phone;
//            entity.Password = customerDTO.Password;
//            entity.Country = customerDTO.Country;
//            entity.City = customerDTO.City;
//            entity.Address = customerDTO.Address;
//            entity.Gender = customerDTO.Gender;

//            string uniqueFileName = $"{Guid.NewGuid()}_{customerDTO.ImageCustomer.FileName}";
//            string ImagePath = Path.Combine(_env.WebRootPath, "CustomerImage", uniqueFileName);
//            FileStream fs = new FileStream(ImagePath,FileMode.Create);
//            customerDTO.ImageCustomer.CopyTo(fs);
//            entity.Image = uniqueFileName;

//             _myContextDb.tbl_Customer.Update(entity);
//            _myContextDb.SaveChanges();
//            return $"Customer with ID {customerDTO.Id} updated successfully.";



//        }

//        public async Task<string> DeleteCustomer(int id)
//        {
//           var entity= await _myContextDb.tbl_Customer.FindAsync(id);
//            if(entity != null)
//            {
//                _myContextDb.tbl_Customer.Remove(entity);
//                _myContextDb.SaveChanges();
//                return $"Delete this {id}";

//            }
//            else
//            {
//                return $"Customer with ID {id} not found.";
//            }


//        }

      
//    }
//}//using EcommerceWebsiteDbConnection;
//using EcommerceWebsiteRepository;
//using EcommerceWebsiteServies.DTO;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace EcommerceWebsiteServies
//{
//    public class CustomerService : ICustomerService
//    {
//        private readonly IGenericRepository<Customer> _IGenericRepository;

//        private readonly myContextDb _myContextDb;
//        private readonly IConfiguration _configuration;
//        private readonly IWebHostEnvironment _env;

//        public CustomerService(IGenericRepository<Customer> genericRepository, myContextDb myContextDb ,IConfiguration configuration, IWebHostEnvironment env)
//        {
//            _IGenericRepository = genericRepository;
//            _myContextDb = myContextDb;
//            _configuration = configuration;
//            _env = env;
            
//        }


//        public async Task<IEnumerable<Customer>> GetAllCustomer() 
        
//        {

//               return await _myContextDb.tbl_Customer.ToListAsync();
            


//        }

//        public async Task<Customer> RegisterCustomer(CustomerDTO customerDTO)
//        {
//            var existingCustomer = await _myContextDb.tbl_Customer.FirstOrDefaultAsync(x=>x.Email==customerDTO.Email);

//            if (existingCustomer !=null) 
//            {
//                throw new KeyNotFoundException("Emai is Already Exist");
//            }

//                Customer customer = new Customer();

//                customer.Name = customerDTO.Name;
//                customer.Email = customerDTO.Email;
//                customer.Phone = customerDTO.Phone;
//                customer.Password = customerDTO.Password;
//                customer.Country = customerDTO.Country;
//                customer.City = customerDTO.City;
//                customer.Address = customerDTO.Address;
//                customer.Gender = customerDTO.Gender;

//                //image create and save it database 
//                string uniqueFileName = $"{Guid.NewGuid()}_{customerDTO.ImageCustomer.FileName}";
//                string ImagePath = Path.Combine(_env.WebRootPath, "CustomerImage",uniqueFileName);
//                FileStream fs = new FileStream(ImagePath,FileMode.Create);
//                customerDTO.ImageCustomer.CopyTo(fs);

//            customer.Image = uniqueFileName;

//            //Image = customerDTO.Image,

//            await _myContextDb.tbl_Customer.AddAsync(customer);
//                await _myContextDb.SaveChangesAsync();
//            return customer;


//        }

//        public async Task<string> LoginCustomer(LoginCustomerDTO LoginCustomerDTO) 
//        {
//            if (LoginCustomerDTO.Email != null && LoginCustomerDTO.Password != null)
//            {
//                var user = _myContextDb.tbl_Customer.FirstOrDefault(x => x.Email == LoginCustomerDTO.Email && x.Password == LoginCustomerDTO.Password);
//                var UserId = user.Id;
//                if (user != null)
//                {
//                    var claims = new List<Claim>
//                {
//                    new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
//                    new Claim("Id",user.Id.ToString()),
//                    new Claim("Email",user.Name.ToString()),

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
                   
//                    await TokenSaveDatabase(tokenValue, UserId);
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
        
//        public async Task<TokenSave> TokenSaveDatabase(string tokenValue, int UserId) 
//        {
           

//            TokenSave tokenSave = new TokenSave();
//            tokenSave.UserId = UserId;
//            tokenSave.Token = tokenValue;



//            if (tokenSave == null)
//            {
//                throw new KeyNotFoundException("Token Is Null.");
//            }
//            await _myContextDb.tbl_tokenSave.AddAsync(tokenSave);
//            await _myContextDb.SaveChangesAsync();
//            return tokenSave;

//        }

//        public async Task<string> UpdateCustomer(CustomerDTO customerDTO)
//        {

//            var entity = await _myContextDb.tbl_Customer.FindAsync(customerDTO.Id);
//            if(entity == null)
//            {
//                return $"Customer with ID {customerDTO.Id} not found.";
//            }

//            entity.Name = customerDTO.Name;
//            entity.Email = customerDTO.Email;
//            entity.Phone = customerDTO.Phone;
//            entity.Password = customerDTO.Password;
//            entity.Country = customerDTO.Country;
//            entity.City = customerDTO.City;
//            entity.Address = customerDTO.Address;
//            entity.Gender = customerDTO.Gender;

//            string uniqueFileName = $"{Guid.NewGuid()}_{customerDTO.ImageCustomer.FileName}";
//            string ImagePath = Path.Combine(_env.WebRootPath, "CustomerImage", uniqueFileName);
//            FileStream fs = new FileStream(ImagePath,FileMode.Create);
//            customerDTO.ImageCustomer.CopyTo(fs);
//            entity.Image = uniqueFileName;

//             _myContextDb.tbl_Customer.Update(entity);
//            _myContextDb.SaveChanges();
//            return $"Customer with ID {customerDTO.Id} updated successfully.";



//        }

//        public async Task<string> DeleteCustomer(int id)
//        {
//           var entity= await _myContextDb.tbl_Customer.FindAsync(id);
//            if(entity != null)
//            {
//                _myContextDb.tbl_Customer.Remove(entity);
//                _myContextDb.SaveChanges();
//                return $"Delete this {id}";

//            }
//            else
//            {
//                return $"Customer with ID {id} not found.";
//            }


//        }

      
//    }
//}
