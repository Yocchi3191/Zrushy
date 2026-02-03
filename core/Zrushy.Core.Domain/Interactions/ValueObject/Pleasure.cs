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
	}
}
