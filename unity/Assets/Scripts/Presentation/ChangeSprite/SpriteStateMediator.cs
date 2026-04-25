// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Linq;
using UnityEngine;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// 遷移に条件がある複数のISpriteStateNodeをまとめて管理するクラス
    /// 遷移条件の基準になるControllerStateの変更を受けて、dependents(ISpriteStateNode)の状態を強制的に遷移させる
    /// </summary>
    public class SpriteStateMediator : MonoBehaviour
    {
        [SerializeField] private ConstraintEntry[] _constraints; // controllerの状態ごとの、dependentsの遷移可能な状態
        private ISpriteStateNode _controller;
        private ISpriteStateNode[] _dependents; // controllerの状態に応じて遷移させるSpriteState
        [SerializeField] private SpriteState controller;
        [SerializeField] private SpriteState[] dependents;

        /// <summary>
        /// 疑似コンストラクタ
        /// ユニットテストとかでセットアップが必要なとき用
        /// 通常はInspectorで設定する
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="dependents"></param>
        /// <param name="constraints"></param>
        internal void Construct(ISpriteStateNode controller, ISpriteStateNode[] dependents, ConstraintEntry[] constraints)
        {
            if (_controller != null)
                throw new InvalidOperationException("Constructは2回以上呼び出せません");

            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _dependents = dependents ?? throw new ArgumentNullException(nameof(dependents));
            _constraints = constraints ?? throw new ArgumentNullException(nameof(constraints));

            _controller.OnStateChanged += OnStateChanged;
            foreach (ISpriteStateNode dependent in _dependents)
            {
                dependent.OnStateChanged += OnStateChanged;
            }
        }

        private void Awake()
        {
            if (controller == null)
            {
                Debug.LogWarning("controllerが設定されていません", this);
                return;
            }

            Construct(controller, dependents, _constraints);
        }

        private void OnStateChanged(ISpriteStateNode changed)
        {
            if (changed == _controller)
            {
                ControllerOperation(changed);
                return;
            }

            if (_dependents.Contains(changed))
            {
                DependentOperation(changed);
                return;
            }

            throw new Exception("このMediatorに登録されていないISpriteStateNodeが通知してきました");
        }

        private void DependentOperation(ISpriteStateNode changed)
        {
            // 遷移後がcontrollerの制約条件に違反していた場合は、許可されている最大の状態に遷移させる
            ConstraintEntry entry = _constraints.FirstOrDefault(c => c.ControllerState == _controller.CurrentState);

            if (entry == null)
                throw new Exception($"対応する制約がconstraintsから見つかりませんでした。制約条件の登録漏れの可能性があります。ControllerState: {_controller.CurrentState}");

            if (changed.IsAbove(entry.MaxAllowedStateIndex))
                changed.ForceState(entry.MaxAllowedStateIndex);
        }

        private void ControllerOperation(ISpriteStateNode changed)
        {
            // 1. controllerの状態に応じた制約をconstraintsから探す
            ConstraintEntry constraint = _constraints.FirstOrDefault(c => c.ControllerState == changed.CurrentState);

            if (constraint == null)
                throw new Exception($"対応する制約がconstraintsから見つかりませんでした。制約条件の登録漏れの可能性があります。ControllerState: {changed.CurrentState}");

            // 2. 制約に従って、dependentsのうち違反しているものを遷移させる
            foreach (ISpriteStateNode dependent in _dependents)
            {
                if (!dependent.IsAbove(constraint.MaxAllowedStateIndex))
                    continue;

                dependent.ForceState(constraint.MaxAllowedStateIndex);
            }
        }
    }
}
