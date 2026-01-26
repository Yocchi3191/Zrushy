using UnityEngine;
using Zenject;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Presentation;
using Event = Zrushy.Core.Domain.Entity.Event;

namespace Zrushy.Presentation.Unity
{
	/// <summary>
	/// 部位のビュー（Unity MonoBehaviour）
	/// リアクションとイベントを表示・再生する
	/// </summary>
	public class PartView : MonoBehaviour
	{
		// TODO: インスペクタで設定する
		// [SerializeField] private TextMeshProUGUI dialogueText;
		// [SerializeField] private Animator characterAnimator;
		// [SerializeField] private AudioSource audioSource;

		[Inject]
		private PartViewModel viewModel;

		private void Start()
		{
			// ViewModelのイベントを購読
			viewModel.OnUpdated += OnViewModelUpdated;
		}

		private void OnDestroy()
		{
			// メモリリーク防止のためイベントハンドラを解除
			if (viewModel != null)
			{
				viewModel.OnUpdated -= OnViewModelUpdated;
			}
		}

		/// <summary>
		/// ViewModelが更新されたときに呼ばれる
		/// リアクションとイベントを再生する
		/// </summary>
		/// <param name="result">さわり反応の結果</param>
		private void OnViewModelUpdated(InteractPartResult result)
		{
			// リアクションを再生
			if (result.Reaction != null)
			{
				PlayReaction(result.Reaction);
			}

			// イベントを再生
			if (result.Event != null)
			{
				PlayEvent(result.Event);
			}
		}

		/// <summary>
		/// リアクションを再生する
		/// </summary>
		/// <param name="reaction">再生するリアクション</param>
		private void PlayReaction(Reaction reaction)
		{
			// TODO: 実際のUI要素が実装されたら以下のコードを有効化
			// dialogueText.text = reaction.Dialogue;
			// characterAnimator.Play(reaction.AnimationName);
			// audioSource.PlayOneShot(Resources.Load<AudioClip>(reaction.VoiceClipName));

			// 現在はデバッグログで代用
			Debug.Log($"[PartView] Reaction - Dialogue: {reaction.Dialogue}, " +
					  $"Animation: {reaction.AnimationName}, " +
					  $"Expression: {reaction.ExpressionName}, " +
					  $"Voice: {reaction.VoiceClipName}");
		}

		/// <summary>
		/// イベントを再生する
		/// </summary>
		/// <param name="evt">再生するイベント</param>
		private void PlayEvent(Event evt)
		{
			// TODO: イベントタイプに応じた演出を実装
			// switch (evt.Type)
			// {
			//     case EventType.FirstTouch:
			//         // 初回さわり演出
			//         break;
			//     case EventType.Climax:
			//         // 絶頂演出
			//         break;
			//     // ...
			// }

			// 現在はデバッグログで代用
			Debug.Log($"[PartView] Event - ID: {evt.EventID}, " +
					  $"Name: {evt.Name}, " +
					  $"Type: {evt.Type}, " +
					  $"Cutscene: {evt.CutsceneName}");
		}
	}
}
