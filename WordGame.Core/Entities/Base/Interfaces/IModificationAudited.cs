using System;

namespace WordGame.Core.Entities.Base.Interfaces
{
	public interface IModificationAudited
	{
		DateTime? LastModificationTime { get; set; }
	}
}