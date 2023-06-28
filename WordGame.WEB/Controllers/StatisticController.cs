using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services;
using WordGame.WEB.Controllers.Base;

namespace WordGame.WEB.Controllers
{
	public class StatisticController : BaseApiController
	{
		private readonly IStatisticService _statisticService;

		public StatisticController(IStatisticService statisticService)
		{
			_statisticService = statisticService;
		}

		// GET: api/<GrammerController>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<StatisticModel>), 200)]
		public async Task<IActionResult> Get()
		{
			var statistic = await _statisticService.GetStatistic();

			return Success("Statistic listed.", null, statistic);
		}

	}
}
