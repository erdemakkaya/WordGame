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
	public class SelectController : BaseApiController
	{
		private readonly IOptionsService _optionService;
		public SelectController(IOptionsService optionService)
		{
			_optionService = optionService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<SelectModel>), 200)]
		public async Task<IActionResult> Get()
		{
			var words = await _optionService.GetAsync();

			return Success("Words listed.", null, words);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<SelectModel>), 200)]
		[ProducesResponseType(typeof(ApiResult<SelectModel>), 400)]
		[ProducesResponseType(typeof(ApiResult<SelectModel>), 500)]
		[Consumes("application/json")]
		public async Task<IActionResult> Post([FromBody] SelectModel model)
		{
			try
			{
				if (model == null) return NotFound("Word not found.", null, model);

				var result = await _optionService.CreateAsync(model);

				if (result == null)
				{
					return NotFound("Word not found.", null, model);
				}

				return Success("Word added successfully.", null, model);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] SelectModel model)
		{
			try
			{
				var SelectModel = await _optionService.UpdateAsync(model);
				bool isNotNull = SelectModel != null;
				if (isNotNull) return Success("Word updated successfully.", null, SelectModel);

				return NotFound("Word not found.", null, model);
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
				var isNotNull = await _optionService.DeleteAsync(id);
				if (isNotNull) return Success<object>("Word deleted successfully.", null, null);

				return NotFound<object>("Word not Found.", null, null);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

	}
}