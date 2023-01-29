using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Repositories.Base.Interfaces;

namespace WordGame.Core.UnitOfWorks
{
	public interface IUnitofWork : IDisposable
	{
		IRepository<TEntity, TId> GetDefaultRepo<TEntity, TId>() where TEntity : class, IEntityBase<TId>;
		TRepo GetCustomRepository<TRepo>() where TRepo : class,IRepository;
		void Commit();
		void CommitAsync();
		void Rollback();
		void RollbackAsync();
		int SaveChanges(bool ensureAutoHistory = false);
		Task <int> SaveChangesAsync(bool ensureAutoHistory = false);
	}
}
