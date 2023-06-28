using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Context;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Repositories.Base.Interfaces;
using WordGame.Core.Repositories.Base.Interfaces.RepositoryProvider;
using WordGame.Infrastructure.Repository.Base;

namespace WordGame.Infrastructure.Repository.Providers
{
	internal class RepositoryProvider : IRepositoryProvider
	{

		private readonly Factory _factory;
		protected Dictionary<Type, object> Repositories { get; private set; }
		public BaseContext DbContext { get; set; }

		public RepositoryProvider()
		{
			_factory = new Factory();
			Repositories = new Dictionary<Type, object>();
		}

		public T GetCustomRepository<T>(Func<BaseContext, object> factory = null) where T : class, IRepository
		{
			object repository;
			Repositories.TryGetValue(typeof(T), out repository);
			if (repository != null)
			{
				return (T)repository;
			}
			return CreateRepository<T>(factory, DbContext);
		}

		public IRepository<T, TId> GetDefaultRepository<T, TId>() where T : class, IEntityBase<TId>
		{
			return new Repository<T, TId>(DbContext);
		}
		private T CreateRepository<T>(Func<BaseContext, object> factory, BaseContext dbContext) where T : class, IRepository
		{
			Func<BaseContext, object> repositoryFactory;
			if (factory != null)
			{
				repositoryFactory = factory;
			}
			else
			{
				repositoryFactory = _factory.GetRepositoryFactoryFromCache<T>();
			}
			if (repositoryFactory == null)
			{
				throw new NotSupportedException(typeof(T).FullName);
			}
			T repository = (T)repositoryFactory(dbContext);
			Repositories[typeof(T)] = repository;
			return repository;
		}


	}
}
