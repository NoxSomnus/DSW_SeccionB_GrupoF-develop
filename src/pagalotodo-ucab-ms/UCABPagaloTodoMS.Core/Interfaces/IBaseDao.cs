using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCABPagaloTodoMS.Core.Entities;
using UCABPagaloTodoMS.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace UCABPagaloTodoMS.Core.Interfaces
{
    // Interfaz DAO genérica
    public interface IBaseDao<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByUserAsync(string username);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        //  Task DeleteAsync(T entity);
    }

    public interface IUserDao : IBaseDao<UserEntity>
    {
        Task<List<UserEntity>> GetProvidersAsync();
    }

    // Clase DAO para la entidad UserEntity
    public class UserDao : IBaseDao<UserEntity>
    {
        private readonly IUCABPagaloTodoDbContext _dbContext;

        public UserDao(IUCABPagaloTodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _dbContext.UserEntities.ToListAsync();
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await _dbContext.UserEntities.FindAsync(id);
        }
        public async Task<UserEntity> GetByUserAsync(string username)
        {
            return await _dbContext.UserEntities.FindAsync(username);
        }

        public async Task AddAsync(UserEntity entity)
        {
            await _dbContext.UserEntities.AddAsync(entity);
            await _dbContext.SaveEfContextChanges("APP");
        }

        public async Task UpdateAsync(UserEntity entity)
        {
            _dbContext.UserEntities.Update(entity);
            await _dbContext.SaveEfContextChanges("APP");
        }
        public async Task<List<UserEntity>> GetProvidersAsync()
        {
            return await _dbContext.UserEntities
                .Where(u => u is ProviderEntity)
                .ToListAsync();
        }

        /*  public async Task DeleteAsync(UserEntity entity)
          {
              _dbContext.UserEntities.Remove(entity);
              await _dbContext.SaveChangesAsync();
          }*/
    }
}
