// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Unity.Config
{
    /// <summary>
    /// 秘部の計算パラメータ設定
    /// </summary>
    [CreateAssetMenu(menuName = "Zrushy/Config/SecretPartConfig", fileName = "SecretPartConfig")]
    public class SecretPartConfigAsset : ScriptableObject
    {
        [Header("濡れ状態")]
        [SerializeField] private int _wetThreshold;

        [Header("乾燥時")]
        [SerializeField] private int _dryDiscomfort;

        [Header("処女喪失")]
        [SerializeField] private int _virginityLossPenalty;

        [Header("濡れ時ゲイン（指）")]
        [SerializeField] private int _fingerWetBase;
        [SerializeField] private float _fingerWetDevFactor;

        [Header("濡れ時ゲイン（ペニス）")]
        [SerializeField] private int _penisWetBase;
        [SerializeField] private float _penisWetDevFactor;

        public SecretPartConfig ToConfig() => new(
            _wetThreshold,
            _dryDiscomfort,
            _virginityLossPenalty,
            _fingerWetBase,
            _fingerWetDevFactor,
            _penisWetBase,
            _penisWetDevFactor);
    }
}
