using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WordGame.Core.Entities;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Infrastructure.Repository.Base;

namespace WordGame.Infrastructure.Repository
{
	public class GrammerRepository : Repository<Grammer,int>, IGrammerRepository
	{
		public GrammerRepository(DbContext dbContext) : base(dbContext)
		{
		}
		public async Task<Grammer> GetGrammerByNameAsync(string name)
		{
			var entity = await FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
			return entity;
		}
	}
}
