// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
    /// <summary>
    /// さわり操作の種類
    /// </summary>
    public enum InteractionType
    {
        Finger, // 指（通常おさわり）
        Penis,  // おちんちん
        Stroke, // 擦る・撫でる（ドラッグ）
        Press,  // 押し込む・つねる（長押し）
        Tongue, // 舐める・キス（右クリック短）
        Oral,   // しゃぶる・ディープキス（右クリック長）
        Lick,   // 素早く舐め上げる（スクロール）
    }
}
