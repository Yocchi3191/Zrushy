// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// Clickableからの入力をどのSpriteStateに通知するかを決定するインターフェース
    /// </summary>
    public interface ISpriteStateRouter
    {
        void Handle(PartInput input);
        ISpriteStateNode Controller { get; }
        ISpriteStateNode[] Dependents { get; }
    }
}
