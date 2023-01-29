using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Repositories.Base.Interfaces;
using WordGame.Core.UnitOfWorks;
using WordGame.Infrastructure.Repository.Base;
using Microsoft.Extensions.DependencyInjection;
using WordGame.Core.Entities.Base.Interfaces;
using WordGame.Core.Repositories.Base.Interfaces.RepositoryProvider;
using WordGame.Infrastructure.Repository.Providers;

namespace WordGame.Infrastructure.UnitOfWork
{
	public class UnitOfWork<TDbContext> : IUnitofWork
		where TDbContext : DbContext
	{
		private readonly TDbContext _context;
		private readonly IRepositoryProvider _repositoryProvider;
		private IDbContextTransaction _transaction;
		private bool _disposed;

		public UnitOfWork(TDbContext context)
		{
			_context = context;
			_transaction = _context.Database.BeginTransaction();
			if (_repositoryProvider == null)
			{
				_repositoryProvider = new RepositoryProvider();
			}
			_repositoryProvider.DbContext = _context;
		}


		public void Commit()
		{
			_transaction.Commit();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}

			this._disposed = true;
		}

		public TRepo GetCustomRepository<TRepo>() where TRepo : class, IRepository
		{
			return _repositoryProvider.GetCustomRepository<TRepo>();
		}

		public void Rollback()
		{
			_transaction.Rollback();
			_transaction = null;
		}

		public int SaveChanges(bool ensureAutoHistory = false)
		{
			var transaction = _transaction != null ? _transaction : _context.Database.BeginTransaction();
			using (transaction)
			{
				try
				{
					if (_context == null)
					{
						throw new ArgumentException("Context is null");
					}

					if (ensureAutoHistory)
					{
						//_context.EnsureAutoHistory();
					}
					int result = _context.SaveChanges();
					transaction.Commit();
					return result;
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw new Exception("Error on save changes ", ex);
				}
			}
		}

		public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
		{
			var transaction = _transaction != null ? _transaction : _context.Database.BeginTransaction();
			using (transaction)
			{
				try
				{
					if (_context == null)
					{
						throw new ArgumentException("Context is null");
					}

					if (ensureAutoHistory)
					{
						//_context.EnsureAutoHistory();
					}
					int result = await _context.SaveChangesAsync();
					await transaction.CommitAsync();
					return result;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					throw new Exception("Error on save changes ", ex);
				}
			}
		}

		public IRepository<TEntity, TId> GetDefaultRepo<TEntity, TId>() where TEntity : class, IEntityBase<TId>
		{
			return _repositoryProvider.GetDefaultRepository<TEntity, TId>();
		}

		public async void CommitAsync()
		{
			await _transaction.CommitAsync();
		}

		public async void RollbackAsync()
		{
			await _transaction.RollbackAsync();
		}
	}
}