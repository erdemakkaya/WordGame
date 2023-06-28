using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services;
using WordGame.WEB.Controllers.Base;
using WordGame.WEB.Helper;

namespace WordGame.WEB.Controllers
{
	public class EpisodeController : BaseApiController
	{
		private readonly IEpisodeService _EpisodeService;
		public EpisodeController(IEpisodeService EpisodeService)
		{
			_EpisodeService = EpisodeService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<EpisodeDto>), 200)]
		public async Task<IActionResult> Get()
		{
			var Episode = await _EpisodeService.GetAsync();

			return Success("Episode listed.", null, Episode);
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<EpisodeDto>), 200)]
		public async Task<IActionResult> Get(int seriesId)
		{
			var Episode = await _EpisodeService.GetBySeriesId(seriesId);

			return Success("Episode listed.", null, Episode);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<EpisodeDto>), 200)]
		[ProducesResponseType(typeof(ApiResult<EpisodeDto>), 400)]
		[ProducesResponseType(typeof(ApiResult<EpisodeDto>), 500)]
		[Consumes("application/json")]
		public async Task<IActionResult> Post([FromBody] EpisodeDto model)
		{
			try
			{
				if (model == null) return NotFound("Episode not found.", null, model);

				var result = await _EpisodeService.CreateAsync(model);

				if (result == null)
				{
					return NotFound("Episode not found.", null, model);
				}

				return Success("Episode added successfully.", null, model);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] EpisodeDto model)
		{
			try
			{
				var EpisodeDto = await _EpisodeService.UpdateAsync(model);
				bool isNotNull = EpisodeDto != null;
				if (isNotNull) return Success("Episode updated successfully.", null, EpisodeDto);

				return NotFound("Episode not found.", null, model);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var isNotNull = await _EpisodeService.DeleteAsync(id);
				if (isNotNull) return Success<object>("Episode deleted successfully.", null, null);

				return NotFound<object>("Episode not Found.", null, null);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

	}
}