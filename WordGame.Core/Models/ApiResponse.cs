using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Core.Models
{
	public class ApiResponse<T>
	{
		public T Data { get; set; }
		public string Message { get; set; }
		public bool IsSuccess { get; set; }
		public string Error { get; set; }

		public ApiResponse(T data, string message = null)
		{
			Data = data;
			Message = message ?? (data != null ? "Operation completed successfully" : "Operation failed");
			IsSuccess = data != null;
		}

		public ApiResponse(string error)
		{
			Error = error;
			Message = "An error occurred";
			IsSuccess = false;
		}
	}
}