using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
	/// <summary>
	/// 部位エンティティ
	/// ヒロインの身体の各部位を表現する
	/// </summary>
	public class Part
	{
		public PartID ID { get; }

		public Development Development { get; private set; } // 開発度
		public Affection Affection { get; private set; } // 好感度

		/// <summary>
		/// 部位を作成する
		/// </summary>
		/// <param name="id">部位ID</param>
		/// <param name="development">開発度の初期値</param>
		/// <param name="affection">好感度の初期値</param>
		public Part(PartID id, Development development, Affection affection)
		{
			ID = id;
			Development = development;
			Affection = affection;
		}

		/// <summary>
		/// さわり操作による反応
		/// 開発度と好感度を更新する（快感はBodyで管理）
		/// </summary>
		/// <param name="interaction">さわり操作</param>
		public void Interact(Interaction interaction)
		{
			Development = Development.CalculateGain();
			Affection = Affection.CalculateGain();
		}
	}
}
