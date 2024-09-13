using System.Collections.Generic;
using WordGame.Core.Dto;

namespace WordGame.WEB.Models.ResponseModels.WordResponseModel
{
	public class WordListResponse
	{
		public IEnumerable<WordDto> Words { get; set; }
		// Add additional fields here
	}
}
