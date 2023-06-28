using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Context;
using WordGame.Core.Entities;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Infrastructure.Repository.Base;

namespace WordGame.Infrastructure.Repository.Providers
{
    internal class Factory
    {
        private readonly IDictionary<Type, Func<BaseContext, object>> _factoryCache;

        public Factory()
        {
            _factoryCache = GetFactories();
        }

        public Func<BaseContext, object> GetRepositoryFactoryForEntityType<T,TId>()
            where T : class, IEntityBase<TId>
        {
            Func<BaseContext, object> factory = GetRepositoryFactoryFromCache<T>();
            if (factory != null)
            {
                return factory;
            }

			return GetDefaultRepo<T, TId>();
        }

        public Func<BaseContext, object> GetRepositoryFactoryFromCache<T>()
        {
            Func<BaseContext, object> factory;
            _factoryCache.TryGetValue(typeof(T), out factory);
            return factory;
        }

        private IDictionary<Type, Func<BaseContext, object>> GetFactories()
        {
            Dictionary<Type, Func<BaseContext, object>> dic = new Dictionary<Type, Func<BaseContext, object>>();

            dic.Add(typeof(IWordRepository), context => new WordRepository(context));
            dic.Add(typeof(IGrammerRepository), context => new GrammerRepository(context));
            return dic;
        }

        private Func<BaseContext, object> GetDefaultRepo<T,TId>() where T : class, IEntityBase<TId>
        { 
            return dbContext => new Repository<T,TId>(dbContext);
        }
    }
}
