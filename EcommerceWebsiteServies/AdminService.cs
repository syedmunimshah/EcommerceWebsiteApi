using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public class AdminService
    {
        private readonly myContextDb _myContextDb;
        public AdminService(myContextDb myContextDb)
        {
            _myContextDb = myContextDb;
        }
         
        public async Task<IEnumerable<Admin>> GetAllAdmin()
        {
            return await _myContextDb.tbl_admin.ToListAsync();
        }

        public async Task<AdminDTO> AddAdmin(AdminDTO admin)
        {
            Admin AddAdmin = new Admin();
            AddAdmin.Name = admin.Name;
            AddAdmin.Email = admin.Email;
            AddAdmin.Password = admin.Password;
            AddAdmin.Image = admin.Image;
            await _myContextDb.tbl_admin.AddAsync(AddAdmin);
            await _myContextDb.SaveChangesAsync();
            return admin;
        }


        public async Task UpdateAdmin(AdminDTO adminDTO)
        {
            var adminEntity = await _myContextDb.tbl_admin.FindAsync(adminDTO.Id);
            if (adminEntity==null) {
                throw new KeyNotFoundException("Admin Not Found");
            }
            adminEntity.Name = adminDTO.Name;
            adminEntity.Email = adminDTO.Email;
            adminEntity.Password = adminDTO.Password;
            adminEntity.Image = adminDTO.Image;
            _myContextDb.tbl_admin.Update(adminEntity);
            await _myContextDb.SaveChangesAsync();
          


        }


        public async Task<Admin> AdminGetById(int id) {

            var Adminentity = await _myContextDb.tbl_admin.FindAsync(id);
            if (Adminentity==null) {
                throw new KeyNotFoundException("Admin Not Found");
            }
            return Adminentity;
            
        }

        public async Task<Admin> DeleteAdmin(int id) {

            var deleteAdmin =await _myContextDb.tbl_admin.FindAsync(id);
            if (deleteAdmin==null)
            {
                throw new KeyNotFoundException("Admin Not Found");
            }
            _myContextDb.tbl_admin.Remove(deleteAdmin);

          await  _myContextDb.SaveChangesAsync();
            return deleteAdmin;
        }
    }
}
