using System.Threading.Tasks;
using WordGame.Core.Entities;
using WordGame.Core.Repositories.Base.Interfaces;

namespace WordGame.Core.Repositories.CustomRepositories
{
	public interface IGrammerRepository:IRepository<Grammer,int>
	{
		Task<Grammer> GetGrammerByNameAsync(string name);
	}
}