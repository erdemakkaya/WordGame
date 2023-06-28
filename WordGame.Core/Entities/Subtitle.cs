using System;
using WordGame.Core.Entities.Base;

namespace WordGame.Core.Entities
{
	public class Subtitle : EntityBase<int>
	{
		public string Text { get; set; }
		public string TurkishText { get; set; }
		public int Section { get; set; }

		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }

		public bool IsFavourite { get; set; }

		public int EpisodeId { get; set; }
		public Episode Episode { get; set; }
	}
}
