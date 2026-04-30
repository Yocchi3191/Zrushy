// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] private string _sceneNameToChange;

        public void ChangeScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneNameToChange);
        }
    }
}
