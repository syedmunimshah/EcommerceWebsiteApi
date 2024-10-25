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
namespace EcommerceWebsiteServies
{
    public class AuthService
    {
        private readonly myContextDb _myContextDb;
        public AuthService(myContextDb myContextDb)
        {
            _myContextDb= myContextDb;
        }


        public async Task<List<UserDto>> GetAllUser()
        {
            try
            {
                var users = await _myContextDb.tbl_users.ToListAsync();
                List<UserDto> usersList = new List<UserDto>();
               
                foreach (var user in users)
                {
                    UserDto userdto = new UserDto();
                    userdto.Id = user.Id;
                    userdto.Name = user.Name;
                    usersList.Add(userdto);
                }
                return usersList;

                //var users =await _myContextDb.tbl_users.ToListAsync();

                //return users.Select(u => new UserDto {Id=u.Id,Name=u.Name }).ToList();
                 
            }
              
            
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var users = await _myContextDb.tbl_users.FindAsync(id);
            if (users==null) {
                throw new KeyNotFoundException("User Not Found");
            }
            UserDto userDto = new UserDto();
            userDto.Id = users.Id;
            userDto.Name = users.Name;
            return userDto;

        }

        public async Task AddUser(UserDto userDto)
        {
            try
            {
                if (userDto != null)
                {
                    User user = new User();

                    user.Name = userDto.Name;
                        
                    
                   await _myContextDb.tbl_users.AddAsync(user);
                   await _myContextDb.SaveChangesAsync();

                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<UserDto> UpdateUser(UserDto userDto) 
        {
          
            var entity = await _myContextDb.tbl_users.FindAsync(userDto.Id);
            if (entity == null)
            {

                 throw new KeyNotFoundException("User Not Found");
            }

            entity.Name = userDto.Name;
            _myContextDb.tbl_users.Update(entity);
             await _myContextDb.SaveChangesAsync();

            UserDto UserFrontView = new UserDto();
            UserFrontView.Id = userDto.Id;
            UserFrontView.Name=userDto.Name;

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
        public async Task<IEnumerable<RoleDTO>> GetAllRoles() {
            try
            {
              var roles= await _myContextDb.tbl_role.ToListAsync();
                return roles.Select(r => new RoleDTO { id = r.Id, Name = r.Name }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RoleDTO> GetRoleById(int id) {
            var Roles =await _myContextDb.tbl_role.FindAsync(id);
            if (Roles == null)
            {
                throw new KeyNotFoundException("Role Not Foundation");
            }
            RoleDTO role = new RoleDTO();
            role.id = Roles.Id;
            role.Name = Roles.Name;
            return role;

        }

        public async Task<RoleDTO> AddRole(RoleDTO role) {

            Role roleAdd = new Role();
            roleAdd.Name = role.Name;

            await _myContextDb.AddAsync(roleAdd);
            await _myContextDb.SaveChangesAsync();

            RoleDTO role1 = new RoleDTO();
            role1.id = roleAdd.Id;
            role1.Name = roleAdd.Name;

            return role1;
        }

        public async Task<RoleDTO> UpdateRole(RoleDTO roleDTO) {
            var entity = await _myContextDb.tbl_role.FindAsync(roleDTO.id);
            if (entity==null) {
                throw new KeyNotFoundException("Not found Role");
            }

            entity.Name=roleDTO.Name;

            _myContextDb.tbl_role.Update(entity);
            await _myContextDb.SaveChangesAsync();

            RoleDTO role= new RoleDTO();
            role.id = entity.Id;
            role.Name = entity.Name;
            return role;

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
                return  await _myContextDb.tbl_userRoles.Include(r => r.role).Include(u => u.user).Select(ur => new UserRoleDTO
                {
                    Id = ur.Id,
                    UserId = ur.UserId,
                    UserName = ur.user.Name,
                    RoleId = ur.RoleId,
                    RoleName = ur.role.Name
                }).ToListAsync();

                    

            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<UserRole> AddUserRole(UserRoleDTO userRoleADO)
        {
            //Object Initializer(Approach 1)
            //UserRole userRole = new UserRole()
            //{
            //    UserId = userRoleADO.UserId,
            //    RoleId = userRoleADO.RoleId
            //};
            //await _myContextDb.tbl_userRoles.AddAsync(userRole);
            //await _myContextDb.SaveChangesAsync();
            //return userRole;



            //Separate Assignment(Approach 2)
            UserRole userRole = new UserRole();
            userRole.UserId = userRoleADO.UserId;
            userRole.RoleId = userRoleADO.RoleId;
            await _myContextDb.tbl_userRoles.AddAsync(userRole);
            await _myContextDb.SaveChangesAsync();
            return userRole;


        }

        public async Task<UserRole> UpdateUserRole(UserRoleDTO userRoleADO) {

            var entity = await _myContextDb.tbl_userRoles.FindAsync(userRoleADO.Id);
            if (entity==null) 
            {
                throw new KeyNotFoundException("Not Found UserRole"); 
            }
            entity.UserId=userRoleADO.UserId;
            entity.RoleId=userRoleADO.RoleId;
            _myContextDb.tbl_userRoles.Update(entity);
            await _myContextDb.SaveChangesAsync();
            return entity;

        }

        public async Task<UserRole> DeleteUserRole(int id)
        {

            var entity = await _myContextDb.tbl_userRoles.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Not Found UserRole");
            }
          
            _myContextDb.tbl_userRoles.Remove(entity);
            await _myContextDb.SaveChangesAsync();
            return entity;

        }

     



    }
}
