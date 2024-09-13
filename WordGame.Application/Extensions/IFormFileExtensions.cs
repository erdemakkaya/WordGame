using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Application.Extensions
{
	public static class IFormFileExtensions
	{
		public static StringBuilder ReadAsList(this IFormFile file)
		{
			var result = new StringBuilder();
			using (var reader = new StreamReader(file.OpenReadStream()))
			{
				while (reader.Peek() >= 0)
					result.AppendLine(reader.ReadLine());
			}
			return result;
		}

		public static async Task<StringBuilder> ReadAsListAsync(this IFormFile file)
		{
			var result = new StringBuilder();
			using (var reader = new StreamReader(file.OpenReadStream()))
			{
				while (reader.Peek() >= 0)
					result.AppendLine(await reader.ReadLineAsync());
			}
			return result;
		}


#if NET8_0


		/// <summary>
		/// public Task<List<string>> Index(
		///IFormFile file, [FromServices] ObjectPool<StringBuilder> pool) =>
		///	file.ReadAsListAsync(pool);
		/// </summary>
		/// <param name="file"></param>
		/// <param name="pool"></param>
		/// <returns></returns>
	//	public static async Task<string> ReadAsStringAsync(
	//this IFormFile file, Object<StringBuilder> pool)
	//	{
	//		var builder = pool.Get();
	//		try
	//		{
	//			using var reader = new StreamReader(file.OpenReadStream());
	//			while (reader.Peek() >= 0)
	//			{
	//				builder.AppendLine(await reader.ReadLineAsync());
	//			}
	//			return builder.ToString();
	//		}
	//		finally
	//		{
	//			pool.Return(builder);
	//		}
	//	}
#endif

	}
}
