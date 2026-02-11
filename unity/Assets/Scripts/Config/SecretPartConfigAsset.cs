using UnityEngine;
using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Unity.Config
{
	/// <summary>
	/// 秘部の計算パラメータ設定
	/// </summary>
	[CreateAssetMenu(menuName = "Zrushy/Config/SecretPartConfig", fileName = "SecretPartConfig")]
	public class SecretPartConfigAsset : ScriptableObject
	{
		[Header("濡れ状態")]
		[SerializeField] private int wetThreshold;

		[Header("乾燥時")]
		[SerializeField] private int dryDiscomfort;

		[Header("処女喪失")]
		[SerializeField] private int virginityLossPenalty;

		[Header("濡れ時ゲイン（指）")]
		[SerializeField] private int fingerWetBase;
		[SerializeField] private float fingerWetDevFactor;

		[Header("濡れ時ゲイン（ペニス）")]
		[SerializeField] private int penisWetBase;
		[SerializeField] private float penisWetDevFactor;

		public SecretPartConfig ToConfig() => new(
			wetThreshold,
			dryDiscomfort,
			virginityLossPenalty,
			fingerWetBase,
			fingerWetDevFactor,
			penisWetBase,
			penisWetDevFactor);
	}
}
