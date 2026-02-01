using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Repository
{
	/// <summary>
	/// リアクションリポジトリのインターフェース
	/// リアクションデータの取得を抽象化する
	/// </summary>
	public interface IReactionRepository
	{
		/// <summary>
		/// 指定した部位とパラメータに基づいてリアクションを取得する
		/// </summary>
		/// <param name="partID">部位ID</param>
		/// <param name="pleasure">快感値</param>
		/// <param name="development">開発度</param>
		/// <param name="affection">好感度</param>
		/// <returns>条件に合致したリアクション（複数候補からランダム選択）</returns>
		Action GetReaction(PartID partID, Pleasure pleasure, Development development, Affection affection);
	}
}
