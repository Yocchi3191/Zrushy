// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Presentation.Unity;
using Zrushy.Core.Presentation.Unity.ChangeSprite;
using SpriteState = Zrushy.Core.Presentation.Unity.ChangeSprite.SpriteState;

namespace Zrushy.Core.Test.Unity.EditMode
{
    /// テストリスト
    ///
    /// x Construct_初期状態はinitialSprite
    ///
    /// x TryTransition_無効な入力の場合何もしない
    /// x TryTransition_有効な入力の場合は状態遷移する
    /// x TryTransition_遷移した場合イベント発火
    /// TODO:_現状mediatorのOnStateChangedを直接叩かせているので、イベント駆動でmediatorがnodeを知っている構造に変更する
    /// 
    /// IsAbove_patternsのうち現在より前のインデックスのspriteが渡された場合、false
    /// IsAbove_〃_先のインデックスのspriteが渡された場合、true
    /// IsAbove_〃_現在のインデックスと同じインデックスのspriteが渡された場合、false
    /// IsAbove_〃_patternsに存在しないスプライトが渡された場合、例外を投げる
    ///
    /// ※_patternsのデータ構造は_from(sprite)_=>_to(sprite)_の関係リストの入れ子
    /// hoodieに関しては_閉じ_=>_半開_=>_開き_の線形遷移なので、IsAboveも_閉じ_からのインデックスで求められる
    /// TODO:_SpriteStatePatternにインデックス計算、線形データ制約のバリデーションを実装する
    /// 
    /// ForceState_渡されたSpriteに強制遷移する
    /// ForceState_遷移時にイベント発火
    /// ForceState_patternsに存在しないSpriteが指定された場合、例外を投げる
    ///
    /// CurrentState_現在のSpriteを返す
    ///


    public class SpriteStateTest
    {
        private SpriteState _spriteState;
        private Sprite _initialSprite;
        private Sprite _nextSprite;
        private SpriteStatePattern _pattern;
        private DragDirectionThresholdSetting _setting;

        [SetUp]
        public void SetUp()
        {
            SetUpTestData();

            var go = new GameObject();
            go.AddComponent<Image>();
            _spriteState = go.AddComponent<SpriteState>();
            _spriteState.Construct(_pattern, _setting);
        }

        private void SetUpTestData()
        {
            _initialSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
            _nextSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

            _pattern = ScriptableObject.CreateInstance<SpriteStatePattern>();
            _pattern.initialState = _initialSprite;
            _pattern.transitions.Add(new StateTransition
            {
                fromState = _initialSprite,
                requiredDirection = CardinalDirection.Down,
                toState = _nextSprite
            });

            _setting = ScriptableObject.CreateInstance<DragDirectionThresholdSetting>();
        }

        [Test]
        public void Construct_初期状態のSpriteはinitialState()
        {
            // Arrange
            Sprite expected = _initialSprite;

            // Arrange
            Assert.AreEqual(expected, _spriteState.CurrentState);
        }

        [Test]
        public void TryTransition_有効な入力の場合は状態遷移する()
        {
            // Arrange
            Sprite expected = _nextSprite;

            // Act
            ZrushyInput input = new ZrushyInput(new System.Numerics.Vector2(0, -1));
            _spriteState.TryTransition(input);

            // Assert
            Assert.AreEqual(expected, _spriteState.CurrentState);
        }

        [Test]
        public void TryTransition_無効な入力の場合何もしない()
        {
            // Arrange
            Sprite expected = _initialSprite; // 遷移しないので初期状態から変化なし

            // Act
            ZrushyInput input = new ZrushyInput(new System.Numerics.Vector2(0, 0)); // 無効な入力
            _spriteState.TryTransition(input);

            // Assert
            Assert.AreEqual(_initialSprite, _spriteState.CurrentState);
        }

        [Test]
        public void TryTransition_遷移した場合イベント発火()
        {
            // Arrange
            var isFired = false;
            Action<ISpriteStateNode> callback = _ => isFired = true;
            _spriteState.OnStateChanged += callback;

            // Act
            ZrushyInput input = new ZrushyInput(new System.Numerics.Vector2(0, -1));
            _spriteState.TryTransition(input);

            // Assert
            Assert.True(isFired);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator SpriteStateWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
