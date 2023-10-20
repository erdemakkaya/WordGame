using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using WordGame.Application.Extensions;
using WordGame.Core.Helpers;

namespace WordGame.Application.Services
{
	public class SubtitleServiceBase
	{

		public async Task<bool> CreateFromFile(IFormFile formFile, int EpisodeId)
		{
			if (formFile == null || formFile.Length == 0)
				return false;

			var fileContent = formFile.ReadAsList();
			var result = FileHelpers.ParseSRTBySB(fileContent);

			return true;

		}
	}
}