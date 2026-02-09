using System;

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	/// <summary>
	/// 快感パラメータ
	/// 開発度と好感度に比例する
	/// </summary>
	public record Pleasure(int Value) : IComparable<Pleasure>
	{
		public int CompareTo(Pleasure other) => Value.CompareTo(other.Value);

		/// <summary>
		/// さわり反応による快感の増加量を計算して新しいPleasureを返す
		/// TODO: 開発度と好感度を考慮した計算式を実装
		/// </summary>
		internal Pleasure CalculateGain()
		{
			// 仮実装: 固定値を加算
			return new Pleasure(Value + 1);
		}

		/// <summary>
		/// さわり反応による快感の増加量を計算して新しいPleasureを返す
		/// 開発度と好感度に比例して増加量が変化する
		/// </summary>
		/// <param name="development">部位の開発度</param>
		/// <param name="affection">部位の好感度</param>
		internal Pleasure CalculateGain(Development development, Affection affection)
		{
			// 計算式: 基本値1 + (開発度 * 0.1) + (好感度 * 0.05)
			int basePleasure = 1;
			int developmentBonus = (int)(development.Value * 0.1);
			int affectionBonus = (int)(affection.Value * 0.05);
			int totalGain = basePleasure + developmentBonus + affectionBonus;

			return new Pleasure(Math.Max(0, Value + totalGain));
		}

		/// <summary>
		/// 絶頂後のクールダウンを適用して新しいPleasureを返す
		/// 開発度が高いほど減少量が少なくなる
		/// </summary>
		/// <param name="development">部位の開発度（平均値を想定）</param>
		internal Pleasure ApplyCooldown(Development development)
		{
			// 計算式: 基本減少50 - (開発度 / 5)、最低10は減る
			int baseReduction = 50;
			int reduction = baseReduction - (development.Value / 5);
			reduction = Math.Max(reduction, 10); // 最低10は減る

			return new Pleasure(Math.Max(0, Value - reduction));
		}

		/// <summary>
		/// 快感が指定した閾値以上かどうかを判定する
		/// </summary>
		/// <param name="threshold">閾値</param>
		public bool IsAboveThreshold(int threshold)
		{
			return Value >= threshold;
		}
	}
}
