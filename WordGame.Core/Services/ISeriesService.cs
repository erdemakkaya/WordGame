using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface ISeriesService: IService
	{
		Task<SeriesDto> GetAsync(int id);
		Task<IEnumerable<SeriesDto>> GetAsync();
		Task<SeriesDto> CreateAsync(SeriesDto dtoObject);
		Task<SeriesDto> CreateOrUpdateAsync(SeriesDto dtoObject);
		Task<SeriesDto> UpdateAsync(SeriesDto dtoObject);
		Task<bool> DeleteAsync(int id);
	}
}