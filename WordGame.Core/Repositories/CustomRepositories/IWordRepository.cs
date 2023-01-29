using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Entities;
using WordGame.Core.Repositories.Base.Interfaces;

namespace WordGame.Core.Repositories.CustomRepositories
{
	public interface IWordRepository:IRepository<Word,int>
	{
		Task<IEnumerable<Word>> GetRandomWordsAsync(int count);
		Task<Word> GetWordByNameAsync(string name);
		Task<IEnumerable<Word>> GetWordsByNameAsync(string name);
		Task<IEnumerable<Word>> GetWordsByStatistic();
		Task<Word> IncreaseAddedWordCountAsync(string name);
	}
}