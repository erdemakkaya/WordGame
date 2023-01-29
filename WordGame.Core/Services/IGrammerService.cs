using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface IGrammerService:IService
	{
		Task<IEnumerable<GrammerModel>> GetAsync();
		Task<GrammerModel> GetAsync(int id);
		Task<GrammerModel> CreateAsync(GrammerModel dtoObject);
		Task<GrammerModel> CreateOrUpdateAsync(GrammerModel dtoObject);
		Task<GrammerModel> UpdateAsync(GrammerModel dtoObject);
		Task<bool> DeleteAsync(int id);

		Task<IEnumerable<GrammerModel>> GetGrammersByNameAsync(string name);
		Task<GrammerModel> GetGrammerByNameAsync(string name);
	}
}