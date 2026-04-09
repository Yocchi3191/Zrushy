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
        private Sprite _currentState;
        [SerializeField] private DragDirectionThresholdSetting _setting;

        public event Action<Sprite> OnStateChanged; // 状態遷移イベント
        public Sprite CurrentState => _currentState;

        private void Awake()
        {
            _image = gameObject.GetComponent<Image>();
            if (_image == null)
                throw new System.Exception("Imageコンポーネントがアタッチされていません");
        }

        void Start()
        {
            _currentState = _statePattern.initialState;
        }

        /// <summary>
        /// 入力がスプライト遷移条件を満たすか判定し、満たすなら状態遷移させる
        /// </summary>
        public void TryTransition(ZrushyInput input)
        {
            StateTransition matched = _statePattern.transitions
                .Where(t => t.fromState == _currentState)
                .FirstOrDefault(t => t.CanTransition(input, _setting));

            if (matched == null) return;

            ForceState(matched.toState);
        }

        /// <summary>
        /// 外部から初期状態に強制リセットする（コーディネーターによるカスケード用）
        /// </summary>
        public void ForceState(Sprite newState)
        {
            _currentState = newState;
            _image.sprite = _currentState;
            OnStateChanged?.Invoke(_currentState);
        }

        public bool IsAbove(Sprite maxAllowed)
        {
            throw new NotImplementedException();
        }
    }
}
