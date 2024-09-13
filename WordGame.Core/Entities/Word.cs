using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WordGame.Core.Entities.Base;

namespace WordGame.Core.Entities
{
	public class Word : EntityBase<int>
	{
		public string WordName { get; set; }
		public string TurkishTranslator { get; set; }
		public string Description { get; set; }

		[Column(TypeName = "jsonb")]
		public string Tags { get; set; }
		public int AddedCount { get; set; }
		public int TrueCount { get; set; }
		public int FalseCount { get; set; }
		public string ExampleSentence { get; set; }
		public string Type { get; set; }
		public string Pronucation { get; set; }
		[Column(TypeName = "jsonb")]
		public List<string> FamiliarWords { get; set; }
		public string ImageUrl { get; set; } // New property
	}
}