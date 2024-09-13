using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface IGrammerService:IService
	{
		Task<IEnumerable<GrammerDto>> GetAsync();
		Task<GrammerDto> GetAsync(int id);
		Task<GrammerDto> CreateAsync(GrammerDto dtoObject);
		Task<GrammerDto> CreateOrUpdateAsync(GrammerDto dtoObject);
		Task<GrammerDto> UpdateAsync(GrammerDto dtoObject);
		Task<bool> DeleteAsync(int id);

		Task<IEnumerable<GrammerDto>> GetGrammersByNameAsync(string name);
		Task<GrammerDto> GetGrammerByNameAsync(string name);
	}
}