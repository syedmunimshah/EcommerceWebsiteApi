using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GellAll();
        Task Add(T entity);
        Task Update(T entity,int id);
        Task Delete(int id);
    }
}
