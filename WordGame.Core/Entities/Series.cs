using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WordGame.Core.Entities.Base;

namespace WordGame.Core.Entities
{
	public class Series : EntityBase<int>
	{

		public string Name { get; set; }
		public string Description { get; set; }

		[Column(TypeName = "jsonb")]
		public string Tags { get; set; }
		public int Duration { get; set; }

		public int TotalSeason { get; set; }
		public bool IsFinished { get; set; }
		public string Image { get; set; }

		public ICollection<Episode> Episodes { get; set; }
	}
}
