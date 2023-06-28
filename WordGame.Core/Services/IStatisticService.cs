using System.Threading.Tasks;
using WordGame.Core.Dto;

namespace WordGame.Core.Services
{
	public  interface IStatisticService
	{
		Task <StatisticModel> GetStatistic();
	}
}