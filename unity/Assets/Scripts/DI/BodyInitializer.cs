using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

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
			// 頭部
			body.AddPart(new Part(
				new PartID("head"),
				new Pleasure(0),
				new Development(0),
				new Affection(0)
			));

			// 胸
			body.AddPart(new Part(
				new PartID("chest"),
				new Pleasure(0),
				new Development(0),
				new Affection(0)
			));

			// お腹
			body.AddPart(new Part(
				new PartID("belly"),
				new Pleasure(0),
				new Development(0),
				new Affection(0)
			));

			Debug.Log($"[BodyInitializer] Body initialized with 3 parts");
		}
	}
}
