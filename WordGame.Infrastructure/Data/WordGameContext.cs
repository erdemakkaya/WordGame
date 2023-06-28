using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WordGame.Core.Context;
using WordGame.Core.Entities;
using WordGame.Core.Entities.Base.Interfaces;


namespace WordGame.Infrastructure.Data
{
	public class WordGameContext :BaseContext
	{
		public WordGameContext(DbContextOptions<WordGameContext> options) : base(options)
		{
		}

		public WordGameContext() { }
		public override Task<int> SaveChangesAsync(
		   bool acceptAllChangesOnSuccess,
		   CancellationToken cancellationToken = default(CancellationToken))
		{
			foreach (var auditableEntity in ChangeTracker.Entries<IFullAudited>())
			{
				if (auditableEntity.State == EntityState.Added ||
					auditableEntity.State == EntityState.Modified)
				{
					auditableEntity.Entity.LastModificationTime = DateTime.Now;
					if (auditableEntity.State == EntityState.Added)
					{
						auditableEntity.Entity.CreateDate = DateTime.Now;
					}
				}
			}
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}


		public virtual DbSet<Word> Words { get; set; }
		public DbSet<Grammer> Grammers { get; set; }

		public DbSet<Select> Selects { get; set; }
		public DbSet<Episode> Episodes { get; set; }
		public DbSet<Series> Series { get; set; }
		public DbSet<Subtitle> Subtitles { get; set; }

	}
}