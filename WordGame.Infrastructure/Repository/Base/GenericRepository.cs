using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Repositories.Base.Interfaces;
using WordGame.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WordGame.Infrastructure.Repository.Base
{
	public class Repository<T,TId> : IRepository<T,TId> where T : class, IEntityBase<TId>
	{
		private readonly DbContext _dbContext;
		private readonly DbSet<T> _dbSet;


		public Repository(DbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_dbSet = _dbContext.Set<T>();
		}

		

		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		//public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
		//{
		//    return await ApplySpecification(spec).ToListAsync();
		//}

		//public async Task<int> CountAsync(ISpecification<T> spec)
		//{
		//    return await ApplySpecification(spec).CountAsync();
		//}

		//private IQueryable<T> ApplySpecification(ISpecification<T> spec)
		//{
		//    return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
		//}

		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
		{
			IQueryable<T> query = _dbSet;
			if (disableTracking) query = query.AsNoTracking();

			if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return await orderBy(query).ToListAsync();
			return await query.ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
		{
			IQueryable<T> query = _dbSet;
			if (disableTracking) query = query.AsNoTracking();

			if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return await orderBy(query).ToListAsync();
			return await query.ToListAsync();
		}

		public virtual async Task<T> GetByIdAsync(TId id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			return entity;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
		{
			return await _dbSet.CountAsync(predicate);
		}

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null)
		{
			var entity = await _dbSet.FirstOrDefaultAsync(predicate);
			return entity;
		}

		public async Task<IReadOnlyList<T>> GetRandomlyAsync(int take)
		{
			IQueryable<T> query = _dbSet;
			var result = query.OrderBy(x => Guid.NewGuid()).Take(take).ToListAsync();
			return await result;
		}

	}
}
