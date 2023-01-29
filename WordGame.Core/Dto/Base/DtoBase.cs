using WordGame.Core.Dto.Base.Interfaces;

namespace WordGame.Core.Dto.Base
{
	public class DtoBase<TId> : IDtoBase<TId>
	{
		public TId Id { get; set; }
	}
}