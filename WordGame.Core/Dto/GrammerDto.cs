using System.Collections.Generic;
using WordGame.Core.Dto.Base;

namespace WordGame.Core.Dto
{
	public class GrammerDto:DtoBase<int>
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public string BasicRules { get; set; }
		public string Hints { get; set; }
		public List<string> Tags { get; set; }
		public string ExampleSentence { get; set; }
	}
}