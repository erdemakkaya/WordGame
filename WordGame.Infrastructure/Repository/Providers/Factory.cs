using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Entities;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Infrastructure.Repository.Base;

namespace WordGame.Infrastructure.Repository.Providers
{
    internal class Factory
    {
        private readonly IDictionary<Type, Func<DbContext, object>> _factoryCache;

        public Factory()
        {
            _factoryCache = GetFactories();
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T,TId>()
            where T : class, IEntityBase<TId>
        {
            Func<DbContext, object> factory = GetRepositoryFactoryFromCache<T>();
            if (factory != null)
            {
                return factory;
            }

			return GetDefaultRepo<T, TId>();
        }

        public Func<DbContext, object> GetRepositoryFactoryFromCache<T>()
        {
            Func<DbContext, object> factory;
            _factoryCache.TryGetValue(typeof(T), out factory);
            return factory;
        }

        private IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            Dictionary<Type, Func<DbContext, object>> dic = new Dictionary<Type, Func<DbContext, object>>();

            dic.Add(typeof(IWordRepository), context => new WordRepository(context));
            dic.Add(typeof(IGrammerRepository), context => new GrammerRepository(context));
            return dic;
        }

        private Func<DbContext, object> GetDefaultRepo<T,TId>() where T : class, IEntityBase<TId>
        { 
            return dbContext => new Repository<T,TId>(dbContext);
        }
    }
}
