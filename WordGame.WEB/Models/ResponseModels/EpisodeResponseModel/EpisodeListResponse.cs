using System.Collections.Generic;
using WordGame.Core.Dto;

namespace WordGame.WEB.Models.ResponseModels.EpisodeResponseModel
{
	public class EpisodeListResponse
	{
		public IEnumerable<EpisodeDto> Episodes { get; set; }
    
	}
}