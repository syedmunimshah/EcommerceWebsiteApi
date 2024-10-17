
using EcommerceWebsiteDbConnection;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWebsiteRepository
{
   
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly myContextDb _myContextDb;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(myContextDb myContextDb)
        {
            _myContextDb= myContextDb;
            _dbSet=_myContextDb.Set<T>();
        }

        public async Task<IEnumerable<T>> GellAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Add(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _myContextDb.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Update(T entity, int id)
        {
            try
            {
                var existingEntity = await _dbSet.FindAsync(id);
                if (existingEntity != null)
                {
                    _dbSet.Entry(existingEntity).CurrentValues.SetValues(entity);
                   await _myContextDb.SaveChangesAsync();
                    
                }
                else
                {
                    throw new KeyNotFoundException($"Entity with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task Delete(int id)
        {
            try
            {
               var existingEntity= await _dbSet.FindAsync(id);
                if (existingEntity != null) 
                {
                    _dbSet.Remove(existingEntity);
                   await _myContextDb.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       

      
    }
}
