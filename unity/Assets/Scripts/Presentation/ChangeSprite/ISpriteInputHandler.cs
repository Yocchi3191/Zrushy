// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Application.UseCase.CanZrushy;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// スプライトの状態遷移が可能なオブジェクトを表すインターフェース
    /// </summary>
    internal interface ISpriteInputHandler
    {
        /// <summary>
        /// 入力がスプライト遷移条件を満たすか判定し、満たすなら状態遷移させる
        /// </summary>
        /// <param name="input">遷移条件の入力情報</param>
        void TryTransition(ZrushyInput input);
    }
}
