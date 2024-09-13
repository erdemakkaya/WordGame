using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Services;
using WordGame.WEB.Controllers.Base;
using WordGame.WEB.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WordGame.WEB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GrammerController : BaseApiController
	{
		private readonly IGrammerService _grammerService;

		public GrammerController(IGrammerService grammerService)
		{
			_grammerService = grammerService;
		}

		// GET: api/<GrammerController>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<GrammerDto>), 200)]
		public async Task<IActionResult> Get()
		{
			var grammers = await _grammerService.GetAsync();

			return Success("Grammers listed.", null, grammers);
		}

		// GET api/<GrammerController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(GrammerDto), 200)]
		public async Task<IActionResult> GetAsync(int id)
		{
			var grammer = await _grammerService.GetAsync(id);

			if (grammer == null) return NotFound<object>("Word not Found.", null, id);

			return Success("Grammers listed.", null, grammer);
		}

		// POST api/<GrammerController>
		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<GrammerDto>), 200)]
		[ProducesResponseType(typeof(ApiResult<GrammerDto>), 400)]
		[ProducesResponseType(typeof(ApiResult<GrammerDto>), 500)]
		[Consumes("application/json")]
		public async Task<IActionResult> PostAsync([FromBody] GrammerDto model)
		{
			try
			{
				var result = await _grammerService.CreateOrUpdateAsync(model);

				return Success("Grammer added successfully.", null, result);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		// PUT api/<GrammerController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] GrammerDto model)
		{
			try
			{
				var grammer = await _grammerService.UpdateAsync(model);
				bool isNotNull = grammer != null;
				if (isNotNull) return Success("Grammer updated successfully.", null, grammer);

				return NotFound("Grammer not found.", null, grammer);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		// DELETE api/<GrammerController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var isNotNull = await _grammerService.DeleteAsync(id);
				if (isNotNull) return Success<object>("Grammer deleted successfully.", null, null);

				return NotFound<object>("Grammer not Found.", null, null);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}
	}
}
