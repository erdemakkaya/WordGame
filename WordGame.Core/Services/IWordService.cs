using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface IWordService : IService
	{
		Task<IEnumerable<WordModel>> GetAsync();
		Task<WordModel> GetAsync(int id);
		Task<WordModel> CreateAsync(WordModel dtoObject);
		Task<WordModel> CreateOrUpdateAsync(WordModel dtoObject);
		Task<WordModel> UpdateAsync(WordModel dtoObject);
		Task<bool> DeleteAsync(int id);
		Task<WordModel> CreateOrIncreaseAsync(WordModel wordModel);
		Task<IEnumerable<WordModel>> GetWordsByNameAsync(string wordName);
		Task<WordModel> GetWordByNameAsync(string wordName);
		Task<bool> IncreaseTrueOrFalseCount(int id, bool isCorrect);
		Task<IEnumerable<WordModel>> GetWordsByStatisticAsync();
	}
}