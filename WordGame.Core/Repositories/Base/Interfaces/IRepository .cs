using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Specifications.Base.Interfaces;

namespace WordGame.Core.Repositories.Base.Interfaces
{
    public interface IRepository<T,TId> : IRepository where T : class, IEntityBase<TId>
    {

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetRandomlyAsync(int take);

        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);
        //Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        //Task<int> CountAsync(ISpecification<T> spec);

    }
    public interface IRepository { }
}
