﻿using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;



namespace EcommerceWebsiteServies
{
    public class AdminService
    {
        private readonly myContextDb _myContextDb;
        private readonly IWebHostEnvironment _env;

        public AdminService(myContextDb myContextDb, IWebHostEnvironment env)
        {
            _myContextDb = myContextDb;
            _env= env;
        }
         
        public async Task<IEnumerable<Admin>> GetAllAdmin()
        {
            return await _myContextDb.tbl_admin.ToListAsync();
        }

        public async Task<Admin> AddAdmin(AdminDTO admin)
        {
            try
            {
                Admin AddAdmin = new Admin();
                AddAdmin.Name = admin.Name;
                AddAdmin.Email = admin.Email;
                AddAdmin.Password = admin.Password;

                //image create and save it database 
                string uniqueFileName = $"{Guid.NewGuid()}_{admin.AdminImage.FileName}";
                string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", uniqueFileName);

                //string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", $"{Guid.NewGuid()}", admin.AdminImage.FileName);

                FileStream fs = new FileStream(ImagePath, FileMode.Create);
                admin.AdminImage.CopyTo(fs);

                //AddAdmin.Image = admin.AdminImage.FileName;
                AddAdmin.Image = uniqueFileName;

                await _myContextDb.tbl_admin.AddAsync(AddAdmin);
                await _myContextDb.SaveChangesAsync();
                return AddAdmin;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding the admin.", ex);
            }
        }


        public async Task<Admin> UpdateAdmin(AdminDTO adminDTO)
        {
            var adminEntity = await _myContextDb.tbl_admin.FindAsync(adminDTO.Id);
            if (adminEntity == null)
            {
                throw new KeyNotFoundException("Admin Not Found");
            }

            adminEntity.Name = adminDTO.Name;
            adminEntity.Email = adminDTO.Email;
            adminEntity.Password = adminDTO.Password;

            //image create and save it database
            string uniqueFileName = $"{Guid.NewGuid()}_{adminDTO.AdminImage.FileName}";
            string ImagePath = Path.Combine(_env.WebRootPath, "AdminImages", uniqueFileName);
            FileStream fs = new FileStream(ImagePath, FileMode.Create);
            adminDTO.AdminImage.CopyTo(fs);

            adminEntity.Image = uniqueFileName;
            _myContextDb.tbl_admin.Update(adminEntity);
            await _myContextDb.SaveChangesAsync();
            return adminEntity;


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
