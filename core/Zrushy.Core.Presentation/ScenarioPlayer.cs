using System;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Presentation
{
	public class ScenarioPlayer
	{
		private readonly IScenarioRepository engine;
		private readonly HeroinViewModel heroin;

		public bool IsPlaying => isPlaying;
		private bool isPlaying = false;

		/// <summary>
		/// シナリオが開始されたときに発火するイベント
		/// ScenarioDriver などが購読して自動進行を開始する
		/// </summary>
		public event System.Action? OnScenarioStarted;

		/// <summary>
		/// シナリオが終了したときに発火するイベント
		/// ScenarioDriver などが購読して自動進行を停止する
		/// </summary>
		public event System.Action? OnScenarioFinished;

		public ScenarioPlayer(IScenarioRepository engine, HeroinViewModel heroin)
		{
			this.engine = engine;
			this.heroin = heroin;
			engine.OnActionChanged += heroin.Act;
		}

		public void Play(ScenarioID scenarioID)
		{
			if (isPlaying)
			{
				return;
			}

			try
			{
				engine.Start(scenarioID);
				isPlaying = true;

				// シナリオ開始イベントを発火
				OnScenarioStarted?.Invoke();
			}
			catch (ScenarioNotFoundException ex)
			{
				// シナリオが見つからない場合はログを出力して処理を中断
				// isPlaying は false のままにする
				Console.WriteLine($"[ScenarioPlayer] {ex.Message}");
				throw;
			}
		}

		public void Next()
		{
			if (!isPlaying) return;
			engine.Next();
			if (engine.IsScenarioFinished)
			{
				isPlaying = false;
				OnScenarioFinished?.Invoke();
			}
		}

		/// <summary>
		/// 現在再生中のシナリオを強制的に停止する
		/// 高優先度イベント（絶頂など）による割り込み時に使用
		/// </summary>
		public void Stop()
		{
			if (!isPlaying)
				return;

			engine.Stop();
			isPlaying = false;
			// 注: OnScenarioFinished は発火しない（強制停止なので）
		}
	}
}
