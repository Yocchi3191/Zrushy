using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// マウス入力の受け付け
	/// 入力種別を判定して InteractionType に変換し PartController へ送る
	/// </summary>
	public class Clickable : MonoBehaviour,
		IPointerClickHandler,
		IPointerDownHandler,
		IPointerUpHandler,
		IDragHandler,
		IScrollHandler
	{
		private const float LongPressThreshold = 0.5f;
		private const float DragInterval = 0.2f;

		public string PartId => gameObject.name;

		[Inject]
		private PartController controller;

		[Inject]
		private ClickableRegistry registry;

		public event Action<PartInput, Vector2> OnInputSent;

		private void Start()
		{
			registry.Register(this);
		}

		private void OnDestroy()
		{
			registry.Unregister(this);
		}

		private float _pointerDownTime;
		private bool _longPressHandled;
		private bool _isDragging;
		private float _lastDragSendTime;

		public void OnPointerDown(PointerEventData eventData)
		{
			Debug.Log($"[Clickable] {gameObject.name}: PointerDown ({eventData.button})");
			_pointerDownTime = Time.time;
			_longPressHandled = false;
			_isDragging = false;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (_isDragging) return;

			float duration = Time.time - _pointerDownTime;
			if (duration >= LongPressThreshold)
			{
				_longPressHandled = true;
				InteractionType type = eventData.button == PointerEventData.InputButton.Right
					? InteractionType.Oral
					: InteractionType.Press;
				Debug.Log($"[Clickable] {gameObject.name}: 長押し → {type} ({duration:F2}s)");
				SendInput(type);
			}
		}

		/// <summary>
		/// IPointerClickHandler の実装（短押し）
		/// 長押し・ドラッグ後は発火しない
		/// </summary>
		public void OnPointerClick(PointerEventData eventData)
		{
			if (_longPressHandled) return;

			InteractionType type = eventData.button == PointerEventData.InputButton.Right
				? InteractionType.Tongue
				: InteractionType.Finger;
			Debug.Log($"[Clickable] {gameObject.name}: クリック → {type}");
			SendInput(type);
		}

		/// <summary>
		/// IDragHandler の実装
		/// 一定間隔で Stroke を発火する
		/// </summary>
		public void OnDrag(PointerEventData eventData)
		{
			_isDragging = true;
			if (Time.time - _lastDragSendTime < DragInterval) return;
			_lastDragSendTime = Time.time;
			Debug.Log($"[Clickable] {gameObject.name}: ドラッグ → Stroke");
			SendInput(InteractionType.Stroke, eventData.delta.normalized);
		}

		/// <summary>
		/// IScrollHandler の実装
		/// </summary>
		public void OnScroll(PointerEventData eventData)
		{
			Debug.Log($"[Clickable] {gameObject.name}: スクロール → Lick (delta: {eventData.scrollDelta})");
			SendInput(InteractionType.Lick);
		}

		/// <summary>
		/// UIボタンからも呼び出し可能
		/// </summary>
		public void OnClick()
		{
			SendInput(InteractionType.Finger);
		}

		/// <summary>
		/// coreへの入力送信
		/// Clickableを購読するクラスへのイベント発火を行う
		/// </summary>
		/// <param name="type"></param>
		private void SendInput(InteractionType type, Vector2 direction = default)
		{
			SendInputToCore(type);
			SendInputToSubscribers(new PartInput(new PartID(gameObject.name), type), direction);
		}

		/// <summary>
		/// coreへの入力送信
		/// </summary>
		/// <param name="type"></param>
		private void SendInputToCore(InteractionType type)
		{
			controller.SendInput(new PartInput(new PartID(gameObject.name), type));
		}

		/// <summary>
		/// Clickable購読クラスへのイベント発火
		/// </summary>
		/// <param name="input"></param>
		private void SendInputToSubscribers(PartInput input, Vector2 direction)
		{
			OnInputSent?.Invoke(input, direction);
			Debug.Log($"[Clickable] {gameObject.name}: OnInputSent event invoked with {input}");
		}
	}
}
