// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// スプライトの状態遷移パターンを定義する ScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "SpriteStatePattern", menuName = "Scriptable Objects/SpriteStatePattern")]
    public class SpriteStatePattern : ScriptableObject
    {
        public string layerID; // 対象レイヤーのID
        public Sprite initialState; // 初期状態のスプライト
        public List<StateTransition> transitions = new(); // 状態遷移のリスト
        private List<Sprite> _orderStates;
        private List<Sprite> OrderStates => _orderStates ??= BuildOrderStates(); // 状態インデックスの遅延解決用

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Sprite> BuildOrderStates()
        {
            var ordered = new List<Sprite> { initialState };
            var current = initialState;
            while (true)
            {
                var next = transitions.FirstOrDefault(t => t.fromState == current)?.toState;
                if (next == null) break;
                ordered.Add(next);
                current = next;
            }

            return ordered;
        }

        public int IndexOf(Sprite target)
        {
            int index = OrderStates.IndexOf(target);
            if (index == -1)
                throw new InvalidOperationException("存在しない状態のインデックスを検索しようとしました");
            return index;
        }
    }
}
