// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zrushy.Core.Presentation.Unity.Title
{

    public class TitelSceneController : MonoBehaviour
    {
        /// <summary>
        /// おさわりシーン開始ボタン押下時の遷移処理
        /// </summary>
        public void OnStartClicked()
        {
            SceneManager.LoadScene("PoC");
        }

        /// <summary>
        /// アプリケーション終了ボタン押下時の処理
        /// </summary>
        public void OnExitClicked()
        {
            UnityEngine.Application.Quit();
        }
    }
}
