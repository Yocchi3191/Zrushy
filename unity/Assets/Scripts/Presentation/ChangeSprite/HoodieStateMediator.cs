// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Linq;
using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// 遷移に条件がある複数のISpriteStateNodeをまとめて管理するクラス
    /// 遷移条件の基準になるControllerStateの変更を受けて、dependents(ISpriteStateNode)の状態を強制的に遷移させる
    /// </summary>
    public class HoodieStateMediator : MonoBehaviour, ISpriteStateMediator
    {
        [SerializeField] ConstraintEntry[] _constraints; // controllerの状態ごとの、dependentsの遷移可能な状態
        [SerializeField] ISpriteStateNode _controller;
        [SerializeField] ISpriteStateNode[] _dependents; // controllerの状態に応じて遷移させるSpriteState

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
            _controller = controller;
            _dependents = dependents;
            _constraints = constraints;
        }

        public void OnStateChanged(ISpriteStateNode changed)
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
            if (changed.IsAbove(entry.MaxAllowedState))
                changed.ForceState(entry.MaxAllowedState);
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
                if (!dependent.IsAbove(constraint.MaxAllowedState))
                    continue;

                dependent.ForceState(constraint.MaxAllowedState);
            }
        }
    }
}
