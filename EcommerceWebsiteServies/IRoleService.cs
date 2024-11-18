using EcommerceWebsiteDbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public interface IRoleService
    {
        Task<IList<Role>> GetRolesByUserIdAsync(int userId);
    }
}
