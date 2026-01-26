using System;
using Zrushy.Core.Application.UseCase.InteractPart;

namespace Zrushy.Core.Presentation
{
	/// <summary>
	/// 部位のビューモデル
	/// さわり反応の結果（リアクションとイベント）を保持し、Viewに通知する
	/// </summary>
	public class PartViewModel
	{
		/// <summary>
		/// 現在のさわり反応の結果
		/// </summary>
		public InteractPartResult? CurrentResult { get; private set; }

		/// <summary>
		/// さわり反応の結果が更新されたときに発火するイベント
		/// Viewはこのイベントを購読して、リアクションとイベントを再生する
		/// </summary>
		public event Action<InteractPartResult>? OnUpdated;

		/// <summary>
		/// さわり反応の結果を更新してViewに通知する
		/// </summary>
		/// <param name="result">新しいさわり反応の結果</param>
		internal void Update(InteractPartResult result)
		{
			CurrentResult = result;
			OnUpdated?.Invoke(result);
		}
	}
}
