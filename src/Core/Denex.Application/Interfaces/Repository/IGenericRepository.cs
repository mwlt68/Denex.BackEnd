using Denex.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Denex.Application.Repository
{
    public interface IGenericRepository<T, in TKey> where T : class, IBaseEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(TKey id);
        Task<T> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(TKey id, T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(TKey id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);
    }
}
