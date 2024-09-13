using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WordGame.Core.Context;
using WordGame.Core.Entities;
using WordGame.Core.Entities.Base.Interfaces;

namespace WordGame.Infrastructure.Data
{
	public class WordGameContext : BaseContext
	{
		public WordGameContext(DbContextOptions<WordGameContext> options) : base(options)
		{
		}

		public WordGameContext() : base()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var isDesignTime = optionsBuilder.Options.Extensions
			.OfType<CoreOptionsExtension>()
			.Any(e => e.IsSensitiveDataLoggingEnabled);

			if (isDesignTime)
			{
				optionsBuilder.UseNpgsql("User ID=postgres;Password=siemens1234;Server=localhost;Port=5433;Database=WordDb;Integrated Security=true;Pooling=true;");
			}
		}

		public override Task<int> SaveChangesAsync(
		   bool acceptAllChangesOnSuccess,
		   CancellationToken cancellationToken = default)
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