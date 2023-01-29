using System.Collections.Generic;
using WordGame.Core.Dto.Base;

namespace WordGame.Core.Dto
{
	public class WordModel:DtoBase<int>
	{
		public string WordName { get; set; }
		public string TurkishTranslator { get; set; }
		public string Description { get; set; }
		public int AddedCount { get; set; }
		public int TrueCount { get; set; }
		public int FalseCount { get; set; }
		public string ExampleSentence { get; set; }
		public string Type { get; set; }
		public List<string> Tags { get; set; }
		public string Pronucation { get; set; }
	}
}