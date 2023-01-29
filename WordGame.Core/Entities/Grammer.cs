using System.ComponentModel.DataAnnotations.Schema;
using WordGame.Core.Entities.Base;

namespace WordGame.Core.Entities
{
	public class Grammer: EntityBase<int>
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public string BasicRules { get; set; }
		public string Hints { get; set; }

		[Column(TypeName = "jsonb")]
		public string Tags { get; set; }
		public string ExampleSentence { get; set; }
	}
}