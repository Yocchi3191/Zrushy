// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
    public interface ISpriteStateNode
    {
        /// <summary>
        /// 現在の状態
        /// </summary>
        Sprite CurrentState { get; }

        /// <summary>
        /// 状態を強制変更する
        /// </summary>
        /// <param name="newState"></param>
        void ForceState(Sprite newState);

        /// <summary>
        /// 現在の状態が、引数の状態よりも上位の状態かどうか
        /// </summary>
        /// <param name="maxAllowed"></param>
        /// <returns></returns>
        bool IsAbove(Sprite maxAllowed);

        /// <summary>
        /// 状態遷移した際に通知する
        /// </summary>
        event Action<ISpriteStateNode> OnStateChanged;
    }
}
