using Denex.Application.Repository;
using Denex.Domain.Common;
using Denex.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Denex.Persistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext context;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public virtual async Task<List<TEntity>> GetAll()
        {
            var entities=await context.Set<TEntity>().ToListAsync();
            return entities;
        }
        public virtual async Task<TEntity?> GetById(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            return entity;
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            var entityResult = await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entityResult.Entity;
        }
    }
}
