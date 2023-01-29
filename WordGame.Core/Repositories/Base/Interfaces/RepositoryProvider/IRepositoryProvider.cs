using Microsoft.EntityFrameworkCore;
using System;
using WordGame.Core.Entities.Base.Interfaces;

namespace WordGame.Core.Repositories.Base.Interfaces.RepositoryProvider
{
	public interface IRepositoryProvider
	{
		public DbContext DbContext { get; set; }

		public IRepository<T, TId> GetDefaultRepository<T, TId>() where T : class, IEntityBase<TId>;

		public T GetCustomRepository<T>(Func<DbContext, object> factory = null) where T: class,IRepository;
	}
}
