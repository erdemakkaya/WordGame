namespace WordGame.Core.Dto.Base.Interfaces
{
	public interface IDtoBase<TId> : IDto
	{
		TId Id { get; }
	}
}
