using EcommerceWebsiteDbConnection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public class RoleService: IRoleService
    {
        private readonly myContextDb _context;

        public RoleService(myContextDb context)
        {
            _context = context;
        }

        public async Task<IList<Role>> GetRolesByUserIdAsync(int userId)
        {
            var roles = await _context.tbl_UserRole
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.role)
                .ToListAsync();

            return roles;
        }
    }
}
