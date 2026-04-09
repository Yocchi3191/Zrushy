using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Unity.Config;

namespace Zrushy.Core.DI
{
	/// <summary>
	/// Bodyの初期化を担当するMonoBehaviour
	/// シーン開始時にBodyに初期部位データを設定する
	/// </summary>
	public class BodyInitializer : MonoBehaviour
	{
		[Inject]
		private Heroin body;

		[Header("部位設定")]
		[SerializeField] private PartConfigAsset defaultPartConfig;
		[SerializeField] private PartConfigAsset chestPartConfig;
		[SerializeField] private SecretPartConfigAsset secretPartConfig;
		[SerializeField] private List<string> defaultPartIds;

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
			foreach (var id in defaultPartIds)
			{
				body.AddPart(new Part(
					new PartID(id),
					new Development(0),
					new Affection(0),
					defaultPartConfig.ToConfig()
				));
			}

			Debug.Log($"[BodyInitializer] Body initialized, Arousal={body.Arousal.Value}");
		}
	}
}
