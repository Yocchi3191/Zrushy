using System;

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	/// <summary>
	/// 興奮度パラメータ（不快〜快感のスペクトル）
	/// 負の値は不快、正の値は快感を表す
	/// </summary>
	public record Arousal(int Value) : IComparable<Arousal>
	{
		public const int MIN_VALUE = -100;
		public const int MAX_VALUE = 100;

		public int CompareTo(Arousal other) => Value.CompareTo(other.Value);

		/// <summary>
		/// 絶頂後のクールダウンを適用して新しいArousalを返す
		/// 開発度が高いほど減少量が少なくなる
		/// </summary>
		/// <param name="development">部位の開発度（平均値を想定）</param>
		internal Arousal ApplyCooldown(Development development)
		{
			// 計算式: 基本減少50 - (開発度 / 5)、最低10は減る
			int baseReduction = 50;
			int reduction = baseReduction - (development.Value / 5);
			reduction = Math.Max(reduction, 10); // 最低10は減る

			return new Arousal(Math.Clamp(Value - reduction, MIN_VALUE, MAX_VALUE));
		}

		/// <summary>
		/// 興奮度が指定した閾値以上かどうかを判定する
		/// </summary>
		/// <param name="threshold">閾値</param>
		public bool IsAboveThreshold(int threshold)
		{
			return Value >= threshold;
		}

		public static Arousal operator +(Arousal a, int delta)
			=> new Arousal(Math.Clamp(a.Value + delta, MIN_VALUE, MAX_VALUE));

		public static Arousal operator -(Arousal a, int delta)
			=> new Arousal(Math.Clamp(a.Value - delta, MIN_VALUE, MAX_VALUE));
	}
}
