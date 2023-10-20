using System.Collections.Generic;
using WordGame.Core.Dto.Base;

namespace WordGame.Core.Dto
{
	public class SeriesDto : DtoBase<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public List<string> Tags { get; set; }

		public int Duration { get; set; }

		public int TotalSeason { get; set; }
		public bool IsFinished { get; set; }
		public string Image { get; set; }

		public ICollection<EpisodeDto> Episodes { get; set; }
	}
}
