using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.DI
{
	/// <summary>
	/// Bodyの初期化を担当するMonoBehaviour
	/// シーン開始時にBodyに初期部位データを設定する
	/// </summary>
	public class BodyInitializer : MonoBehaviour
	{
		[Inject]
		private Body body;

		private void Start()
		{
			InitializeBody();
		}

		/// <summary>
		/// Bodyに初期部位データを設定する
		/// TODO: 実際のゲームデータから部位を読み込む実装に置き換える
		/// </summary>
		private void InitializeBody()
		{
			string[] partIds = { "head", "torso", "arm", "hand", "waist", "leg", "foot" };

			foreach (var id in partIds)
			{
				body.AddPart(new Part(
					new PartID(id),
					new Development(0),
					new Affection(0)
				));
			}

			Debug.Log($"[BodyInitializer] Body initialized with {partIds.Length} parts, Pleasure={body.Pleasure.Value}");
		}
	}
}
