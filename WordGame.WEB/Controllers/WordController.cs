using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WordGame.Core.Services;
using WordGame.WEB.Controllers.Base;
using WordGame.WEB.Helper;
using WordGame.WEB.Models.ResponseModels.WordResponseModel;

namespace WordGame.WEB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WordController : BaseApiController
	{
		private readonly IWordService _wordService;

		public WordController(IWordService wordService)
		{
			_wordService = wordService;
		}

		[HttpGet]
		[ProducesResponseType(typeof(WordListResponse), 200)]
		public async Task<IActionResult> Get()
		{
			var words = await _wordService.GetAsync();
			var response = new WordListResponse { Words = words };

			return Success("Words listed.", null, response);
		}

		[HttpGet("statistic")]
		[ProducesResponseType(typeof(WordListResponse), 200)]
		public async Task<IActionResult> GetByStatistic()
		{
			var words = await _wordService.GetWordsByStatisticAsync();
			var response = new WordListResponse { Words = words };

			return Success("Words listed.", null, response);
		}

		[HttpGet("{name}")]
		public async Task<IActionResult> Get(string name)
		{
			int id;
			WordResponse wordResponse = new WordResponse();

			bool isNumericId = int.TryParse(name, out id);

			wordResponse.Word = isNumericId ? await _wordService.GetAsync(id) : await _wordService.GetWordByNameAsync(name);

			if (wordResponse.Word == null) return NotFound<object>("Word not found.", "Word not found in database.", null);

			return Success("Word found.", null, wordResponse);
		}

		[HttpPost]
		[ProducesResponseType(typeof(ApiResult<WordResponse>), 200)]
		[ProducesResponseType(typeof(ApiResult<WordResponse>), 400)]
		[ProducesResponseType(typeof(ApiResult<WordResponse>), 500)]
		[Consumes("application/json")]
		public async Task<IActionResult> Post([FromBody] WordResponse model)
		{
			try
			{
				if (model == null) return NotFound("Word not found.", null, model);

				var result = await _wordService.CreateOrIncreaseAsync(model.Word);

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
		public async Task<IActionResult> Put(int id, [FromBody] WordResponse model)
		{
			try
			{
				var wordModel = await _wordService.UpdateAsync(model.Word);
				bool isNotNull = wordModel != null;
				if (isNotNull) return Success("Word updated successfully.", null, wordModel);

				return NotFound("Word not found.", null, model);
			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpPut("true/{id}")]
		public async Task<IActionResult> TrueCount(int id)
		{
			try
			{
				var isNotNull = await _wordService.IncreaseTrueOrFalseCount(id, true);
				if (isNotNull) return Success<object>("True  count increased successfully.", null, null);

				return NotFound<object>("Word not Found.", null, null);

			}
			catch (Exception ex)
			{
				return Error<object>("Something went wrong!", ex.Message, null);
			}
		}

		[HttpPut("false/{id}")]
		public async Task<IActionResult> FalseCount(int id)
		{
			try
			{
				var isNotNull = await _wordService.IncreaseTrueOrFalseCount(id, false);
				if (isNotNull) return Success<object>("Word false count increased successfully.", null, null);

				return NotFound<object>("Word not Found.", null, null);

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
				var isNotNull = await _wordService.DeleteAsync(id);
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
