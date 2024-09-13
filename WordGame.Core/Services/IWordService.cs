using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface IWordService : IService
	{
		Task<IEnumerable<WordDto>> GetAsync();
		Task<WordDto> GetAsync(int id);
		Task<WordDto> CreateAsync(WordDto dtoObject);
		Task<WordDto> CreateOrUpdateAsync(WordDto dtoObject);
		Task<WordDto> UpdateAsync(WordDto dtoObject);
		Task<bool> DeleteAsync(int id);
		Task<WordDto> CreateOrIncreaseAsync(WordDto wordModel);
		Task<IEnumerable<WordDto>> GetWordsByNameAsync(string wordName);
		Task<WordDto> GetWordByNameAsync(string wordName);
		Task<bool> IncreaseTrueOrFalseCount(int id, bool isCorrect);
		Task<IEnumerable<WordDto>> GetWordsByStatisticAsync();
	}
}