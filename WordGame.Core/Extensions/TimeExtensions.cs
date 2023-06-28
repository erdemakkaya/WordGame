using System;


namespace WordGame.Core.Extensions
{
	public static class TimeExtensions
	{
		public static bool IsBetween(this TimeSpan value,TimeSpan startTime , TimeSpan endTime)
		{
			return (TimeOnly.FromTimeSpan(value).IsBetween(TimeOnly.FromTimeSpan(startTime), TimeOnly.FromTimeSpan(endTime)));
		}
	}
}
