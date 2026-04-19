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
    /// 
    /// x IsAbove_patternsのうち現在より先のインデックスのspriteが渡された場合_false
    /// x IsAbove_〃_前のインデックスのspriteが渡された場合_true
    /// x IsAbove_〃_現在のインデックスと同じインデックスのspriteが渡された場合_false
    /// x IsAbove_〃_patternsに存在しないスプライトが渡された場合_例外を投げる
    ///
    /// ※_patternsのデータ構造は_from(sprite)_=>_to(sprite)_の関係リストの入れ子
    /// hoodieに関しては_閉じ_=>_半開_=>_開き_の線形遷移なので_IsAboveも_閉じ_からのインデックスで求められる
    /// TODO:_SpriteStatePatternにインデックス計算_線形データ制約のバリデーションを実装する
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
        private Sprite _furtherSprite;
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
            _furtherSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

            _pattern = ScriptableObject.CreateInstance<SpriteStatePattern>();
            _pattern.initialState = _initialSprite;
            _pattern.Add(new StateTransition
            {
                fromState = _initialSprite,
                requiredDirection = CardinalDirection.Down,
                toState = _nextSprite
            });
            _pattern.Add(new StateTransition
            {
                fromState = _nextSprite,
                requiredDirection = CardinalDirection.Down,
                toState = _furtherSprite
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
            ZrushyInput input = new ZrushyInput("dummy", new System.Numerics.Vector2(0, -1));
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
            ZrushyInput input = new ZrushyInput("dummy", new System.Numerics.Vector2(0, 0)); // 無効な入力
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
            ZrushyInput input = new ZrushyInput("dummy", new System.Numerics.Vector2(0, -1));
            _spriteState.TryTransition(input);

            // Assert
            Assert.True(isFired);
        }

        [Test]
        public void IsAbove_patternsのうち現在状態より先のインデックスのspriteが渡された場合_false()
        {
            // Arrange
            _spriteState.ForceState(1);

            // Assert
            Assert.False(_spriteState.IsAbove(2));
        }

        [Test]
        public void IsAbove_patternsのうち現在状態より前のインデックスのspriteが渡された場合_true()
        {
            // Arrange
            _spriteState.ForceState(1);

            // Assert
            Assert.True(_spriteState.IsAbove(0));
        }

        [Test]
        public void IsAbove_patternsのうち_現在のインデックスと同じインデックスのspriteが渡された場合_false()
        {
            // Arrange
            _spriteState.ForceState(1);

            // Assert
            Assert.False(_spriteState.IsAbove(1));
        }
    }
}
