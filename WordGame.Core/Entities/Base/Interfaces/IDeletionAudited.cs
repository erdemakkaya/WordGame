using System;

namespace WordGame.Core.Entities.Base.Interfaces
{
	public interface IDeletionAudited
	{
		bool IsDeleted { get; set; }
		DateTime? DeletionTime { get; set; }
	}
}