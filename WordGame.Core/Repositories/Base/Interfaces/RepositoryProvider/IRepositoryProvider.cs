using Microsoft.EntityFrameworkCore;
using System;
using WordGame.Core.Context;
using WordGame.Core.Entities.Base.Interfaces;

namespace WordGame.Core.Repositories.Base.Interfaces.RepositoryProvider
{
	public interface IRepositoryProvider
	{
		public BaseContext DbContext { get; set; }

		public IRepository<T, TId> GetDefaultRepository<T, TId>() where T : class, IEntityBase<TId>;

		public T GetCustomRepository<T>(Func<BaseContext, object> factory = null) where T: class,IRepository;
	}
}
