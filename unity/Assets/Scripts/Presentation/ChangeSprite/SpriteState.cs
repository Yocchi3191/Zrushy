// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Linq;
using UnityEngine;
using Zenject;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation.Unity.ChangeSprite
{
    /// <summary>
    /// スプライトの状態遷移を実行するランタイム
    /// </summary>
    [RequireComponent(typeof(SpriteBindingMarker))]
    public class SpriteState : MonoBehaviour, ISpriteInputHandler, ISpriteStateNode
    {
        [SerializeField] private SpriteStatePattern _statePattern;
        [SerializeField] private DragDirectionThresholdSetting _setting;
        [Inject] private ISpriteLayerController _controller;
        private SpriteLayerID _layerID;

        public event Action<ISpriteStateNode> OnStateChanged; // 状態遷移イベント
        public Sprite CurrentState { get; private set; }

        internal void Construct(SpriteStatePattern pattern, DragDirectionThresholdSetting setting, ISpriteLayerController controller, SpriteLayerID layerID)
        {
            _statePattern = pattern;
            _setting = setting;
            _controller = controller;
            _layerID = layerID;
            CurrentState = pattern.initialState;
            SetState(CurrentState);
        }

        void Awake()
        {
            if (_statePattern == null)
                return;

            _layerID = gameObject.GetComponent<SpriteBindingMarker>().LayerID;

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
            OnStateChanged?.Invoke(this);
            _controller.ChangeSprite(_layerID, new LayerState(newState.name));
        }

        public bool IsAbove(int targetStateIndex)
        {
            return _statePattern.IndexOf(CurrentState) > targetStateIndex;
        }
    }
}
