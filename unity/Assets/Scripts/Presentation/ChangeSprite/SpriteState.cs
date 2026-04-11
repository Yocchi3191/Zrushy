// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zrushy.Core.Application.UseCase.CanZrushy;

namespace Zrushy.Core.Presentation.Unity.ChangeSprite
{
    /// <summary>
    /// スプライトの状態遷移を実行するランタイム
    /// </summary>
    public class SpriteState : MonoBehaviour, ISpriteInputHandler, ISpriteStateNode
    {
        [SerializeField] private SpriteStatePattern _statePattern;
        Image _image;
        [SerializeField] private DragDirectionThresholdSetting _setting;

        public event Action<ISpriteStateNode> OnStateChanged; // 状態遷移イベント
        public Sprite CurrentState { get; private set; }

        internal void Construct(SpriteStatePattern pattern, DragDirectionThresholdSetting setting)
        {
            _statePattern = pattern;
            _setting = setting;
            CurrentState = pattern.initialState;
        }

        private void Awake()
        {
            _image = gameObject.GetComponent<Image>();
            if (_image == null)
                throw new System.Exception("Imageコンポーネントがアタッチされていません");
        }

        void Start()
        {
            CurrentState = _statePattern.initialState;
        }

        /// <summary>
        /// 入力がスプライト遷移条件を満たすか判定し、満たすなら状態遷移させる
        /// </summary>
        public void TryTransition(ZrushyInput input)
        {
            StateTransition matched = _statePattern.Transitions
                .Where(t => t.fromState == CurrentState)
                .FirstOrDefault(t => t.CanTransition(input, _setting));

            if (matched == null) return;

            ForceState(matched.toState);
        }

        /// <summary>
        /// 外部から初期状態に強制リセットする（コーディネーターによるカスケード用）
        /// </summary>
        public void ForceState(Sprite newState)
        {
            CurrentState = newState;
            _image.sprite = CurrentState;
            OnStateChanged?.Invoke(this);
        }

        public bool IsAbove(Sprite targetState)
        {
            return _statePattern.IndexOf(CurrentState) > _statePattern.IndexOf(targetState);
        }

    }
}
