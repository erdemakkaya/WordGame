using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Enumerations;
using WordGame.Core.Services.Base;

namespace WordGame.Core.Services
{
	public interface IOptionsService:IService
	{
		Task<SelectModel> GetAsync(int id);
		Task<IEnumerable<SelectModel>> GetAsync();
		Task<SelectModel> CreateAsync(SelectModel dtoObject);
		Task<SelectModel> CreateOrUpdateAsync(SelectModel dtoObject);
		Task<SelectModel> UpdateAsync(SelectModel dtoObject);
		Task<bool> DeleteAsync(int id);
	}
}
