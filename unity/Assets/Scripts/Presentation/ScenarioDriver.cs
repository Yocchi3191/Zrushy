using UnityEngine;
using Zenject;
using Zrushy.Core.Presentation;

namespace Zrushy.Unity.Presentation
{
	/// <summary>
	/// シナリオを進めるクラス
	/// </summary>
	public class ScenarioDriver : MonoBehaviour
	{
		[Inject]
		private IScenarioAdvancable scenarioPlayer;

		[Inject]
		private ILogger logger;

		/// <summary>
		/// この優先度以上のイベントは現在のシナリオを割り込む
		/// </summary>
		private const int INTERRUPT_PRIORITY_THRESHOLD = 900;

		private void Start()
		{
			if (scenarioPlayer == null)
			{
				logger.LogError("ScenarioDriver", "ScenarioPlayer is not assigned.");
			}
		}

		/// <summary>
		/// シナリオを進める
		/// unity側での集約ルートとしてデバッグ時などに使えそうなので、薄いけど残しておく
		/// </summary>
		public void Next()
		{
			scenarioPlayer.Next();
		}
	}
}
