// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;

namespace Zrushy.Core.Presentation.Unity.ChangeSprite
{
    [CreateAssetMenu(fileName = "DragDirectionThresholdSetting", menuName = "Scriptable Objects/DragDirectionThresholdSetting")]
    public class DragDirectionThresholdSetting : ScriptableObject
    {
        public float DragDirectionThreshold = 0.7f; // ドラッグ入力の方向がこの値以上であれば、指定された方向とみなす
    }
}
