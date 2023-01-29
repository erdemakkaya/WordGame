using System.Collections.Generic;
using System.Text;

namespace WordGame.Core.Extensions
{
	public static class StringExtensions
	{
		public static bool HasValue(this string value)
		{
			return !string.IsNullOrEmpty(value);
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}

		public static string MultipleReplace(this string s, Dictionary<string, string> replacements)
		{
			var sb = new StringBuilder(s);
			foreach (var key in replacements.Keys)
			{
				sb.Replace(key, replacements[key]);
			}
			return sb.ToString();
		}
	}
}