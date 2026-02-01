using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Entity
{
	/// <summary>
	/// 部位エンティティ
	/// ヒロインの身体の各部位を表現する
	/// </summary>
	public class Part
	{
		public PartID ID { get; }

		public Pleasure Pleasure { get; private set; } // 快感
		public Development Development { get; private set; } // 開発度
		public Affection Affection { get; private set; } // 好感度

		/// <summary>
		/// 部位を作成する
		/// </summary>
		/// <param name="id">部位ID</param>
		/// <param name="pleasure">快感の初期値</param>
		/// <param name="development">開発度の初期値</param>
		/// <param name="affection">好感度の初期値</param>
		public Part(PartID id, Pleasure pleasure, Development development, Affection affection)
		{
			ID = id;
			Pleasure = pleasure;
			Development = development;
			Affection = affection;
		}

		/// <summary>
		/// さわり操作による反応
		/// 各パラメータを更新する
		/// </summary>
		/// <param name="interaction">さわり操作</param>
		public void Interact(Interaction interaction)
		{
			Pleasure = Pleasure.CalculateGain();
			Development = Development.CalculateGain();
			Affection = Affection.CalculateGain();
		}
	}
}
