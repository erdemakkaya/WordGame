using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface ISubtitleService : IService
	{
		Task<SubtitleDto> CreateAsync(SubtitleDto dtoObject);
		Task<SubtitleDto> CreateOrUpdateAsync(SubtitleDto dtoObject);
		Task<bool> DeleteAsync(int id);
		Task<SubtitleDto> GetAsync();
		Task<SubtitleDto> GetAsync(int id);
		Task<IEnumerable<SubtitleDto>> GetByEpisodeId(int episodeId);
		Task<SubtitleDto> UpdateAsync(SubtitleDto dtoObject);
	}
}
