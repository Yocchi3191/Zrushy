namespace Zrushy.Core.Domain.ValueObject
{
	/// <summary>
	/// 好感度パラメータ
	/// 生活パートでのやりとりや覚醒状態でのおさわりで増える
	/// 非覚醒状態のおさわりでは増えない
	/// </summary>
	public class Affection
	{
		public int Value { get; }

		public Affection(int value)
		{
			Value = value;
		}

		/// <summary>
		/// さわり反応による好感度の増加量を計算して新しいAffectionを返す
		/// TODO: 覚醒状態、さわり方を考慮した計算式を実装
		/// </summary>
		internal Affection CalcRateGain()
		{
			// 仮実装: 固定値を加算
			return new Affection(Value + 1);
		}
	}
}
