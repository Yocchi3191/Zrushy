// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// ControllerState と dependents の遷移可能な状態の組み合わせを定義するクラス
    /// 差し替える gameObject ごとに1つアタッチする
    /// </summary>
    [CreateAssetMenu(fileName = "ConstraintEntry", menuName = "Scriptable Objects/ConstraintEntry")]
    public class ConstraintEntry : ScriptableObject
    {
        public Sprite ControllerState;
        public Sprite MaxAllowedState;
    }
}
