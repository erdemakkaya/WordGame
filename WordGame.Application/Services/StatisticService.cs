using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services;

namespace WordGame.Application.Services
{
	public class StatisticService : IStatisticService
	{
		IWordService _wordService;
		IGrammerService _grammerService;

		public StatisticService(IWordService wordService,IGrammerService grammerService)
		{
			_wordService = wordService;
			_grammerService = grammerService;
		}
		public async Task<StatisticModel> GetStatistic()
		{
			var words = await _wordService.GetAsync();
			var grammers = await _grammerService.GetAsync();
			var statisticModel = new StatisticModel();

			statisticModel.WordsCount = words.Count();
			statisticModel.GrammerTopicsCount = grammers.Count();
			statisticModel.TrueAnswerCount = words.Sum(x => x.TrueCount);
			statisticModel.WrongAnswerCount = words.Sum(x => x.FalseCount);
			statisticModel.MaxWrongAnswerCountWord = words.OrderByDescending(x => x.FalseCount).FirstOrDefault().WordName;
			statisticModel.CompletedWordsCount = words.Count(x => x.TrueCount - (x.FalseCount + x.AddedCount) >= 10);  
				
			return  statisticModel;
		}
	}
}
