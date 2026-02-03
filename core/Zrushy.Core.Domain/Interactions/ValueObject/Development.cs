using System;

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	/// <summary>
	/// 開発度パラメータ
	/// おさわりや特定のイベントで増える
	/// 部位ごとに蓄積され、溜まる量はさわり方によって異なる
	/// </summary>
	public record Development(int Value) : IComparable<Development>
	{
		public int CompareTo(Development other) => Value.CompareTo(other.Value);

		/// <summary>
		/// さわり反応による開発度の増加量を計算して新しいDevelopmentを返す
		/// TODO: さわり方、条件付けを考慮した計算式を実装
		/// </summary>
		internal Development CalculateGain()
		{
			// 仮実装: 固定値を加算
			return new Development(Value + 1);
		}
	}
}
