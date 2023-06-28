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
	public class SubtitleController : BaseApiController
	{
		private readonly ISubtitleService _subtitleService;
		public SubtitleController(ISubtitleService subtitleService)
		{
			_subtitleService = subtitleService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<SubtitleDto>), 200)]
		public async Task<IActionResult> Get()
		{
			var Subtitle = await _subtitleService.GetAsync();

			return Success("Subtitle listed.", null, Subtitle);
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<SubtitleDto>), 200)]
		public async Task<IActionResult> Get(int seriesId)
		{
			var Subtitle = await _subtitleService.GetBySeriesId(seriesId);

			return Success("Subtitle listed.", null, Subtitle);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<SubtitleDto>), 200)]
		[ProducesResponseType(typeof(ApiResult<SubtitleDto>), 400)]
		[ProducesResponseType(typeof(ApiResult<SubtitleDto>), 500)]
		[Consumes("application/json")]
		public async Task<IActionResult> Post([FromBody] SubtitleDto model)
		{
			try
			{
				if (model == null) return NotFound("Subtitle not found.", null, model);

				var result = await _subtitleService.CreateAsync(model);

				if (result == null)
				{
					return NotFound("Subtitle not found.", null, model);
				}

				return Success("Subtitle added successfully.", null, model);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] SubtitleDto model)
		{
			try
			{
				var SubtitleDto = await _subtitleService.UpdateAsync(model);
				bool isNotNull = SubtitleDto != null;
				if (isNotNull) return Success("Subtitle updated successfully.", null, SubtitleDto);

				return NotFound("Subtitle not found.", null, model);
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
				var isNotNull = await _subtitleService.DeleteAsync(id);
				if (isNotNull) return Success<object>("Subtitle deleted successfully.", null, null);

				return NotFound<object>("Subtitle not Found.", null, null);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

	}
}