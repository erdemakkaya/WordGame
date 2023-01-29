using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WordGame.WEB.Helper;

namespace WordGame.WEB.Controllers.Base
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseApiController : ControllerBase
	{
		[NonAction]
		protected IActionResult Success<T>(string message, string internalMessage, T data)
		{
			return Success(new ApiResult<T>
			{
				Success = true,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult Success<T>(ApiResult<T> data)
		{
			return Ok(data);
		}

		[NonAction]
		protected IActionResult Created<T>(string message, string internalMessage, T data)
		{
			return Created(new ApiResult<T>
			{
				Success = true,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult Created<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status201Created, data);
		}

		[NonAction]
		protected IActionResult NoContent<T>(string message, string internalMessage, T data)
		{
			return NoContent(new ApiResult<T>
			{
				Success = true,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult NoContent<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status204NoContent, data);
		}

		[NonAction]
		protected IActionResult BadRequest<T>(string message, string internalMessage, T data)
		{
			return BadRequest(new ApiResult<T>
			{
				Success = false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult BadRequest<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status400BadRequest, data);
		}

		[NonAction]
		protected IActionResult Unauthorized<T>(string message, string internalMessage, T data)
		{
			return Unauthorized(new ApiResult<T>
			{
				Success = false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult Unauthorized<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status401Unauthorized, data);
		}

		[NonAction]
		protected IActionResult Forbidden<T>(string message, string internalMessage, T data)
		{
			return Forbidden(new ApiResult<T>
			{
				Success=false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult Forbidden<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status403Forbidden, data);
		}

		[NonAction]
		protected IActionResult NotFound<T>(string message, string internalMessage, T data)
		{
			return NotFound(new ApiResult<T>
			{
				Success = false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult NotFound<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status404NotFound, data);
		}

		[NonAction]
		protected IActionResult Error<T>(string message, string internalMessage, T data)
		{
			return Error(new ApiResult<T>
			{
				Success = false,
				Message = message,
				InternalMessage = internalMessage,
				Data = data
			});
		}

		[NonAction]
		protected IActionResult Error<T>(ApiResult<T> data)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, data);
		}
	}
}
