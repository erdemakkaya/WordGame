using WordGame.Core.Dto.Base;


namespace WordGame.Core.Dto
{
	public  class EpisodeDto : DtoBase<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public int Season { get; set; }
		public int Order { get; set; }

		public int Duration { get; set; }

		public int SeriesId { get; set; }

	}
}
