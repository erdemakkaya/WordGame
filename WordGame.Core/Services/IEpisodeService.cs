using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface IEpisodeService : IService
	{
		Task<EpisodeDto> CreateAsync(EpisodeDto dtoObject);
		Task<EpisodeDto> CreateOrUpdateAsync(EpisodeDto dtoObject);
		Task<bool> DeleteAsync(int id);
		Task<EpisodeDto> GetAsync(int id);
		Task<IEnumerable<EpisodeDto>> GetAsync();
		Task<IEnumerable<EpisodeDto>> GetBySeriesId(int seriesId);
		Task<EpisodeDto> UpdateAsync(EpisodeDto dtoObject);
	}
}
