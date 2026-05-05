// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Linq;
using UnityEngine;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation.Unity.ChangeSprite
{
    /// <summary>
    /// スプライトの状態遷移を実行するランタイム
    /// </summary>
    [RequireComponent(typeof(SpriteChanger))]
    public class SpriteState : MonoBehaviour, ISpriteInputHandler, ISpriteStateNode
    {
        [SerializeField] private SpriteStatePattern _statePattern;
        [SerializeField] private DragDirectionThresholdSetting _setting;
        private ISpriteChanger _spriteChanger;

        public event Action<ISpriteStateNode> OnStateChanged; // 状態遷移イベント
        public Sprite CurrentState { get; private set; }

        internal void Construct(SpriteStatePattern pattern, DragDirectionThresholdSetting setting, ISpriteChanger spriteChanger)
        {
            _statePattern = pattern;
            _setting = setting;
            CurrentState = pattern.initialState;
            _spriteChanger = spriteChanger;
            SetState(CurrentState);
        }

        void Awake()
        {
            if (_statePattern == null)
                return;

            _spriteChanger = gameObject.GetComponent<SpriteChanger>();

            CurrentState = _statePattern.initialState;
            SetState(CurrentState);
        }

        /// <summary>
        /// 入力がスプライト遷移条件を満たすか判定し、満たすなら状態遷移させる
        /// </summary>
        public void TryTransition(ZrushyInput input)
        {
            StateTransition matched = _statePattern.Transitions
                .Where(t => t.fromState == CurrentState)
                .FirstOrDefault(t => t.CanTransition(input, _setting));

            if (matched == null)
                return;

            SetState(matched.toState);
        }

        /// <summary>
        /// 外部から初期状態に強制リセットする（コーディネーターによるカスケード用）
        /// </summary>
        public void ForceState(int newStateIndex) => SetState(_statePattern.GetState(newStateIndex));

        private void SetState(Sprite newState)
        {
            CurrentState = newState;
            OnStateChanged?.Invoke(this); // Mediatorに通知 状態が違反していればここで調整される

            SpriteLayerID layerID = new SpriteLayerID(_statePattern.layerID);
            _spriteChanger.ChangeSprite(newState.name);
        }

        public bool IsAbove(int targetStateIndex)
        {
            return _statePattern.IndexOf(CurrentState) > targetStateIndex;
        }
    }
}
