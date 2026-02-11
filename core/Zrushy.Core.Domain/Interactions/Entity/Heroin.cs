using System.Collections.Generic;
using System.Linq;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Exception;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
	/// <summary>
	/// 身体エンティティ（集約ルート）
	/// ヒロインの身体全体を管理し、各部位へのアクセスを制御する
	/// </summary>
	public class Heroin
	{
		private readonly List<IPart> parts;
		private readonly IEventBus eventBus;

		/// <summary>
		/// Body全体の興奮度パラメータ
		/// 各部位の開発度と好感度に応じて増減する
		/// </summary>
		public Arousal Arousal { get; private set; }

		/// <summary>
		/// 絶頂判定の閾値
		/// </summary>
		private const int CLIMAX_THRESHOLD = 100;

		/// <summary>
		/// 身体を作成する
		/// </summary>
		/// <param name="eventBus">イベントバス（絶頂イベント発火用）</param>
		public Heroin(IEventBus eventBus)
		{
			parts = new List<IPart>();
			this.eventBus = eventBus;
			Arousal = new Arousal(0);
		}

		/// <summary>
		/// 部位を追加する
		/// </summary>
		/// <param name="part">追加する部位</param>
		public void AddPart(IPart part)
		{
			parts.Add(part);
		}

		/// <summary>
		/// さわり操作を実行する
		/// 対象部位のパラメータを更新し、快感を蓄積し、絶頂判定を行う
		/// </summary>
		/// <param name="interaction">さわり操作</param>
		public void Interact(Interaction interaction)
		{
			IPart targetPart = GetPart(interaction.PartID);

			// 部位ごとの計算ロジックで興奮度を更新
			Arousal = targetPart.CalculateArousal(Arousal, interaction);

			// 部位のパラメータを更新
			targetPart.Interact(interaction);

			// 絶頂判定とイベント発火
			CheckAndHandleClimax();
		}

		/// <summary>
		/// 絶頂判定を行い、閾値を超えていれば絶頂イベントを発火してクールダウンする
		/// </summary>
		private void CheckAndHandleClimax()
		{
			if (Arousal.IsAboveThreshold(CLIMAX_THRESHOLD))
			{
				// 絶頂イベントを発火
				var climaxEvent = new Event(
					new EventID("climax"),
					new ScenarioID("climax_scenario"),
					priority: 1000 // 高優先度（割り込み）
				);

				eventBus.Publish(climaxEvent);

				// クールダウンを適用
				ApplyCooldown();
			}
		}

		/// <summary>
		/// 絶頂後のクールダウンを適用して快感を減少させる
		/// 全部位の平均開発度を使って減少量を補正する
		/// </summary>
		private void ApplyCooldown()
		{
			if (parts.Count == 0)
			{
				// 部位がない場合は固定値で減少
				Arousal = Arousal.ApplyCooldown(new Development(0));
				return;
			}

			// 全部位の平均開発度を計算
			int totalDevelopment = parts.Sum(p => p.Development.Value);
			int avgDevelopment = totalDevelopment / parts.Count;

			// 平均開発度を使ってクールダウンを適用
			Arousal = Arousal.ApplyCooldown(new Development(avgDevelopment));
		}

		/// <summary>
		/// 指定した部位を取得する
		/// </summary>
		/// <param name="partID">部位ID</param>
		/// <returns>部位</returns>
		/// <exception cref="PartNotFoundException">部位が見つからない場合</exception>
		public IPart GetPart(PartID partID)
		{
			return parts.Find(p => p.ID.Equals(partID))
				?? throw new PartNotFoundException(partID);
		}
	}
}
