using System;
using WordGame.Core.Dto.Base;

namespace WordGame.Core.Dto
{
	public  class SubtitleDto: DtoBase<int>
	{
		public string Text { get; set; }
		public string TurkishText { get; set; }
		public int Section { get; set; }

		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }

		public bool IsFavourite { get; set; }

		public int EpisodeId { get; set; }
	}
}
