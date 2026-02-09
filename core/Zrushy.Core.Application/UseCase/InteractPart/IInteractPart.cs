namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケースインターフェース
	/// </summary>
	public interface IInteractPart
	{
		/// <summary>
		/// 部位をさわる操作を実行する
		/// </summary>
		InteractPartResult Execute(InteractPartCommand command);
	}
}
