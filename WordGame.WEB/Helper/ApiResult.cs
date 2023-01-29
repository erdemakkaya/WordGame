using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordGame.WEB.Helper
{
	public class ApiResult
	{
		public string Message { get; set; }
		public bool Success { get; set; }
		public string InternalMessage { get; set; }
	}

	public class ApiResult<T>
	{
		public string Message { get; set; }
		public bool Success { get; set; }
		public string InternalMessage { get; set; }
		public T Data { get; set; }
	}
}
