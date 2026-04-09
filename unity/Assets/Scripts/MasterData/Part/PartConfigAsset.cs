using UnityEngine;
using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Unity.Config
{
	/// <summary>
	/// デフォルト部位・胸部位の計算パラメータ設定
	/// ChestPart / Part 両方で使用する PartConfig を生成する
	/// </summary>
	[CreateAssetMenu(menuName = "Zrushy/Config/PartConfig", fileName = "PartConfig")]
	public class PartConfigAsset : ScriptableObject
	{
		[SerializeField] private int baseGain;
		[SerializeField] private float developmentFactor;
		[SerializeField] private float affectionFactor;

		public PartConfig ToConfig() => new(baseGain, developmentFactor, affectionFactor);
	}
}
