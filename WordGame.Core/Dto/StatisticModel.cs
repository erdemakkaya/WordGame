using WordGame.Core.Dto.Base.Interfaces;

namespace WordGame.Core.Dto
{
	public class StatisticModel: IDto
	{
		public int WordsCount { get; set; }
		public int CompletedWordsCount { get; set; }
		public string MaxWrongAnswerCountWord { get; set; }
		public int GrammerTopicsCount { get; set; }
		public int TrueAnswerCount { get; set; }
		public int WrongAnswerCount { get; set; }
		public double ScoreRate => TrueAnswerCount / (WrongAnswerCount+TrueAnswerCount);

	}
}
