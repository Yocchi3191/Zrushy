// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.UI;

namespace Zrushy.Presentation.ChangeIllust
{
    [RequireComponent(typeof(Image))]
    public class AlphaHitTestInitializer : MonoBehaviour
    {
        void Start()
        {
            var image = GetComponent<UnityEngine.UI.Image>();
            image.alphaHitTestMinimumThreshold = 0.1f;
        }
    }
}
