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


        public async Task<List<UserDTO>> GetAllUser()
        {
            try
            {
                var users = await _myContextDb.tbl_users.ToListAsync();
                return users.Select(x => new UserDTO { Id = x.Id,Name=x.Name,CreateAt=x.CreateAt}).ToList();
                 
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
                CreateAt=users.CreateAt,
            };
            return userDTO;

        }


        public async Task<UserDTO> AddUser(UserAddDTO user)
        {
            try
            {
              
                var username =await _myContextDb.tbl_users.FirstOrDefaultAsync(x=>x.Name==user.Name);
                if (username != null)
                {
                    throw new KeyNotFoundException("User name is Already");
                }

                User user1 = new User() { Name = user.Name };
                await _myContextDb.tbl_users.AddAsync(user1);
                await _myContextDb.SaveChangesAsync();

                UserDTO user2=new UserDTO 
                { 
                    Id=user1.Id, 
                    Name=user1.Name,
                    CreateAt=user1.CreateAt 
                };
                return user2;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<UserDTO> UpdateUser(UserUpdateDTO userDto) 
        {
          
            var entity = await _myContextDb.tbl_users.FindAsync(userDto.Id);
            if (entity == null)
            {

                 throw new KeyNotFoundException("User Not Found");
            }

            entity.Name = userDto.Name;
            _myContextDb.tbl_users.Update(entity);
             await _myContextDb.SaveChangesAsync();

            UserDTO UserFrontView = new UserDTO();
            UserFrontView.Id = entity.Id;
            UserFrontView.Name= entity.Name;
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
