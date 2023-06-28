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
	public class SeriesController : BaseApiController
	{
		private readonly ISeriesService _seriesService;
		public SeriesController(ISeriesService seriesService)
		{
			_seriesService = seriesService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<SeriesDto>), 200)]
		public async Task<IActionResult> Get()
		{
			var series = await _seriesService.GetAsync();

			return Success("series listed.", null, series);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<SeriesDto>), 200)]
		[ProducesResponseType(typeof(ApiResult<SeriesDto>), 400)]
		[ProducesResponseType(typeof(ApiResult<SeriesDto>), 500)]
		[Consumes("application/json")]
		public async Task<IActionResult> Post([FromBody] SeriesDto model)
		{
			try
			{
				if (model == null) return NotFound("Series not found.", null, model);

				var result = await _seriesService.CreateAsync(model);

				if (result == null)
				{
					return NotFound("Series not found.", null, model);
				}

				return Success("Series added successfully.", null, model);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] SeriesDto model)
		{
			try
			{
				var SeriesDto = await _seriesService.UpdateAsync(model);
				bool isNotNull = SeriesDto != null;
				if (isNotNull) return Success("Series updated successfully.", null, SeriesDto);

				return NotFound("Series not found.", null, model);
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
				var isNotNull = await _seriesService.DeleteAsync(id);
				if (isNotNull) return Success<object>("Series deleted successfully.", null, null);

				return NotFound<object>("Series not Found.", null, null);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

	}
}