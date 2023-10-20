using Microsoft.AspNetCore.Http;

namespace WordGame.WEB.Models
{
	public class FileModel
	{
		public int episodeId { get; set; }	
		public string FileName { get; set; }
		public IFormFile FormFile { get; set; }
	}
}
