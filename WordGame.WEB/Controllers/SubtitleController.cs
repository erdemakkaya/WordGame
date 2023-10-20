using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services;
using WordGame.WEB.Controllers.Base;
using WordGame.WEB.Helper;
using WordGame.WEB.Models;

namespace WordGame.WEB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
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

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(IEnumerable<SubtitleDto>), 200)]
		public async Task<IActionResult> Get(int id)
		{
			var Subtitle = await _subtitleService.GetByEpisodeId(id);

			return Success("Subtitle listed.", null, Subtitle);
		}

		[HttpGet("sub/{id}")]
		[ProducesResponseType(typeof(IEnumerable<SubtitleDto>), 200)]
		public async Task<IActionResult> GetBySubtitleId(int id)
		{
			var Subtitle = await _subtitleService.GetAsync(id);

			return Success("Subtitle listed.", null, Subtitle);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<SubtitleDto>), 200)]
		[ProducesResponseType(typeof(ApiResult<SubtitleDto>), 400)]
		[ProducesResponseType(typeof(ApiResult<SubtitleDto>), 500)]
		//[Consumes("application/json")]
		public IActionResult Post([FromForm] FileModel formData)
		{
			var result = _subtitleService.CreateFromFileAsync(formData.FormFile, formData.episodeId);

			return null;
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